"""
Generate model classes from Kubernetes swagger.
"""

import os.path
import pprint
import yaml

BASE_DIRECTORY = os.path.abspath('../KubeClient/Models/generated')
ROOT_NAMESPACE = 'KubeClient.Models'
IGNORE_MODELS = {
    'io.k8s.apimachinery.pkg.apis.meta.v1.DeleteOptions',
    'io.k8s.apimachinery.pkg.apis.meta.v1.Time',
    'io.k8s.apimachinery.pkg.api.resource.Quantity',
    'io.k8s.apimachinery.pkg.util.intstr.IntOrString',

    # Present in both regular and and 'extensions' groups:
    'io.k8s.api.extensions.v1beta1.Deployment',
    'io.k8s.api.extensions.v1beta1.DeploymentList',
    'io.k8s.api.extensions.v1beta1.DeploymentRollback',
    'io.k8s.api.extensions.v1beta1.NetworkPolicy',
    'io.k8s.api.extensions.v1beta1.NetworkPolicyList',
    'io.k8s.api.extensions.v1beta1.PodSecurityPolicy',
    'io.k8s.api.extensions.v1beta1.PodSecurityPolicyList',
    'io.k8s.api.extensions.v1beta1.ReplicaSet',
    'io.k8s.api.extensions.v1beta1.ReplicaSetList',
    'io.k8s.kubernetes.pkg.apis.apps.v1beta1.ControllerRevision',
    'io.k8s.kubernetes.pkg.apis.apps.v1beta1.ControllerRevisionList',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DaemonSet',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DaemonSetList',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.Deployment',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DeploymentList',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DeploymentRollback',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.NetworkPolicy',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.NetworkPolicyList',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.PodSecurityPolicy',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.PodSecurityPolicyList',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ReplicaSet',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ReplicaSetList',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.Scale',

    # Hand-coded:
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ThirdPartyResource',
    'io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ThirdPartyResourceList'
}
VALUE_TYPE_NAMES = [
    'bool',
    'int',
    'long',
    'double',
    'DateTime'
]
KUBE_ACTIONS = {
    'deletecollection': 'DeleteCollection',
    'list': 'List',
    'post': 'Create',
    'delete': 'Delete',
    'get': 'Get',
    'patch': 'Patch',
    'put': 'Update',
    'watch': 'Watch',
    'watchlist': 'WatchList',
}
LINE_ENDING = '\n'


class KubeModel(object):
    """
    Represents a Kubernetes API model.
    """

    def __init__(self, name, summary, api_version, pretty_api_version, kube_group, required_property_keys):
        self.name = name
        self.api_version = api_version
        self.pretty_api_version = pretty_api_version
        self.kube_group = kube_group
        if self.kube_group:
            self.api_groupversion = '{0}/{1}'.format(self.kube_group, self.api_version)
        else:
            self.api_groupversion = self.api_version
        self.clr_name = self.name + self.pretty_api_version
        self.summary = summary or 'No summary provided'
        self.required_property_keys = required_property_keys
        self.properties = {}

    def update_properties(self, property_definitions, data_types):
        self.properties.clear()

        for property_name in sorted(property_definitions.keys(), key=get_defname_sort_key):
            safe_property_name = property_name
            if safe_property_name.startswith('$'):
                # Trim prefix
                safe_property_name = safe_property_name[1:]

                # Capitalise
                safe_property_name = safe_property_name[0].capitalize() + safe_property_name[1:]
                safe_property_name = safe_property_name.replace('$', 'dollar')

            self.properties[safe_property_name] = KubeModelProperty.from_definition(
                safe_property_name,
                property_name,
                property_definitions[property_name],
                data_types,
                is_optional=(
                    property_name not in self.required_property_keys
                )
            )

    def is_kube_object(self):
        kind_property = self.properties.get('kind')
        if not kind_property or kind_property.data_type.name != 'string':
            return False

        api_version_property = self.properties.get('apiVersion')
        if not api_version_property or api_version_property.data_type.name != 'string':
            return False

        return True

    def is_kube_resource(self):
        return self.is_kube_object() and self.has_kube_metadata()

    def is_kube_resource_list(self):
        kind_property = self.properties.get('kind')
        if not kind_property or kind_property.data_type.name != 'string':
            return False

        api_version_property = self.properties.get('apiVersion')
        if not api_version_property or api_version_property.data_type.name != 'string':
            return False

        return self.has_kube_list_metadata()

    def has_kube_metadata(self):
        metadata_property = self.properties.get('metadata')
        if not metadata_property:
            return False

        if metadata_property.data_type.name != 'ObjectMeta':
            return False

        return True

    def has_kube_list_metadata(self):
        metadata_property = self.properties.get('metadata')
        if not metadata_property:
            return False

        if metadata_property.data_type.name != 'ListMeta':
            return False

        return True

    def has_list_items(self):
        items_property = self.properties.get('items')
        if not items_property:
            return False

        return items_property.data_type.is_collection()

    def list_item_data_type(self):
        if not self.has_list_items():
            return None

        return self.properties['items'].data_type.element_type

    @classmethod
    def from_definition(cls, definition_name, definition):
        (name, api_version, pretty_api_version) = KubeModel.get_model_info(definition_name)
        summary = definition.get(
            'description',
            'No description provided.'
        ).replace(
            '&', '&amp;'
        ).replace(
            '<', '&lt;'
        ).replace(
            '>', '&gt;'
        )

        required_property_keys = set(
            definition.get('required') or []
        )

        # Override model metadata with Kubernetes-specific values, if available.
        kube_group = ''
        if 'x-kubernetes-group-version-kind' in definition:
            kube_metadata = definition['x-kubernetes-group-version-kind'][0]
            kube_group = kube_metadata.get('group', '')
            kube_kind = kube_metadata['kind']
            api_version = kube_metadata['version']

            name = kube_kind

        return KubeModel(name, summary, api_version, pretty_api_version, kube_group, required_property_keys)


    @classmethod
    def get_model_info(cls, definition_name):
        name_components = definition_name.split('.')
        if len(name_components) < 2:
            return (definition_name[-1], '', '')

        name = capitalize_name(name_components[-1])

        api_version = name_components[-2]
        pretty_api_version = api_version.capitalize().replace(
            'alpha', 'Alpha'
        ).replace(
            'beta', 'Beta'
        )

        return (name, api_version, pretty_api_version)

    def __repr__(self):
        return 'KubeModel(name="{}",version="{}")\n{}'.format(
            self.name,
            self.api_version,
            pprint.pformat([
                self.properties.values()
            ])
        )


class KubeModelProperty(object):
    def __init__(self, name, json_name, summary, data_type, is_optional, is_merge, is_retain_keys, merge_key):
        self.name = capitalize_name(name)
        self.json_name = json_name
        self.summary = summary or 'No summary provided'
        self.summary = self.summary.replace(
            '&', '&amp;'
        ).replace(
            '<', '&lt;'
        ).replace(
            '>', '&gt;'
        )
        self.data_type = data_type
        self.is_optional = is_optional
        self.is_merge = is_merge
        self.is_retain_keys = is_retain_keys
        self.merge_key = merge_key

    def __repr__(self):
        return 'KubeModelProperty(name="{}",type={})'.format(
            self.name,
            repr(self.data_type)
        )

    @classmethod
    def from_definition(cls, name, json_name, property_definition, data_types, is_optional):
        summary = property_definition.get('description', 'Description not provided.')
        data_type = KubeDataType.from_definition(property_definition, data_types)

        is_merge = False
        is_retain_keys = False

        if property_definition.get('x-kubernetes-patch-strategy') == 'merge':
            is_merge = True
        elif property_definition.get('x-kubernetes-patch-strategy') == 'merge,retainKeys':
            is_merge = True
            is_retain_keys = True
        elif property_definition.get('x-kubernetes-patch-strategy') == 'retainKeys':
            is_retain_keys = True

        merge_key = property_definition.get('x-kubernetes-patch-merge-key')

        return KubeModelProperty(name, json_name, summary, data_type, is_optional, is_merge, is_retain_keys, merge_key)


class KubeDataType(object):
    def __init__(self, name, summary):
        self.name = name
        self.summary = summary

    def is_intrinsic(self):
        return False

    def is_collection(self):
        return False

    def to_clr_type_name(self, is_nullable=False):
        return get_cts_type_name(self.name)

    def __repr__(self):
        return "KubeDataType(name='{0}')".format(self.name)

    @classmethod
    def from_definition(cls, definition, data_types):

        if 'type' in definition:
            type_name = definition['type']
            if type_name == 'array':
                item_definition = definition['items']
                element_type = KubeDataType.from_definition(item_definition, data_types)

                return KubeArrayDataType(element_type)
            elif type_name == 'object':
                item_definition = definition['additionalProperties']
                element_type = KubeDataType.from_definition(item_definition, data_types)

                return KubeDictionaryDataType(element_type)
            elif type_name == 'number':
                type_format = definition['format']
                if type_format == 'double':
                    return KubeIntrinsicDataType('double')
            elif type_name == 'integer':
                type_format = definition['format']
                if type_format == 'int32':
                    return KubeIntrinsicDataType('int')
                elif type_format == 'int64':
                    return KubeIntrinsicDataType('long')
            else:
                if type_name in data_types:
                    return data_types[type_name]

                data_type = KubeIntrinsicDataType(type_name)
                data_types[type_name] = data_type

                return data_type

        type_name = definition['$ref'].replace('#/definitions/', '')
        if type_name in data_types:
            return data_types[type_name]

        summary = definition.get('description', 'Description not provided.')
        data_type = KubeDataType(type_name, summary)
        data_types[type_name] = data_type

        return data_type

class KubeIntrinsicDataType(KubeDataType):
    def __init__(self, name):
        super().__init__(name, 'Intrinsic data-type.')

    def is_intrinsic(self):
        return True

    def __repr__(self):
        return "KubeIntrinsicDataType(name='{0}')".format(self.name)

    def to_clr_type_name(self, is_nullable=False):
        clr_type_name = super().to_clr_type_name(is_nullable)

        if (is_nullable and clr_type_name in VALUE_TYPE_NAMES) or clr_type_name == 'DateTime':
            clr_type_name += '?'

        return clr_type_name

class KubeArrayDataType(KubeDataType):
    def __init__(self, element_type):
        super().__init__(element_type.name, element_type.summary)

        self.element_type = element_type

    def is_collection(self):
        return True

    def __repr__(self):
        return "KubeArrayDataType(element_type={0})".format(
            repr(self.element_type)
        )

    def to_clr_type_name(self, is_nullable=False):
        return 'List<{}>'.format(
            get_cts_type_name(self.element_type.to_clr_type_name(is_nullable)).replace('?', '') # List<DateTime?> would be odious to deal with.
        )

class KubeDictionaryDataType(KubeDataType):
    def __init__(self, element_type):
        super().__init__(element_type.name, element_type.summary)

        self.element_type = element_type

    def is_collection(self):
        return True

    def __repr__(self):
        return "KubeDictionaryDataType(element_type={0})".format(
            repr(self.element_type)
        )

    def to_clr_type_name(self, is_nullable=False):
        return 'Dictionary<string, {}>'.format( # AFAICT, Kubernetes models only use strings as dictionary keys
            get_cts_type_name(self.element_type.to_clr_type_name(is_nullable)).replace('?', '') # Dictionary<string, DateTime?> would be odious to deal with.
        )

class KubeModelDataType(KubeDataType):
    def __init__(self, model):
        super().__init__(model.name, model.summary)
        self.model = model
        self.clr_name = self.model.name + self.model.api_version

    def __repr__(self):
        return "KubeModelDataType(kind='{0}', api_version='{1}', clr_type_name='{2}')".format(
            self.model.name,
            self.model.pretty_api_version,
            self.to_clr_type_name()
        )

    def to_clr_type_name(self, is_nullable=False):
        return self.model.clr_name

def capitalize_name(name):
    return name[0].capitalize() + name[1:]

def get_defname_sort_key(definition_name):
    (type_name, _, _) = KubeModel.get_model_info(definition_name)

    return type_name

def get_cts_type_name(swagger_type_name):
    if swagger_type_name == 'integer':
        return 'int'

    if swagger_type_name == 'boolean':
        return 'bool'

    return swagger_type_name

def parse_models(definitions):
    models = {
        definition_name: KubeModel.from_definition(
            definition_name,
            definitions[definition_name]
        )
        for definition_name in definitions.keys()
        if definition_name not in IGNORE_MODELS
    }

    # Some model definitions are deprecated, and remapped to other definitions
    for (definition_name, definition) in definitions.items():
        if definition_name in IGNORE_MODELS:
            continue

        # Remapped (stub) models have '$ref' but not 'properties'.
        if '$ref' in definition and 'properties' not in definition:
            map_to_definition_name = definition['$ref'].replace('#/definitions/', '')
            if map_to_definition_name in IGNORE_MODELS:
                continue

            # Point the model name to the updated definition.
            models[definition_name] = models[map_to_definition_name]

    return models

def get_data_types(models):
    data_types = {
        model_name: KubeModelDataType(
            models[model_name]
        )
        for model_name in models.keys()
    }

    # Well-known intrinsic data-types
    data_types.update({
        'integer': KubeIntrinsicDataType('int'),
        'string': KubeIntrinsicDataType('string'),
        'io.k8s.apimachinery.pkg.apis.meta.v1.Time': KubeIntrinsicDataType('DateTime'),
        'io.k8s.apimachinery.pkg.util.intstr.IntOrString': KubeIntrinsicDataType('Int32OrStringV1'),
        'io.k8s.apimachinery.pkg.api.resource.Quantity': KubeIntrinsicDataType('string'),
        'io.k8s.apimachinery.pkg.apis.meta.v1.DeleteOptions': KubeIntrinsicDataType('DeleteOptionsV1')  # This model is hand-crafted
    })

    return data_types

def parse_properties(models, data_types, definitions):
    for definition_name in definitions.keys():
        definition = definitions[definition_name]
        if definition_name not in models:
            continue

        properties = definition.get('properties')
        if not properties:
            continue

        model = models[definition_name]
        model.update_properties(properties, data_types)

def parse_apis(api_paths):
    apis = {}

    for api_path in sorted(api_paths.keys()):
        api_verbs = api_paths[api_path]
        for api_verb in sorted(api_verbs.keys()):
            if api_verb == 'parameters':
                continue

            api_metadata = api_verbs[api_verb]

            if 'x-kubernetes-action' not in api_metadata or 'x-kubernetes-group-version-kind' not in api_metadata:
                continue

            action = api_metadata['x-kubernetes-action']
            resource_metadata = api_metadata['x-kubernetes-group-version-kind']
            resource_group = resource_metadata['group']
            resource_api_version = resource_metadata['version']
            resource_kind = resource_metadata['kind']

            api_key = '{}/{}/{}'.format(resource_group, resource_api_version, resource_kind)

            api = apis.get(api_key)
            if not api:
                api = {}
                apis[api_key] = api

            if action not in api:
                api[action] = []

            api[action].append(api_path)

    return apis

def main():
    try:
        os.stat(BASE_DIRECTORY)
    except FileNotFoundError:
        os.mkdir(BASE_DIRECTORY)

    with open('kube-swagger.yml') as kube_swagger_file:
        kube_swagger = yaml.load(kube_swagger_file)

    paths = kube_swagger["paths"]
    apis = parse_apis(paths)

    definitions = kube_swagger["definitions"]
    models = parse_models(definitions)

    data_types = get_data_types(models)
    parse_properties(models, data_types, definitions)

    for definition_name in sorted(definitions.keys(), key=get_defname_sort_key):
        if definition_name in IGNORE_MODELS:
            continue

        model = models[definition_name]

        class_namespace = ROOT_NAMESPACE
        class_directory_path = BASE_DIRECTORY

        if not os.path.exists(class_directory_path):
            os.mkdir(class_directory_path)
        
        resource_api_key = '{}/{}/{}'.format(model.kube_group, model.api_version, model.name)
        resource_api = apis.get(resource_api_key)

        class_file_name = os.path.join(class_directory_path, model.clr_name + '.cs')
        with open(class_file_name, 'w') as class_file:
            class_file.write('using Newtonsoft.Json;' + LINE_ENDING)
            class_file.write('using System;' + LINE_ENDING)
            class_file.write('using System.Collections.Generic;' + LINE_ENDING)
            class_file.write('using YamlDotNet.Serialization;' + LINE_ENDING)
            class_file.write(LINE_ENDING)
            class_file.write('namespace ' + class_namespace + LINE_ENDING)
            class_file.write('{' + LINE_ENDING)

            class_file.write('    /// <summary>' + LINE_ENDING)

            for model_summary_line in model.summary.split('\n'):
                class_file.write('    ///     ' + model_summary_line + LINE_ENDING)
            class_file.write('    /// </summary>' + LINE_ENDING)

            model_annotations = []

            if model.has_list_items():
                try:
                    list_item_model = model.list_item_data_type().model
                except AttributeError:
                    print('List item datatype "{0}" is not a model'.format(model.name))
                    pprint.pprint(model.list_item_data_type)

                    raise

                model_annotations.append('    [KubeListItem("{0}", "{1}")]{2}'.format(
                    list_item_model.name,
                    list_item_model.api_groupversion,
                    LINE_ENDING
                ))

            if model.is_kube_resource() or model.is_kube_resource_list():
                model_annotations.append('    [KubeObject("{0}", "{1}")]{2}'.format(
                    model.name,
                    model.api_groupversion,
                    LINE_ENDING
                ))

            # TODO: Add KubeResourceAliasAttribute, but how do we infer singularName and shortNames? These are only available via the API.

            if model.is_kube_resource() and resource_api:
                added_annotations = set()
                action_paths = {}
                for action in sorted(resource_api.keys()):
                    api_paths = resource_api[action]
                    api_action = 'KubeAction.' + KUBE_ACTIONS.get(action,
                        action.capitalize()  # Default
                    )

                    for api_path in api_paths:
                        if api_action not in action_paths:
                            action_paths[api_action] = []

                        action_paths[api_action].append(api_path)

                for api_action in sorted(action_paths.keys()):
                    for api_path in sorted(action_paths[api_action]):
                        annotation = '    [KubeApi({0}, "{1}")]{2}'.format(
                            api_action,
                            api_path.strip('/'),
                            LINE_ENDING
                        )

                        # Ignore duplicates.
                        if annotation not in added_annotations:
                            model_annotations.append(annotation)
                            added_annotations.add(annotation)

            model_annotations.sort(key=len)  # Shorter attributes come first
            for model_annotation in model_annotations:
                class_file.write(model_annotation)

            class_file.write('    public partial class ' + model.clr_name)
            if model.is_kube_resource():
                class_file.write(' : KubeResourceV1')
            elif model.is_kube_resource_list():
                if model.has_list_items():
                    class_file.write(' : KubeResourceListV1<{0}>'.format(
                        model.list_item_data_type().to_clr_type_name()
                    ))
                else:
                    class_file.write(' : KubeResourceListV1')
            elif model.is_kube_object():
                class_file.write(' : KubeObjectV1')

            class_file.write(LINE_ENDING)

            class_file.write('    {' + LINE_ENDING)

            properties = model.properties
            property_names = [name for name in properties.keys()]

            if model.is_kube_object():
                property_names.remove('apiVersion')
                property_names.remove('kind')

                if model.has_kube_metadata() or model.has_kube_list_metadata():
                    property_names.remove('metadata')

                if model.is_kube_resource_list() and model.has_list_items():
                    property_names.remove('items')

            for property_index in range(0, len(property_names)):
                property_name = property_names[property_index]
                model_property = properties[property_name]

                class_file.write('        /// <summary>' + LINE_ENDING)
                for property_summary_line in model_property.summary.split('\n'):
                    class_file.write('        ///     ' + property_summary_line + LINE_ENDING)
                class_file.write('        /// </summary>' + LINE_ENDING)

                if model_property.data_type.is_collection():
                    if model_property.is_retain_keys:
                        class_file.write('        [RetainKeysStrategy]%s' % (LINE_ENDING, ))

                    # Shorter attribute comes before [YamlMember]...
                    if model_property.is_merge:
                        if not model_property.merge_key:
                            class_file.write('        [MergeStrategy]%s' % (LINE_ENDING,))
                        elif len(model_property.merge_key) <= len(model_property.json_name):
                            class_file.write('        [MergeStrategy(Key = "%s")]%s' % (model_property.merge_key, LINE_ENDING))  

                    class_file.write('        [YamlMember(Alias = "%s")]%s' % (model_property.json_name, LINE_ENDING))

                    # ...but longer attribute comes after [YamlMember].
                    if model_property.is_merge:
                        if model_property.merge_key and len(model_property.merge_key) > len(model_property.json_name):
                            class_file.write('        [MergeStrategy(Key = "%s")]%s' % (model_property.merge_key, LINE_ENDING))

                    class_file.write('        [JsonProperty("%s", ObjectCreationHandling = ObjectCreationHandling.Reuse)]%s' % (model_property.json_name, LINE_ENDING))

                    class_file.write('        public %s %s { get; } = new %s();%s' % (
                        model_property.data_type.to_clr_type_name(),
                        model_property.name,
                        model_property.data_type.to_clr_type_name(),
                        LINE_ENDING
                    ))

                    # Don't serialise empty lists for optional properties.
                    # See tintoy/dotnet-kube-client#36 for reasoning behind this.
                    if model_property.is_optional:
                        class_file.write(LINE_ENDING)

                        class_file.write('        /// <summary>' + LINE_ENDING)
                        class_file.write('        ///     Determine whether the <see cref="{0}"/> property should be serialised.{1}'.format(model_property.name, LINE_ENDING))
                        class_file.write('        /// </summary>' + LINE_ENDING)
                        class_file.write('        public bool ShouldSerialize{0}() => {0}.Count > 0;{1}'.format(model_property.name, LINE_ENDING))
                else:
                    if model_property.is_retain_keys:
                        class_file.write('        [RetainKeysStrategy]%s' % (LINE_ENDING, ))

                    # Shorter attribute comes before [JsonProperty]...
                    if model_property.is_merge:
                        if not model_property.merge_key:
                            class_file.write('        [MergeStrategy]%s' % (LINE_ENDING,))

                    class_file.write('        [YamlMember(Alias = "%s")]%s' % (model_property.json_name, LINE_ENDING))

                    if model_property.is_optional:
                        class_file.write('        [JsonProperty("%s", NullValueHandling = NullValueHandling.Ignore)]%s' % (model_property.json_name, LINE_ENDING))
                    else:
                        class_file.write('        [JsonProperty("%s", NullValueHandling = NullValueHandling.Include)]%s' % (model_property.json_name, LINE_ENDING))

                    # ...but longer attribute comes after [YamlMember].
                    if model_property.is_merge:
                        if model_property.merge_key:
                            class_file.write('        [MergeStrategy(Key = "%s")]%s' % (model_property.merge_key, LINE_ENDING))

                    class_file.write('        public %s %s { get; set; }%s' % (
                        model_property.data_type.to_clr_type_name(is_nullable=model_property.is_optional),
                        model_property.name,
                        LINE_ENDING
                    ))

                if property_index + 1 < len(property_names):
                    class_file.write(LINE_ENDING)

            # Special case for Items property (we override the base class's property, adding the JsonProperty attribute).
            if model.is_kube_resource_list() and model.has_list_items():
                model_property = model.properties['items']

                class_file.write('        /// <summary>' + LINE_ENDING)
                for property_summary_line in model_property.summary.split('\n'):
                    class_file.write('        ///     ' + property_summary_line + LINE_ENDING)
                class_file.write('        /// </summary>' + LINE_ENDING)

                class_file.write('        [JsonProperty("%s", ObjectCreationHandling = ObjectCreationHandling.Reuse)]%s' % (model_property.json_name, LINE_ENDING))
                class_file.write('        public override %s %s { get; } = new %s();%s' % (
                    model_property.data_type.to_clr_type_name(),
                    model_property.name,
                    model_property.data_type.to_clr_type_name(),
                    LINE_ENDING
                ))

            class_file.write('    }' + LINE_ENDING) # Class

            class_file.write('}' + LINE_ENDING) # Namespace

if __name__ == '__main__':
    main()
