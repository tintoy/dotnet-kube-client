using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Volume represents a named volume in a pod that may be accessed by any container in the pod.
    /// </summary>
    public partial class VolumeV1
    {
        /// <summary>
        ///     DownwardAPI represents downward API about the pod that should populate this volume
        /// </summary>
        [YamlMember(Alias = "downwardAPI")]
        [JsonProperty("downwardAPI", NullValueHandling = NullValueHandling.Ignore)]
        public DownwardAPIVolumeSourceV1 DownwardAPI { get; set; }

        /// <summary>
        ///     ScaleIO represents a ScaleIO persistent volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [YamlMember(Alias = "scaleIO")]
        [JsonProperty("scaleIO", NullValueHandling = NullValueHandling.Ignore)]
        public ScaleIOVolumeSourceV1 ScaleIO { get; set; }

        /// <summary>
        ///     FC represents a Fibre Channel resource that is attached to a kubelet's host machine and then exposed to the pod.
        /// </summary>
        [YamlMember(Alias = "fc")]
        [JsonProperty("fc", NullValueHandling = NullValueHandling.Ignore)]
        public FCVolumeSourceV1 Fc { get; set; }

        /// <summary>
        ///     Items for all in one resources secrets, configmaps, and downward API
        /// </summary>
        [YamlMember(Alias = "projected")]
        [JsonProperty("projected", NullValueHandling = NullValueHandling.Ignore)]
        public ProjectedVolumeSourceV1 Projected { get; set; }

        /// <summary>
        ///     RBD represents a Rados Block Device mount on the host that shares a pod's lifetime. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md
        /// </summary>
        [YamlMember(Alias = "rbd")]
        [JsonProperty("rbd", NullValueHandling = NullValueHandling.Ignore)]
        public RBDVolumeSourceV1 Rbd { get; set; }

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
        public AzureFileVolumeSourceV1 AzureFile { get; set; }

        /// <summary>
        ///     FlexVolume represents a generic volume resource that is provisioned/attached using an exec based plugin.
        /// </summary>
        [YamlMember(Alias = "flexVolume")]
        [JsonProperty("flexVolume", NullValueHandling = NullValueHandling.Ignore)]
        public FlexVolumeSourceV1 FlexVolume { get; set; }

        /// <summary>
        ///     Volume's name. Must be a DNS_LABEL and unique within the pod. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

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
        ///     VsphereVolume represents a vSphere volume attached and mounted on kubelets host machine
        /// </summary>
        [YamlMember(Alias = "vsphereVolume")]
        [JsonProperty("vsphereVolume", NullValueHandling = NullValueHandling.Ignore)]
        public VsphereVirtualDiskVolumeSourceV1 VsphereVolume { get; set; }

        /// <summary>
        ///     HostPath represents a pre-existing file or directory on the host machine that is directly exposed to the container. This is generally used for system agents or other privileged things that are allowed to see the host machine. Most containers will NOT need this. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
        /// </summary>
        [YamlMember(Alias = "hostPath")]
        [JsonProperty("hostPath", NullValueHandling = NullValueHandling.Ignore)]
        public HostPathVolumeSourceV1 HostPath { get; set; }

        /// <summary>
        ///     ISCSI represents an ISCSI Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://releases.k8s.io/HEAD/examples/volumes/iscsi/README.md
        /// </summary>
        [YamlMember(Alias = "iscsi")]
        [JsonProperty("iscsi", NullValueHandling = NullValueHandling.Ignore)]
        public ISCSIVolumeSourceV1 Iscsi { get; set; }

        /// <summary>
        ///     AzureDisk represents an Azure Data Disk mount on the host and bind mount to the pod.
        /// </summary>
        [YamlMember(Alias = "azureDisk")]
        [JsonProperty("azureDisk", NullValueHandling = NullValueHandling.Ignore)]
        public AzureDiskVolumeSourceV1 AzureDisk { get; set; }

        /// <summary>
        ///     GCEPersistentDisk represents a GCE Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
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
        ///     PersistentVolumeClaimVolumeSource represents a reference to a PersistentVolumeClaim in the same namespace. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [YamlMember(Alias = "persistentVolumeClaim")]
        [JsonProperty("persistentVolumeClaim", NullValueHandling = NullValueHandling.Ignore)]
        public PersistentVolumeClaimVolumeSourceV1 PersistentVolumeClaim { get; set; }

        /// <summary>
        ///     GitRepo represents a git repository at a particular revision. DEPRECATED: GitRepo is deprecated. To provision a container with a git repo, mount an EmptyDir into an InitContainer that clones the repo using git, then mount the EmptyDir into the Pod's container.
        /// </summary>
        [YamlMember(Alias = "gitRepo")]
        [JsonProperty("gitRepo", NullValueHandling = NullValueHandling.Ignore)]
        public GitRepoVolumeSourceV1 GitRepo { get; set; }

        /// <summary>
        ///     ConfigMap represents a configMap that should populate this volume
        /// </summary>
        [YamlMember(Alias = "configMap")]
        [JsonProperty("configMap", NullValueHandling = NullValueHandling.Ignore)]
        public ConfigMapVolumeSourceV1 ConfigMap { get; set; }

        /// <summary>
        ///     Cinder represents a cinder volume attached and mounted on kubelets host machine More info: https://releases.k8s.io/HEAD/examples/mysql-cinder-pd/README.md
        /// </summary>
        [YamlMember(Alias = "cinder")]
        [JsonProperty("cinder", NullValueHandling = NullValueHandling.Ignore)]
        public CinderVolumeSourceV1 Cinder { get; set; }

        /// <summary>
        ///     EmptyDir represents a temporary directory that shares a pod's lifetime. More info: https://kubernetes.io/docs/concepts/storage/volumes#emptydir
        /// </summary>
        [YamlMember(Alias = "emptyDir")]
        [JsonProperty("emptyDir", NullValueHandling = NullValueHandling.Ignore)]
        public EmptyDirVolumeSourceV1 EmptyDir { get; set; }

        /// <summary>
        ///     Flocker represents a Flocker volume attached to a kubelet's host machine. This depends on the Flocker control service being running
        /// </summary>
        [YamlMember(Alias = "flocker")]
        [JsonProperty("flocker", NullValueHandling = NullValueHandling.Ignore)]
        public FlockerVolumeSourceV1 Flocker { get; set; }

        /// <summary>
        ///     CephFS represents a Ceph FS mount on the host that shares a pod's lifetime
        /// </summary>
        [YamlMember(Alias = "cephfs")]
        [JsonProperty("cephfs", NullValueHandling = NullValueHandling.Ignore)]
        public CephFSVolumeSourceV1 Cephfs { get; set; }

        /// <summary>
        ///     Glusterfs represents a Glusterfs mount on the host that shares a pod's lifetime. More info: https://releases.k8s.io/HEAD/examples/volumes/glusterfs/README.md
        /// </summary>
        [YamlMember(Alias = "glusterfs")]
        [JsonProperty("glusterfs", NullValueHandling = NullValueHandling.Ignore)]
        public GlusterfsVolumeSourceV1 Glusterfs { get; set; }

        /// <summary>
        ///     NFS represents an NFS mount on the host that shares a pod's lifetime More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [YamlMember(Alias = "nfs")]
        [JsonProperty("nfs", NullValueHandling = NullValueHandling.Ignore)]
        public NFSVolumeSourceV1 Nfs { get; set; }

        /// <summary>
        ///     StorageOS represents a StorageOS volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [YamlMember(Alias = "storageos")]
        [JsonProperty("storageos", NullValueHandling = NullValueHandling.Ignore)]
        public StorageOSVolumeSourceV1 Storageos { get; set; }

        /// <summary>
        ///     Secret represents a secret that should populate this volume. More info: https://kubernetes.io/docs/concepts/storage/volumes#secret
        /// </summary>
        [YamlMember(Alias = "secret")]
        [JsonProperty("secret", NullValueHandling = NullValueHandling.Ignore)]
        public SecretVolumeSourceV1 Secret { get; set; }
    }
}
