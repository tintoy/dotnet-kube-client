using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Event is a report of an event somewhere in the cluster. It generally denotes some state change in the system.
    /// </summary>
    [KubeObject("Event", "v1beta1")]
    [KubeApi(KubeAction.List, "apis/events.k8s.io/v1beta1/events")]
    [KubeApi(KubeAction.WatchList, "apis/events.k8s.io/v1beta1/watch/events")]
    [KubeApi(KubeAction.List, "apis/events.k8s.io/v1beta1/namespaces/{namespace}/events")]
    [KubeApi(KubeAction.Create, "apis/events.k8s.io/v1beta1/namespaces/{namespace}/events")]
    [KubeApi(KubeAction.Get, "apis/events.k8s.io/v1beta1/namespaces/{namespace}/events/{name}")]
    [KubeApi(KubeAction.Patch, "apis/events.k8s.io/v1beta1/namespaces/{namespace}/events/{name}")]
    [KubeApi(KubeAction.Delete, "apis/events.k8s.io/v1beta1/namespaces/{namespace}/events/{name}")]
    [KubeApi(KubeAction.Update, "apis/events.k8s.io/v1beta1/namespaces/{namespace}/events/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/events.k8s.io/v1beta1/watch/namespaces/{namespace}/events")]
    [KubeApi(KubeAction.DeleteCollection, "apis/events.k8s.io/v1beta1/namespaces/{namespace}/events")]
    [KubeApi(KubeAction.Watch, "apis/events.k8s.io/v1beta1/watch/namespaces/{namespace}/events/{name}")]
    public partial class EventV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Optional secondary object for more complex actions. E.g. when regarding object triggers a creation or deletion of related object.
        /// </summary>
        [JsonProperty("related")]
        [YamlMember(Alias = "related")]
        public ObjectReferenceV1 Related { get; set; }

        /// <summary>
        ///     Deprecated field assuring backward compatibility with core.v1 Event type
        /// </summary>
        [JsonProperty("deprecatedSource")]
        [YamlMember(Alias = "deprecatedSource")]
        public EventSourceV1 DeprecatedSource { get; set; }

        /// <summary>
        ///     Required. Time when this Event was first observed.
        /// </summary>
        [JsonProperty("eventTime")]
        [YamlMember(Alias = "eventTime")]
        public MicroTimeV1 EventTime { get; set; }

        /// <summary>
        ///     Optional. A human-readable description of the status of this operation. Maximal length of the note is 1kB, but libraries should be prepared to handle values up to 64kB.
        /// </summary>
        [JsonProperty("note")]
        [YamlMember(Alias = "note")]
        public string Note { get; set; }

        /// <summary>
        ///     ID of the controller instance, e.g. `kubelet-xyzf`.
        /// </summary>
        [JsonProperty("reportingInstance")]
        [YamlMember(Alias = "reportingInstance")]
        public string ReportingInstance { get; set; }

        /// <summary>
        ///     Type of this event (Normal, Warning), new types could be added in the future.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     The object this Event is about. In most cases it's an Object reporting controller implements. E.g. ReplicaSetController implements ReplicaSets and this event is emitted because it acts on some changes in a ReplicaSet object.
        /// </summary>
        [JsonProperty("regarding")]
        [YamlMember(Alias = "regarding")]
        public ObjectReferenceV1 Regarding { get; set; }

        /// <summary>
        ///     What action was taken/failed regarding to the regarding object.
        /// </summary>
        [JsonProperty("action")]
        [YamlMember(Alias = "action")]
        public string Action { get; set; }

        /// <summary>
        ///     Why the action was taken.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Deprecated field assuring backward compatibility with core.v1 Event type
        /// </summary>
        [JsonProperty("deprecatedFirstTimestamp")]
        [YamlMember(Alias = "deprecatedFirstTimestamp")]
        public DateTime? DeprecatedFirstTimestamp { get; set; }

        /// <summary>
        ///     Deprecated field assuring backward compatibility with core.v1 Event type
        /// </summary>
        [JsonProperty("deprecatedLastTimestamp")]
        [YamlMember(Alias = "deprecatedLastTimestamp")]
        public DateTime? DeprecatedLastTimestamp { get; set; }

        /// <summary>
        ///     Name of the controller that emitted this Event, e.g. `kubernetes.io/kubelet`.
        /// </summary>
        [JsonProperty("reportingController")]
        [YamlMember(Alias = "reportingController")]
        public string ReportingController { get; set; }

        /// <summary>
        ///     Data about the Event series this event represents or nil if it's a singleton Event.
        /// </summary>
        [JsonProperty("series")]
        [YamlMember(Alias = "series")]
        public EventSeriesV1Beta1 Series { get; set; }

        /// <summary>
        ///     Deprecated field assuring backward compatibility with core.v1 Event type
        /// </summary>
        [JsonProperty("deprecatedCount")]
        [YamlMember(Alias = "deprecatedCount")]
        public int DeprecatedCount { get; set; }
    }
}
