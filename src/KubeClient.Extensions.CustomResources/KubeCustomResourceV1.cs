using System;
using Newtonsoft.Json;

namespace KubeClient.Models
{
    using Extensions.CustomResources;

    /// <summary>
    ///     The base class for models representing Kubernetes Custom Resources (CRDs).
    /// </summary>
    public abstract class KubeCustomResourceV1
        : KubeResourceV1
    {
        /// <summary>
        ///     Create a new <see cref="KubeCustomResourceV1"/>.
        /// </summary>
        protected KubeCustomResourceV1()
        {
            if (String.IsNullOrEmpty(Kind) || string.IsNullOrWhiteSpace(ApiVersion))
                throw new KubeClientException($"Class '{GetType().Name}' derives from '{nameof(KubeCustomResourceV1)}' but is not decorated with the 'KubeResource' attribute.");
        }

        /// <summary>
        ///     Generate a JSON schema for validating the specification model.
        /// </summary>
        /// <returns>
        ///     The configured <see cref="JSONSchemaPropsV1Beta1"/>.
        /// </returns>
        public abstract JSONSchemaPropsV1Beta1 GenerateSpecificationSchema();
    }

    /// <summary>
    ///     The base class for models representing Kubernetes Custom Resource Definitions (CRDs).
    /// </summary>
    /// <typeparam name="TSpecification">
    ///     The type of model used to represent the resource specification.
    /// </typeparam>
    public abstract class KubeCustomResourceV1<TSpecification>
        : KubeCustomResourceV1
        where TSpecification : class
    {
        /// <summary>
        ///     Create a new <see cref="KubeCustomResourceV1{TSpec}"/>.
        /// </summary>
        protected KubeCustomResourceV1()
        {
        }

        /// <summary>
        ///     The resource specification.
        /// </summary>
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public virtual TSpecification Specification { get; set; }

        /// <summary>
        ///     Generate a JSON schema for validating the specification model.
        /// </summary>
        /// <returns>
        ///     The configured <see cref="JSONSchemaPropsV1Beta1"/>.
        /// </returns>
        public override JSONSchemaPropsV1Beta1 GenerateSpecificationSchema() => SchemaGenerator.GenerateSchema(modelType: typeof(TSpecification));
    }
}
