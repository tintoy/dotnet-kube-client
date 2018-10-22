using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeSpec is the specification of a persistent volume.
    /// </summary>
    public partial class PersistentVolumeSpecV1
    {
        /// <summary>
        ///     ScaleIO represents a ScaleIO persistent volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [YamlMember(Alias = "scaleIO")]
        [JsonProperty("scaleIO", NullValueHandling = NullValueHandling.Ignore)]
        public ScaleIOPersistentVolumeSourceV1 ScaleIO { get; set; }

        /// <summary>
        ///     FC represents a Fibre Channel resource that is attached to a kubelet's host machine and then exposed to the pod.
        /// </summary>
        [YamlMember(Alias = "fc")]
        [JsonProperty("fc", NullValueHandling = NullValueHandling.Ignore)]
        public FCVolumeSourceV1 Fc { get; set; }

        /// <summary>
        ///     RBD represents a Rados Block Device mount on the host that shares a pod's lifetime. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md
        /// </summary>
        [YamlMember(Alias = "rbd")]
        [JsonProperty("rbd", NullValueHandling = NullValueHandling.Ignore)]
        public RBDPersistentVolumeSourceV1 Rbd { get; set; }

        /// <summary>
        ///     AWSElasticBlockStore represents an AWS Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes#awselasticblockstore
        /// </summary>
        [YamlMember(Alias = "awsElasticBlockStore")]
        [JsonProperty("awsElasticBlockStore", NullValueHandling = NullValueHandling.Ignore)]
        public AWSElasticBlockStoreVolumeSourceV1 AwsElasticBlockStore { get; set; }

        /// <summary>
        ///     AzureFile represents an Azure File Service mount on the host and bind mount to the pod.
        /// </summary>
        [YamlMember(Alias = "azureFile")]
        [JsonProperty("azureFile", NullValueHandling = NullValueHandling.Ignore)]
        public AzureFilePersistentVolumeSourceV1 AzureFile { get; set; }

        /// <summary>
        ///     FlexVolume represents a generic volume resource that is provisioned/attached using an exec based plugin.
        /// </summary>
        [YamlMember(Alias = "flexVolume")]
        [JsonProperty("flexVolume", NullValueHandling = NullValueHandling.Ignore)]
        public FlexPersistentVolumeSourceV1 FlexVolume { get; set; }

        /// <summary>
        ///     PortworxVolume represents a portworx volume attached and mounted on kubelets host machine
        /// </summary>
        [YamlMember(Alias = "portworxVolume")]
        [JsonProperty("portworxVolume", NullValueHandling = NullValueHandling.Ignore)]
        public PortworxVolumeSourceV1 PortworxVolume { get; set; }

        /// <summary>
        ///     Quobyte represents a Quobyte mount on the host that shares a pod's lifetime
        /// </summary>
        [YamlMember(Alias = "quobyte")]
        [JsonProperty("quobyte", NullValueHandling = NullValueHandling.Ignore)]
        public QuobyteVolumeSourceV1 Quobyte { get; set; }

        /// <summary>
        ///     Name of StorageClass to which this persistent volume belongs. Empty value means that this volume does not belong to any StorageClass.
        /// </summary>
        [YamlMember(Alias = "storageClassName")]
        [JsonProperty("storageClassName", NullValueHandling = NullValueHandling.Ignore)]
        public string StorageClassName { get; set; }

        /// <summary>
        ///     volumeMode defines if a volume is intended to be used with a formatted filesystem or to remain in raw block state. Value of Filesystem is implied when not included in spec. This is an alpha feature and may change in the future.
        /// </summary>
        [YamlMember(Alias = "volumeMode")]
        [JsonProperty("volumeMode", NullValueHandling = NullValueHandling.Ignore)]
        public string VolumeMode { get; set; }

        /// <summary>
        ///     VsphereVolume represents a vSphere volume attached and mounted on kubelets host machine
        /// </summary>
        [YamlMember(Alias = "vsphereVolume")]
        [JsonProperty("vsphereVolume", NullValueHandling = NullValueHandling.Ignore)]
        public VsphereVirtualDiskVolumeSourceV1 VsphereVolume { get; set; }

        /// <summary>
        ///     ClaimRef is part of a bi-directional binding between PersistentVolume and PersistentVolumeClaim. Expected to be non-nil when bound. claim.VolumeName is the authoritative bind between PV and PVC. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#binding
        /// </summary>
        [YamlMember(Alias = "claimRef")]
        [JsonProperty("claimRef", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectReferenceV1 ClaimRef { get; set; }

        /// <summary>
        ///     HostPath represents a directory on the host. Provisioned by a developer or tester. This is useful for single-node development and testing only! On-host storage is not supported in any way and WILL NOT WORK in a multi-node cluster. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
        /// </summary>
        [YamlMember(Alias = "hostPath")]
        [JsonProperty("hostPath", NullValueHandling = NullValueHandling.Ignore)]
        public HostPathVolumeSourceV1 HostPath { get; set; }

        /// <summary>
        ///     CSI represents storage that handled by an external CSI driver (Beta feature).
        /// </summary>
        [YamlMember(Alias = "csi")]
        [JsonProperty("csi", NullValueHandling = NullValueHandling.Ignore)]
        public CSIPersistentVolumeSourceV1 Csi { get; set; }

        /// <summary>
        ///     ISCSI represents an ISCSI Disk resource that is attached to a kubelet's host machine and then exposed to the pod. Provisioned by an admin.
        /// </summary>
        [YamlMember(Alias = "iscsi")]
        [JsonProperty("iscsi", NullValueHandling = NullValueHandling.Ignore)]
        public ISCSIPersistentVolumeSourceV1 Iscsi { get; set; }

        /// <summary>
        ///     AzureDisk represents an Azure Data Disk mount on the host and bind mount to the pod.
        /// </summary>
        [YamlMember(Alias = "azureDisk")]
        [JsonProperty("azureDisk", NullValueHandling = NullValueHandling.Ignore)]
        public AzureDiskVolumeSourceV1 AzureDisk { get; set; }

        /// <summary>
        ///     GCEPersistentDisk represents a GCE Disk resource that is attached to a kubelet's host machine and then exposed to the pod. Provisioned by an admin. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
        /// </summary>
        [YamlMember(Alias = "gcePersistentDisk")]
        [JsonProperty("gcePersistentDisk", NullValueHandling = NullValueHandling.Ignore)]
        public GCEPersistentDiskVolumeSourceV1 GcePersistentDisk { get; set; }

        /// <summary>
        ///     PhotonPersistentDisk represents a PhotonController persistent disk attached and mounted on kubelets host machine
        /// </summary>
        [YamlMember(Alias = "photonPersistentDisk")]
        [JsonProperty("photonPersistentDisk", NullValueHandling = NullValueHandling.Ignore)]
        public PhotonPersistentDiskVolumeSourceV1 PhotonPersistentDisk { get; set; }

        /// <summary>
        ///     Local represents directly-attached storage with node affinity
        /// </summary>
        [YamlMember(Alias = "local")]
        [JsonProperty("local", NullValueHandling = NullValueHandling.Ignore)]
        public LocalVolumeSourceV1 Local { get; set; }

        /// <summary>
        ///     Cinder represents a cinder volume attached and mounted on kubelets host machine More info: https://releases.k8s.io/HEAD/examples/mysql-cinder-pd/README.md
        /// </summary>
        [YamlMember(Alias = "cinder")]
        [JsonProperty("cinder", NullValueHandling = NullValueHandling.Ignore)]
        public CinderPersistentVolumeSourceV1 Cinder { get; set; }

        /// <summary>
        ///     Flocker represents a Flocker volume attached to a kubelet's host machine and exposed to the pod for its usage. This depends on the Flocker control service being running
        /// </summary>
        [YamlMember(Alias = "flocker")]
        [JsonProperty("flocker", NullValueHandling = NullValueHandling.Ignore)]
        public FlockerVolumeSourceV1 Flocker { get; set; }

        /// <summary>
        ///     AccessModes contains all ways the volume can be mounted. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#access-modes
        /// </summary>
        [YamlMember(Alias = "accessModes")]
        [JsonProperty("accessModes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> AccessModes { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="AccessModes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAccessModes() => AccessModes.Count > 0;

        /// <summary>
        ///     CephFS represents a Ceph FS mount on the host that shares a pod's lifetime
        /// </summary>
        [YamlMember(Alias = "cephfs")]
        [JsonProperty("cephfs", NullValueHandling = NullValueHandling.Ignore)]
        public CephFSPersistentVolumeSourceV1 Cephfs { get; set; }

        /// <summary>
        ///     Glusterfs represents a Glusterfs volume that is attached to a host and exposed to the pod. Provisioned by an admin. More info: https://releases.k8s.io/HEAD/examples/volumes/glusterfs/README.md
        /// </summary>
        [YamlMember(Alias = "glusterfs")]
        [JsonProperty("glusterfs", NullValueHandling = NullValueHandling.Ignore)]
        public GlusterfsVolumeSourceV1 Glusterfs { get; set; }

        /// <summary>
        ///     A list of mount options, e.g. ["ro", "soft"]. Not validated - mount will simply fail if one is invalid. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes/#mount-options
        /// </summary>
        [YamlMember(Alias = "mountOptions")]
        [JsonProperty("mountOptions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> MountOptions { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="MountOptions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMountOptions() => MountOptions.Count > 0;

        /// <summary>
        ///     NFS represents an NFS mount on the host. Provisioned by an admin. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [YamlMember(Alias = "nfs")]
        [JsonProperty("nfs", NullValueHandling = NullValueHandling.Ignore)]
        public NFSVolumeSourceV1 Nfs { get; set; }

        /// <summary>
        ///     StorageOS represents a StorageOS volume that is attached to the kubelet's host machine and mounted into the pod More info: https://releases.k8s.io/HEAD/examples/volumes/storageos/README.md
        /// </summary>
        [YamlMember(Alias = "storageos")]
        [JsonProperty("storageos", NullValueHandling = NullValueHandling.Ignore)]
        public StorageOSPersistentVolumeSourceV1 Storageos { get; set; }

        /// <summary>
        ///     A description of the persistent volume's resources and capacity. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#capacity
        /// </summary>
        [YamlMember(Alias = "capacity")]
        [JsonProperty("capacity", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Capacity { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Capacity"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeCapacity() => Capacity.Count > 0;

        /// <summary>
        ///     NodeAffinity defines constraints that limit what nodes this volume can be accessed from. This field influences the scheduling of pods that use this volume.
        /// </summary>
        [YamlMember(Alias = "nodeAffinity")]
        [JsonProperty("nodeAffinity", NullValueHandling = NullValueHandling.Ignore)]
        public VolumeNodeAffinityV1 NodeAffinity { get; set; }

        /// <summary>
        ///     What happens to a persistent volume when released from its claim. Valid options are Retain (default for manually created PersistentVolumes), Delete (default for dynamically provisioned PersistentVolumes), and Recycle (deprecated). Recycle must be supported by the volume plugin underlying this PersistentVolume. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#reclaiming
        /// </summary>
        [YamlMember(Alias = "persistentVolumeReclaimPolicy")]
        [JsonProperty("persistentVolumeReclaimPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string PersistentVolumeReclaimPolicy { get; set; }
    }
}
