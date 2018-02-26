using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeSpec is the specification of a persistent volume.
    /// </summary>
    [KubeObject("PersistentVolumeSpec", "v1")]
    public class PersistentVolumeSpecV1
    {
        /// <summary>
        ///     ScaleIO represents a ScaleIO persistent volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [JsonProperty("scaleIO")]
        public ScaleIOVolumeSourceV1 ScaleIO { get; set; }

        /// <summary>
        ///     FC represents a Fibre Channel resource that is attached to a kubelet's host machine and then exposed to the pod.
        /// </summary>
        [JsonProperty("fc")]
        public FCVolumeSourceV1 Fc { get; set; }

        /// <summary>
        ///     RBD represents a Rados Block Device mount on the host that shares a pod's lifetime. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md
        /// </summary>
        [JsonProperty("rbd")]
        public RBDVolumeSourceV1 Rbd { get; set; }

        /// <summary>
        ///     AWSElasticBlockStore represents an AWS Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes#awselasticblockstore
        /// </summary>
        [JsonProperty("awsElasticBlockStore")]
        public AWSElasticBlockStoreVolumeSourceV1 AwsElasticBlockStore { get; set; }

        /// <summary>
        ///     AzureFile represents an Azure File Service mount on the host and bind mount to the pod.
        /// </summary>
        [JsonProperty("azureFile")]
        public AzureFileVolumeSourceV1 AzureFile { get; set; }

        /// <summary>
        ///     FlexVolume represents a generic volume resource that is provisioned/attached using an exec based plugin. This is an alpha feature and may change in future.
        /// </summary>
        [JsonProperty("flexVolume")]
        public FlexVolumeSourceV1 FlexVolume { get; set; }

        /// <summary>
        ///     PortworxVolume represents a portworx volume attached and mounted on kubelets host machine
        /// </summary>
        [JsonProperty("portworxVolume")]
        public PortworxVolumeSourceV1 PortworxVolume { get; set; }

        /// <summary>
        ///     Quobyte represents a Quobyte mount on the host that shares a pod's lifetime
        /// </summary>
        [JsonProperty("quobyte")]
        public QuobyteVolumeSourceV1 Quobyte { get; set; }

        /// <summary>
        ///     Name of StorageClass to which this persistent volume belongs. Empty value means that this volume does not belong to any StorageClass.
        /// </summary>
        [JsonProperty("storageClassName")]
        public string StorageClassName { get; set; }

        /// <summary>
        ///     VsphereVolume represents a vSphere volume attached and mounted on kubelets host machine
        /// </summary>
        [JsonProperty("vsphereVolume")]
        public VsphereVirtualDiskVolumeSourceV1 VsphereVolume { get; set; }

        /// <summary>
        ///     ClaimRef is part of a bi-directional binding between PersistentVolume and PersistentVolumeClaim. Expected to be non-nil when bound. claim.VolumeName is the authoritative bind between PV and PVC. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#binding
        /// </summary>
        [JsonProperty("claimRef")]
        public ObjectReferenceV1 ClaimRef { get; set; }

        /// <summary>
        ///     HostPath represents a directory on the host. Provisioned by a developer or tester. This is useful for single-node development and testing only! On-host storage is not supported in any way and WILL NOT WORK in a multi-node cluster. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
        /// </summary>
        [JsonProperty("hostPath")]
        public HostPathVolumeSourceV1 HostPath { get; set; }

        /// <summary>
        ///     ISCSI represents an ISCSI Disk resource that is attached to a kubelet's host machine and then exposed to the pod. Provisioned by an admin.
        /// </summary>
        [JsonProperty("iscsi")]
        public ISCSIVolumeSourceV1 Iscsi { get; set; }

        /// <summary>
        ///     AzureDisk represents an Azure Data Disk mount on the host and bind mount to the pod.
        /// </summary>
        [JsonProperty("azureDisk")]
        public AzureDiskVolumeSourceV1 AzureDisk { get; set; }

        /// <summary>
        ///     GCEPersistentDisk represents a GCE Disk resource that is attached to a kubelet's host machine and then exposed to the pod. Provisioned by an admin. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
        /// </summary>
        [JsonProperty("gcePersistentDisk")]
        public GCEPersistentDiskVolumeSourceV1 GcePersistentDisk { get; set; }

        /// <summary>
        ///     PhotonPersistentDisk represents a PhotonController persistent disk attached and mounted on kubelets host machine
        /// </summary>
        [JsonProperty("photonPersistentDisk")]
        public PhotonPersistentDiskVolumeSourceV1 PhotonPersistentDisk { get; set; }

        /// <summary>
        ///     Local represents directly-attached storage with node affinity
        /// </summary>
        [JsonProperty("local")]
        public LocalVolumeSourceV1 Local { get; set; }

        /// <summary>
        ///     Cinder represents a cinder volume attached and mounted on kubelets host machine More info: https://releases.k8s.io/HEAD/examples/mysql-cinder-pd/README.md
        /// </summary>
        [JsonProperty("cinder")]
        public CinderVolumeSourceV1 Cinder { get; set; }

        /// <summary>
        ///     Flocker represents a Flocker volume attached to a kubelet's host machine and exposed to the pod for its usage. This depends on the Flocker control service being running
        /// </summary>
        [JsonProperty("flocker")]
        public FlockerVolumeSourceV1 Flocker { get; set; }

        /// <summary>
        ///     AccessModes contains all ways the volume can be mounted. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#access-modes
        /// </summary>
        [JsonProperty("accessModes", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AccessModes { get; set; } = new List<string>();

        /// <summary>
        ///     CephFS represents a Ceph FS mount on the host that shares a pod's lifetime
        /// </summary>
        [JsonProperty("cephfs")]
        public CephFSVolumeSourceV1 Cephfs { get; set; }

        /// <summary>
        ///     Glusterfs represents a Glusterfs volume that is attached to a host and exposed to the pod. Provisioned by an admin. More info: https://releases.k8s.io/HEAD/examples/volumes/glusterfs/README.md
        /// </summary>
        [JsonProperty("glusterfs")]
        public GlusterfsVolumeSourceV1 Glusterfs { get; set; }

        /// <summary>
        ///     NFS represents an NFS mount on the host. Provisioned by an admin. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [JsonProperty("nfs")]
        public NFSVolumeSourceV1 Nfs { get; set; }

        /// <summary>
        ///     StorageOS represents a StorageOS volume that is attached to the kubelet's host machine and mounted into the pod More info: https://releases.k8s.io/HEAD/examples/volumes/storageos/README.md
        /// </summary>
        [JsonProperty("storageos")]
        public StorageOSPersistentVolumeSourceV1 Storageos { get; set; }

        /// <summary>
        ///     A description of the persistent volume's resources and capacity. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#capacity
        /// </summary>
        [JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Capacity { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     What happens to a persistent volume when released from its claim. Valid options are Retain (default) and Recycle. Recycling must be supported by the volume plugin underlying this persistent volume. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#reclaiming
        /// </summary>
        [JsonProperty("persistentVolumeReclaimPolicy")]
        public string PersistentVolumeReclaimPolicy { get; set; }
    }
}
