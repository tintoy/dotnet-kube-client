using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Event is a report of an event somewhere in the cluster.
    /// </summary>
    [KubeObject("Event", "v1")]
    [KubeApi(KubeAction.List, "api/v1/events")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/events")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/events")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/events")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/events/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/events/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/events/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/events/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/events")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/events")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/events/{name}")]
    public partial class EventV1 : KubeResourceV1
    {
        /// <summary>
        ///     Optional secondary object for more complex actions.
        /// </summary>
        [YamlMember(Alias = "related")]
        [JsonProperty("related", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectReferenceV1 Related { get; set; }

        /// <summary>
        ///     Time when this Event was first observed.
        /// </summary>
        [YamlMember(Alias = "eventTime")]
        [JsonProperty("eventTime", NullValueHandling = NullValueHandling.Ignore)]
        public MicroTimeV1 EventTime { get; set; }

        /// <summary>
        ///     A human-readable description of the status of this operation.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     ID of the controller instance, e.g. `kubelet-xyzf`.
        /// </summary>
        [YamlMember(Alias = "reportingInstance")]
        [JsonProperty("reportingInstance", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingInstance { get; set; }

        /// <summary>
        ///     The component reporting this event. Should be a short machine understandable string.
        /// </summary>
        [YamlMember(Alias = "source")]
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public EventSourceV1 Source { get; set; }

        /// <summary>
        ///     Type of this event (Normal, Warning), new types could be added in the future
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        ///     What action was taken/failed regarding to the Regarding object.
        /// </summary>
        [YamlMember(Alias = "action")]
        [JsonProperty("action", NullValueHandling = NullValueHandling.Ignore)]
        public string Action { get; set; }

        /// <summary>
        ///     This should be a short, machine understandable string that gives the reason for the transition into the object's current status.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     The time at which the event was first recorded. (Time of server receipt is in TypeMeta.)
        /// </summary>
        [YamlMember(Alias = "firstTimestamp")]
        [JsonProperty("firstTimestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? FirstTimestamp { get; set; }

        /// <summary>
        ///     The time at which the most recent occurrence of this event was recorded.
        /// </summary>
        [YamlMember(Alias = "lastTimestamp")]
        [JsonProperty("lastTimestamp", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastTimestamp { get; set; }

        /// <summary>
        ///     Data about the Event series this event represents or nil if it's a singleton Event.
        /// </summary>
        [YamlMember(Alias = "series")]
        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public EventSeriesV1 Series { get; set; }

        /// <summary>
        ///     The number of times this event has occurred.
        /// </summary>
        [YamlMember(Alias = "count")]
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int? Count { get; set; }

        /// <summary>
        ///     The object that this event is about.
        /// </summary>
        [YamlMember(Alias = "involvedObject")]
        [JsonProperty("involvedObject", NullValueHandling = NullValueHandling.Include)]
        public ObjectReferenceV1 InvolvedObject { get; set; }

        /// <summary>
        ///     Name of the controller that emitted this Event, e.g. `kubernetes.io/kubelet`.
        /// </summary>
        [YamlMember(Alias = "reportingComponent")]
        [JsonProperty("reportingComponent", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingComponent { get; set; }
    }
}
