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
        ///     downwardAPI represents downward API about the pod that should populate this volume
        /// </summary>
        [YamlMember(Alias = "downwardAPI")]
        [JsonProperty("downwardAPI", NullValueHandling = NullValueHandling.Ignore)]
        public DownwardAPIVolumeSourceV1 DownwardAPI { get; set; }

        /// <summary>
        ///     scaleIO represents a ScaleIO persistent volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [YamlMember(Alias = "scaleIO")]
        [JsonProperty("scaleIO", NullValueHandling = NullValueHandling.Ignore)]
        public ScaleIOVolumeSourceV1 ScaleIO { get; set; }

        /// <summary>
        ///     fc represents a Fibre Channel resource that is attached to a kubelet's host machine and then exposed to the pod.
        /// </summary>
        [YamlMember(Alias = "fc")]
        [JsonProperty("fc", NullValueHandling = NullValueHandling.Ignore)]
        public FCVolumeSourceV1 Fc { get; set; }

        /// <summary>
        ///     projected items for all in one resources secrets, configmaps, and downward API
        /// </summary>
        [YamlMember(Alias = "projected")]
        [JsonProperty("projected", NullValueHandling = NullValueHandling.Ignore)]
        public ProjectedVolumeSourceV1 Projected { get; set; }

        /// <summary>
        ///     rbd represents a Rados Block Device mount on the host that shares a pod's lifetime. More info: https://examples.k8s.io/volumes/rbd/README.md
        /// </summary>
        [YamlMember(Alias = "rbd")]
        [JsonProperty("rbd", NullValueHandling = NullValueHandling.Ignore)]
        public RBDVolumeSourceV1 Rbd { get; set; }

        /// <summary>
        ///     awsElasticBlockStore represents an AWS Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes#awselasticblockstore
        /// </summary>
        [YamlMember(Alias = "awsElasticBlockStore")]
        [JsonProperty("awsElasticBlockStore", NullValueHandling = NullValueHandling.Ignore)]
        public AWSElasticBlockStoreVolumeSourceV1 AwsElasticBlockStore { get; set; }

        /// <summary>
        ///     azureFile represents an Azure File Service mount on the host and bind mount to the pod.
        /// </summary>
        [YamlMember(Alias = "azureFile")]
        [JsonProperty("azureFile", NullValueHandling = NullValueHandling.Ignore)]
        public AzureFileVolumeSourceV1 AzureFile { get; set; }

        /// <summary>
        ///     flexVolume represents a generic volume resource that is provisioned/attached using an exec based plugin.
        /// </summary>
        [YamlMember(Alias = "flexVolume")]
        [JsonProperty("flexVolume", NullValueHandling = NullValueHandling.Ignore)]
        public FlexVolumeSourceV1 FlexVolume { get; set; }

        /// <summary>
        ///     image represents an OCI object (a container image or artifact) pulled and mounted on the kubelet's host machine. The volume is resolved at pod startup depending on which PullPolicy value is provided:
        ///     
        ///     - Always: the kubelet always attempts to pull the reference. Container creation will fail If the pull fails. - Never: the kubelet never pulls the reference and only uses a local image or artifact. Container creation will fail if the reference isn't present. - IfNotPresent: the kubelet pulls if the reference isn't already present on disk. Container creation will fail if the reference isn't present and the pull fails.
        ///     
        ///     The volume gets re-resolved if the pod gets deleted and recreated, which means that new remote content will become available on pod recreation. A failure to resolve or pull the image during pod startup will block containers from starting and may add significant latency. Failures will be retried using normal volume backoff and will be reported on the pod reason and message. The types of objects that may be mounted by this volume are defined by the container runtime implementation on a host machine and at minimum must include all valid types supported by the container image field. The OCI object gets mounted in a single directory (spec.containers[*].volumeMounts.mountPath) by merging the manifest layers in the same way as for container images. The volume will be mounted read-only (ro) and non-executable files (noexec). Sub path mounts for containers are not supported (spec.containers[*].volumeMounts.subpath). The field spec.securityContext.fsGroupChangePolicy has no effect on this volume type.
        /// </summary>
        [YamlMember(Alias = "image")]
        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public ImageVolumeSourceV1 Image { get; set; }

        /// <summary>
        ///     name of the volume. Must be a DNS_LABEL and unique within the pod. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     portworxVolume represents a portworx volume attached and mounted on kubelets host machine
        /// </summary>
        [YamlMember(Alias = "portworxVolume")]
        [JsonProperty("portworxVolume", NullValueHandling = NullValueHandling.Ignore)]
        public PortworxVolumeSourceV1 PortworxVolume { get; set; }

        /// <summary>
        ///     quobyte represents a Quobyte mount on the host that shares a pod's lifetime
        /// </summary>
        [YamlMember(Alias = "quobyte")]
        [JsonProperty("quobyte", NullValueHandling = NullValueHandling.Ignore)]
        public QuobyteVolumeSourceV1 Quobyte { get; set; }

        /// <summary>
        ///     vsphereVolume represents a vSphere volume attached and mounted on kubelets host machine
        /// </summary>
        [YamlMember(Alias = "vsphereVolume")]
        [JsonProperty("vsphereVolume", NullValueHandling = NullValueHandling.Ignore)]
        public VsphereVirtualDiskVolumeSourceV1 VsphereVolume { get; set; }

        /// <summary>
        ///     hostPath represents a pre-existing file or directory on the host machine that is directly exposed to the container. This is generally used for system agents or other privileged things that are allowed to see the host machine. Most containers will NOT need this. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
        /// </summary>
        [YamlMember(Alias = "hostPath")]
        [JsonProperty("hostPath", NullValueHandling = NullValueHandling.Ignore)]
        public HostPathVolumeSourceV1 HostPath { get; set; }

        /// <summary>
        ///     csi (Container Storage Interface) represents ephemeral storage that is handled by certain external CSI drivers (Beta feature).
        /// </summary>
        [YamlMember(Alias = "csi")]
        [JsonProperty("csi", NullValueHandling = NullValueHandling.Ignore)]
        public CSIVolumeSourceV1 Csi { get; set; }

        /// <summary>
        ///     iscsi represents an ISCSI Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://examples.k8s.io/volumes/iscsi/README.md
        /// </summary>
        [YamlMember(Alias = "iscsi")]
        [JsonProperty("iscsi", NullValueHandling = NullValueHandling.Ignore)]
        public ISCSIVolumeSourceV1 Iscsi { get; set; }

        /// <summary>
        ///     azureDisk represents an Azure Data Disk mount on the host and bind mount to the pod.
        /// </summary>
        [YamlMember(Alias = "azureDisk")]
        [JsonProperty("azureDisk", NullValueHandling = NullValueHandling.Ignore)]
        public AzureDiskVolumeSourceV1 AzureDisk { get; set; }

        /// <summary>
        ///     gcePersistentDisk represents a GCE Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
        /// </summary>
        [YamlMember(Alias = "gcePersistentDisk")]
        [JsonProperty("gcePersistentDisk", NullValueHandling = NullValueHandling.Ignore)]
        public GCEPersistentDiskVolumeSourceV1 GcePersistentDisk { get; set; }

        /// <summary>
        ///     photonPersistentDisk represents a PhotonController persistent disk attached and mounted on kubelets host machine
        /// </summary>
        [YamlMember(Alias = "photonPersistentDisk")]
        [JsonProperty("photonPersistentDisk", NullValueHandling = NullValueHandling.Ignore)]
        public PhotonPersistentDiskVolumeSourceV1 PhotonPersistentDisk { get; set; }

        /// <summary>
        ///     ephemeral represents a volume that is handled by a cluster storage driver. The volume's lifecycle is tied to the pod that defines it - it will be created before the pod starts, and deleted when the pod is removed.
        ///     
        ///     Use this if: a) the volume is only needed while the pod runs, b) features of normal volumes like restoring from snapshot or capacity
        ///        tracking are needed,
        ///     c) the storage driver is specified through a storage class, and d) the storage driver supports dynamic volume provisioning through
        ///        a PersistentVolumeClaim (see EphemeralVolumeSource for more
        ///        information on the connection between this volume type
        ///        and PersistentVolumeClaim).
        ///     
        ///     Use PersistentVolumeClaim or one of the vendor-specific APIs for volumes that persist for longer than the lifecycle of an individual pod.
        ///     
        ///     Use CSI for light-weight local ephemeral volumes if the CSI driver is meant to be used that way - see the documentation of the driver for more information.
        ///     
        ///     A pod can use both types of ephemeral volumes and persistent volumes at the same time.
        /// </summary>
        [YamlMember(Alias = "ephemeral")]
        [JsonProperty("ephemeral", NullValueHandling = NullValueHandling.Ignore)]
        public EphemeralVolumeSourceV1 Ephemeral { get; set; }

        /// <summary>
        ///     persistentVolumeClaimVolumeSource represents a reference to a PersistentVolumeClaim in the same namespace. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [YamlMember(Alias = "persistentVolumeClaim")]
        [JsonProperty("persistentVolumeClaim", NullValueHandling = NullValueHandling.Ignore)]
        public PersistentVolumeClaimVolumeSourceV1 PersistentVolumeClaim { get; set; }

        /// <summary>
        ///     gitRepo represents a git repository at a particular revision. DEPRECATED: GitRepo is deprecated. To provision a container with a git repo, mount an EmptyDir into an InitContainer that clones the repo using git, then mount the EmptyDir into the Pod's container.
        /// </summary>
        [YamlMember(Alias = "gitRepo")]
        [JsonProperty("gitRepo", NullValueHandling = NullValueHandling.Ignore)]
        public GitRepoVolumeSourceV1 GitRepo { get; set; }

        /// <summary>
        ///     configMap represents a configMap that should populate this volume
        /// </summary>
        [YamlMember(Alias = "configMap")]
        [JsonProperty("configMap", NullValueHandling = NullValueHandling.Ignore)]
        public ConfigMapVolumeSourceV1 ConfigMap { get; set; }

        /// <summary>
        ///     cinder represents a cinder volume attached and mounted on kubelets host machine. More info: https://examples.k8s.io/mysql-cinder-pd/README.md
        /// </summary>
        [YamlMember(Alias = "cinder")]
        [JsonProperty("cinder", NullValueHandling = NullValueHandling.Ignore)]
        public CinderVolumeSourceV1 Cinder { get; set; }

        /// <summary>
        ///     emptyDir represents a temporary directory that shares a pod's lifetime. More info: https://kubernetes.io/docs/concepts/storage/volumes#emptydir
        /// </summary>
        [YamlMember(Alias = "emptyDir")]
        [JsonProperty("emptyDir", NullValueHandling = NullValueHandling.Ignore)]
        public EmptyDirVolumeSourceV1 EmptyDir { get; set; }

        /// <summary>
        ///     flocker represents a Flocker volume attached to a kubelet's host machine. This depends on the Flocker control service being running
        /// </summary>
        [YamlMember(Alias = "flocker")]
        [JsonProperty("flocker", NullValueHandling = NullValueHandling.Ignore)]
        public FlockerVolumeSourceV1 Flocker { get; set; }

        /// <summary>
        ///     cephFS represents a Ceph FS mount on the host that shares a pod's lifetime
        /// </summary>
        [YamlMember(Alias = "cephfs")]
        [JsonProperty("cephfs", NullValueHandling = NullValueHandling.Ignore)]
        public CephFSVolumeSourceV1 Cephfs { get; set; }

        /// <summary>
        ///     glusterfs represents a Glusterfs mount on the host that shares a pod's lifetime. More info: https://examples.k8s.io/volumes/glusterfs/README.md
        /// </summary>
        [YamlMember(Alias = "glusterfs")]
        [JsonProperty("glusterfs", NullValueHandling = NullValueHandling.Ignore)]
        public GlusterfsVolumeSourceV1 Glusterfs { get; set; }

        /// <summary>
        ///     nfs represents an NFS mount on the host that shares a pod's lifetime More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [YamlMember(Alias = "nfs")]
        [JsonProperty("nfs", NullValueHandling = NullValueHandling.Ignore)]
        public NFSVolumeSourceV1 Nfs { get; set; }

        /// <summary>
        ///     storageOS represents a StorageOS volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [YamlMember(Alias = "storageos")]
        [JsonProperty("storageos", NullValueHandling = NullValueHandling.Ignore)]
        public StorageOSVolumeSourceV1 Storageos { get; set; }

        /// <summary>
        ///     secret represents a secret that should populate this volume. More info: https://kubernetes.io/docs/concepts/storage/volumes#secret
        /// </summary>
        [YamlMember(Alias = "secret")]
        [JsonProperty("secret", NullValueHandling = NullValueHandling.Ignore)]
        public SecretVolumeSourceV1 Secret { get; set; }
    }
}
