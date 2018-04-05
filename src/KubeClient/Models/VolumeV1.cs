using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Volume represents a named volume in a pod that may be accessed by any container in the pod.
    /// </summary>
    public partial class VolumeV1
    {
        /// <summary>
        ///     RBD represents a Rados Block Device mount on the host that shares a pod's lifetime. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md
        /// </summary>
        [JsonProperty("rbd")]
        public RBDVolumeSourceV1 Rbd { get; set; }

        /// <summary>
        ///     Glusterfs represents a Glusterfs mount on the host that shares a pod's lifetime. More info: https://releases.k8s.io/HEAD/examples/volumes/glusterfs/README.md
        /// </summary>
        [JsonProperty("glusterfs")]
        public GlusterfsVolumeSourceV1 Glusterfs { get; set; }

        /// <summary>
        ///     AWSElasticBlockStore represents an AWS Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes#awselasticblockstore
        /// </summary>
        [JsonProperty("awsElasticBlockStore")]
        public AWSElasticBlockStoreVolumeSourceV1 AwsElasticBlockStore { get; set; }

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
        ///     Secret represents a secret that should populate this volume. More info: https://kubernetes.io/docs/concepts/storage/volumes#secret
        /// </summary>
        [JsonProperty("secret")]
        public SecretVolumeSourceV1 Secret { get; set; }

        /// <summary>
        ///     EmptyDir represents a temporary directory that shares a pod's lifetime. More info: https://kubernetes.io/docs/concepts/storage/volumes#emptydir
        /// </summary>
        [JsonProperty("emptyDir")]
        public EmptyDirVolumeSourceV1 EmptyDir { get; set; }

        /// <summary>
        ///     GCEPersistentDisk represents a GCE Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
        /// </summary>
        [JsonProperty("gcePersistentDisk")]
        public GCEPersistentDiskVolumeSourceV1 GcePersistentDisk { get; set; }

        /// <summary>
        ///     ScaleIO represents a ScaleIO persistent volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [JsonProperty("scaleIO")]
        public ScaleIOVolumeSourceV1 ScaleIO { get; set; }

        /// <summary>
        ///     Items for all in one resources secrets, configmaps, and downward API
        /// </summary>
        [JsonProperty("projected")]
        public ProjectedVolumeSourceV1 Projected { get; set; }

        /// <summary>
        ///     StorageOS represents a StorageOS volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [JsonProperty("storageos")]
        public StorageOSVolumeSourceV1 Storageos { get; set; }

        /// <summary>
        ///     Volume's name. Must be a DNS_LABEL and unique within the pod. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     GitRepo represents a git repository at a particular revision.
        /// </summary>
        [JsonProperty("gitRepo")]
        public GitRepoVolumeSourceV1 GitRepo { get; set; }

        /// <summary>
        ///     ConfigMap represents a configMap that should populate this volume
        /// </summary>
        [JsonProperty("configMap")]
        public ConfigMapVolumeSourceV1 ConfigMap { get; set; }

        /// <summary>
        ///     AzureFile represents an Azure File Service mount on the host and bind mount to the pod.
        /// </summary>
        [JsonProperty("azureFile")]
        public AzureFileVolumeSourceV1 AzureFile { get; set; }

        /// <summary>
        ///     CephFS represents a Ceph FS mount on the host that shares a pod's lifetime
        /// </summary>
        [JsonProperty("cephfs")]
        public CephFSVolumeSourceV1 Cephfs { get; set; }

        /// <summary>
        ///     Cinder represents a cinder volume attached and mounted on kubelets host machine More info: https://releases.k8s.io/HEAD/examples/mysql-cinder-pd/README.md
        /// </summary>
        [JsonProperty("cinder")]
        public CinderVolumeSourceV1 Cinder { get; set; }

        /// <summary>
        ///     FC represents a Fibre Channel resource that is attached to a kubelet's host machine and then exposed to the pod.
        /// </summary>
        [JsonProperty("fc")]
        public FCVolumeSourceV1 Fc { get; set; }

        /// <summary>
        ///     HostPath represents a pre-existing file or directory on the host machine that is directly exposed to the container. This is generally used for system agents or other privileged things that are allowed to see the host machine. Most containers will NOT need this. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
        /// </summary>
        [JsonProperty("hostPath")]
        public HostPathVolumeSourceV1 HostPath { get; set; }

        /// <summary>
        ///     PhotonPersistentDisk represents a PhotonController persistent disk attached and mounted on kubelets host machine
        /// </summary>
        [JsonProperty("photonPersistentDisk")]
        public PhotonPersistentDiskVolumeSourceV1 PhotonPersistentDisk { get; set; }

        /// <summary>
        ///     Flocker represents a Flocker volume attached to a kubelet's host machine. This depends on the Flocker control service being running
        /// </summary>
        [JsonProperty("flocker")]
        public FlockerVolumeSourceV1 Flocker { get; set; }

        /// <summary>
        ///     Quobyte represents a Quobyte mount on the host that shares a pod's lifetime
        /// </summary>
        [JsonProperty("quobyte")]
        public QuobyteVolumeSourceV1 Quobyte { get; set; }

        /// <summary>
        ///     DownwardAPI represents downward API about the pod that should populate this volume
        /// </summary>
        [JsonProperty("downwardAPI")]
        public DownwardAPIVolumeSourceV1 DownwardAPI { get; set; }

        /// <summary>
        ///     ISCSI represents an ISCSI Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://releases.k8s.io/HEAD/examples/volumes/iscsi/README.md
        /// </summary>
        [JsonProperty("iscsi")]
        public ISCSIVolumeSourceV1 Iscsi { get; set; }

        /// <summary>
        ///     AzureDisk represents an Azure Data Disk mount on the host and bind mount to the pod.
        /// </summary>
        [JsonProperty("azureDisk")]
        public AzureDiskVolumeSourceV1 AzureDisk { get; set; }

        /// <summary>
        ///     VsphereVolume represents a vSphere volume attached and mounted on kubelets host machine
        /// </summary>
        [JsonProperty("vsphereVolume")]
        public VsphereVirtualDiskVolumeSourceV1 VsphereVolume { get; set; }

        /// <summary>
        ///     PersistentVolumeClaimVolumeSource represents a reference to a PersistentVolumeClaim in the same namespace. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [JsonProperty("persistentVolumeClaim")]
        public PersistentVolumeClaimVolumeSourceV1 PersistentVolumeClaim { get; set; }

        /// <summary>
        ///     NFS represents an NFS mount on the host that shares a pod's lifetime More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [JsonProperty("nfs")]
        public NFSVolumeSourceV1 Nfs { get; set; }
    }
}
