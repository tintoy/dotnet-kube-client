using KubeClient.Models;

namespace KubeClient.Extensions.CustomResources.Schema
{
    /// <summary>
    ///     Schema-related extension methods for working with <see cref="KubeCustomResourceV1{TSpecification}"/> and related types.
    /// </summary>
    public static class CustomResourceSchemaExtensions
    {
        /// <summary>
        ///     Generate a JSON schema for validating the specification model for a Custom Resource Definition (CRD).
        /// </summary>
        /// <typeparam name="TSpecification">
        ///     The type of model used to represent the CRD specification.
        /// </typeparam>
        /// <param name="customResource">
        ///     The Custom Resource Definition (CRD).
        /// </param>
        /// <returns>
        ///     The configured <see cref="JSONSchemaPropsV1"/>.
        /// </returns>
        public static JSONSchemaPropsV1Beta1 GenerateSpecificationSchema<TSpecification>(this KubeCustomResourceV1Beta1<TSpecification> customResource)
            where TSpecification : class
        {
            if (customResource == null)
                throw new ArgumentNullException(nameof(customResource));

            return SchemaGeneratorV1Beta1.GenerateSchema(modelType: typeof(TSpecification));
        }

        /// <summary>
        ///     Generate a JSON schema for validating the specification model for a Custom Resource Definition (CRD).
        /// </summary>
        /// <typeparam name="TSpecification">
        ///     The type of model used to represent the CRD specification.
        /// </typeparam>
        /// <param name="customResource">
        ///     The Custom Resource Definition (CRD).
        /// </param>
        /// <returns>
        ///     The configured <see cref="JSONSchemaPropsV1"/>.
        /// </returns>
        public static JSONSchemaPropsV1 GenerateSpecificationSchema<TSpecification>(this KubeCustomResourceV1<TSpecification> customResource)
            where TSpecification : class
        {
            if (customResource == null)
                throw new ArgumentNullException(nameof(customResource));

            return SchemaGeneratorV1.GenerateSchema(modelType: typeof(TSpecification));
        }
    }
}
