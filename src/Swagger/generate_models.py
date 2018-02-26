"""
Generate model classes from Kubernetes swagger.
"""

import os.path
import pprint
import yaml

BASE_DIRECTORY = os.path.abspath('../KubeClient/Models')
ROOT_NAMESPACE = 'KubeClient.Models'
IGNORE_MODELS = [
    'io.k8s.apimachinery.pkg.apis.meta.v1.Time'
    'io.k8s.apimachinery.pkg.api.resource.Quantity',
    'io.k8s.apimachinery.pkg.util.intstr.IntOrString'
]
VALUE_TYPE_NAMES = [
    'int',
    'DateTime'
]
LINE_ENDING = '\n'


class KubeModel(object):
    """
    Represents a Kubernetes API model.
    """

    def __init__(self, name, summary, api_version, pretty_api_version):
        self.name = name
        self.api_version = api_version
        self.pretty_api_version = pretty_api_version
        self.clr_name = self.name + self.pretty_api_version
        self.summary = summary or 'No summary provided'
        self.properties = {}

    def update_properties(self, property_definitions, data_types):
        self.properties.clear()

        for property_name in sorted(property_definitions.keys(), key=get_defname_sort_key):
            self.properties[property_name] = KubeModelProperty.from_definition(
                property_name,
                property_definitions[property_name],
                data_types
            )

    def is_resource(self):
        kind_property = self.properties.get('kind')
        if not kind_property or kind_property.data_type.name != 'string':
            return False

        api_version_property = self.properties.get('apiVersion')
        if not api_version_property or api_version_property.data_type.name != 'string':
            return False

        return self.has_metadata()

    def is_resource_list(self):
        kind_property = self.properties.get('kind')
        if not kind_property or kind_property.data_type.name != 'string':
            return False

        api_version_property = self.properties.get('apiVersion')
        if not api_version_property or api_version_property.data_type.name != 'string':
            return False

        return self.has_list_metadata()

    def has_metadata(self):
        metadata_property = self.properties.get('metadata')
        if not metadata_property:
            return False

        if metadata_property.data_type.name != 'ObjectMeta':
            return False

        return True

    def has_list_metadata(self):
        metadata_property = self.properties.get('metadata')
        if not metadata_property:
            return False

        if metadata_property.data_type.name != 'ListMeta':
            return False

        return True

    @classmethod
    def from_definition(cls, definition_name, definition):
        (name, api_version, pretty_api_version) = KubeModel.get_model_info(definition_name)
        summary = definition.get('description', 'No description provided.')

        return KubeModel(name, summary, api_version, pretty_api_version)


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
    def __init__(self, name, summary, data_type, is_optional):
        self.name = capitalize_name(name)
        self.json_name = name
        self.summary = summary or 'No summary provided'
        self.summary = self.summary.replace('<', '&lt;').replace('>', '&gt;', '&', '&amp;')
        self.data_type = data_type
        self.is_optional = is_optional

    def __repr__(self):
        return 'Property(name="{}",type="{}")'.format(
            self.name,
            self.data_type.to_clr_type_name()
        )

    @classmethod
    def from_definition(cls, name, property_definition, data_types):
        summary = property_definition.get('description', 'Description not provided.')
        is_optional = summary.startswith('Optional') or 'if not specified' in summary
        data_type = KubeDataType.from_definition(property_definition, data_types)

        return KubeModelProperty(name, summary, data_type, is_optional)


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

    def to_clr_type_name(self, is_nullable=False):
        return 'Dictionary<string, {}>'.format( # AFAICT, Kubernetes models only use strings as dictionary keys
            get_cts_type_name(self.element_type.to_clr_type_name(is_nullable)).replace('?', '') # Dictionary<string, DateTime?> would be odious to deal with.
        )

class KubeModelDataType(KubeDataType):
    def __init__(self, model):
        super().__init__(model.name, model.summary)
        self.model = model
        self.clr_name = self.model.name + self.model.api_version

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
    return {
        definition_name: KubeModel.from_definition(
            definition_name,
            definitions[definition_name]
        )
        for definition_name in definitions.keys()
        if definition_name not in IGNORE_MODELS
    }

def get_data_types(models):
    data_types = {
        model_name: KubeModelDataType(
            models[model_name]
        )
        for model_name in models.keys()
    }
    data_types.update({ # Well-known intrinsic data-types
        'integer': KubeIntrinsicDataType('int'),
        'string': KubeIntrinsicDataType('string'),
        'io.k8s.apimachinery.pkg.apis.meta.v1.Time': KubeIntrinsicDataType('DateTime'),
        'io.k8s.apimachinery.pkg.util.intstr.IntOrString': KubeIntrinsicDataType('string'),
        'io.k8s.apimachinery.pkg.api.resource.Quantity': KubeIntrinsicDataType('string')
    })

    return data_types

def parse_properties(models, data_types, definitions):
    for definition_name in definitions.keys():
        definition = definitions[definition_name]
        properties = definition.get('properties')
        if not properties:
            continue

        model = models[definition_name]
        model.update_properties(properties, data_types)

def main():
    try:
        os.stat(BASE_DIRECTORY)
    except FileNotFoundError:
        os.mkdir(BASE_DIRECTORY)

    with open('kube-swagger.yml') as kube_swagger_file:
        kube_swagger = yaml.load(kube_swagger_file)

    definitions = kube_swagger["definitions"]

    models = parse_models(definitions)
    data_types = get_data_types(models)
    parse_properties(models, data_types, definitions)

    for definition_name in sorted(definitions.keys(), key=get_defname_sort_key):
        if definition_name in IGNORE_MODELS or definition_name.startswith('apimachinery.pkg.'):
            continue

        model = models[definition_name]

        class_file_name = os.path.join(BASE_DIRECTORY, model.clr_name + '.cs')
        with open(class_file_name, 'w') as class_file:
            class_file.write('using Newtonsoft.Json;' + LINE_ENDING)
            class_file.write('using System;' + LINE_ENDING)
            class_file.write('using System.Collections.Generic;' + LINE_ENDING)
            class_file.write(LINE_ENDING)
            class_file.write('namespace ' + ROOT_NAMESPACE + LINE_ENDING)
            class_file.write('{' + LINE_ENDING)

            class_file.write('    /// <summary>' + LINE_ENDING)

            for model_summary_line in model.summary.split('\n'):
                class_file.write('    ///     ' + model_summary_line + LINE_ENDING)
            class_file.write('    /// </summary>' + LINE_ENDING)

            class_file.write('    [KubeObject("{0}", "{1}")]{2}'.format(
                model.name,
                model.api_version,
                LINE_ENDING
            ))

            class_file.write('    public class ' + model.clr_name)
            if model.is_resource():
                class_file.write(' : KubeResourceV1')
            elif model.is_resource_list():
                class_file.write(' : KubeResourceListV1')
            class_file.write(LINE_ENDING)

            class_file.write('    {' + LINE_ENDING)

            properties = model.properties
            property_names = [name for name in properties.keys()]

            if model.is_resource() or model.is_resource_list():
                property_names.remove('apiVersion')
                property_names.remove('kind')

                if model.has_metadata() or model.has_list_metadata():
                    property_names.remove('metadata')

            for property_index in range(0, len(property_names)):
                property_name = property_names[property_index]
                model_property = properties[property_name]

                class_file.write('        /// <summary>' + LINE_ENDING)
                for property_summary_line in model_property.summary.split('\n'):
                    class_file.write('        ///     ' + property_summary_line + LINE_ENDING)
                class_file.write('        /// </summary>' + LINE_ENDING)

                if model_property.data_type.is_collection():
                    class_file.write('        [JsonProperty("%s", NullValueHandling = NullValueHandling.Ignore)]%s' % (model_property.json_name, LINE_ENDING))
                    class_file.write('        public %s %s { get; set; } = new %s();%s' % (
                        model_property.data_type.to_clr_type_name(),
                        model_property.name,
                        model_property.data_type.to_clr_type_name(),
                        LINE_ENDING
                    ))
                else:
                    class_file.write('        [JsonProperty("%s")]%s' % (model_property.json_name, LINE_ENDING))
                    class_file.write('        public %s %s { get; set; }%s' % (
                        model_property.data_type.to_clr_type_name(is_nullable=model_property.is_optional),
                        model_property.name,
                        LINE_ENDING
                    ))

                if property_index + 1 < len(property_names):
                    class_file.write(LINE_ENDING)

            class_file.write('    }' + LINE_ENDING) # Class

            class_file.write('}' + LINE_ENDING) # Namespace

if __name__ == '__main__':
    main()
