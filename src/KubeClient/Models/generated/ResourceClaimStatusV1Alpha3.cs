using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaimStatus tracks whether the resource has been allocated and what the result of that was.
    /// </summary>
    public partial class ResourceClaimStatusV1Alpha3
    {
        /// <summary>
        ///     Indicates that a claim is to be deallocated. While this is set, no new consumers may be added to ReservedFor.
        ///     
        ///     This is only used if the claim needs to be deallocated by a DRA driver. That driver then must deallocate this claim and reset the field together with clearing the Allocation field.
        ///     
        ///     This is an alpha field and requires enabling the DRAControlPlaneController feature gate.
        /// </summary>
        [YamlMember(Alias = "deallocationRequested")]
        [JsonProperty("deallocationRequested", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DeallocationRequested { get; set; }

        /// <summary>
        ///     Allocation is set once the claim has been allocated successfully.
        /// </summary>
        [YamlMember(Alias = "allocation")]
        [JsonProperty("allocation", NullValueHandling = NullValueHandling.Ignore)]
        public AllocationResultV1Alpha3 Allocation { get; set; }

        /// <summary>
        ///     ReservedFor indicates which entities are currently allowed to use the claim. A Pod which references a ResourceClaim which is not reserved for that Pod will not be started. A claim that is in use or might be in use because it has been reserved must not get deallocated.
        ///     
        ///     In a cluster with multiple scheduler instances, two pods might get scheduled concurrently by different schedulers. When they reference the same ResourceClaim which already has reached its maximum number of consumers, only one pod can be scheduled.
        ///     
        ///     Both schedulers try to add their pod to the claim.status.reservedFor field, but only the update that reaches the API server first gets stored. The other one fails with an error and the scheduler which issued it knows that it must put the pod back into the queue, waiting for the ResourceClaim to become usable again.
        ///     
        ///     There can be at most 32 such reservations. This may get increased in the future, but not reduced.
        /// </summary>
        [MergeStrategy(Key = "uid")]
        [YamlMember(Alias = "reservedFor")]
        [JsonProperty("reservedFor", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ResourceClaimConsumerReferenceV1Alpha3> ReservedFor { get; } = new List<ResourceClaimConsumerReferenceV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="ReservedFor"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeReservedFor() => ReservedFor.Count > 0;
    }
}
