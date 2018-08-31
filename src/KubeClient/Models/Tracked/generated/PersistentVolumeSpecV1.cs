using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PersistentVolumeSpec is the specification of a persistent volume.
    /// </summary>
    public partial class PersistentVolumeSpecV1 : Models.PersistentVolumeSpecV1
    {
        /// <summary>
        ///     ScaleIO represents a ScaleIO persistent volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [JsonProperty("scaleIO")]
        [YamlMember(Alias = "scaleIO")]
        public override Models.ScaleIOVolumeSourceV1 ScaleIO
        {
            get
            {
                return base.ScaleIO;
            }
            set
            {
                base.ScaleIO = value;

                __ModifiedProperties__.Add("ScaleIO");
            }
        }


        /// <summary>
        ///     FC represents a Fibre Channel resource that is attached to a kubelet's host machine and then exposed to the pod.
        /// </summary>
        [JsonProperty("fc")]
        [YamlMember(Alias = "fc")]
        public override Models.FCVolumeSourceV1 Fc
        {
            get
            {
                return base.Fc;
            }
            set
            {
                base.Fc = value;

                __ModifiedProperties__.Add("Fc");
            }
        }


        /// <summary>
        ///     RBD represents a Rados Block Device mount on the host that shares a pod's lifetime. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md
        /// </summary>
        [JsonProperty("rbd")]
        [YamlMember(Alias = "rbd")]
        public override Models.RBDVolumeSourceV1 Rbd
        {
            get
            {
                return base.Rbd;
            }
            set
            {
                base.Rbd = value;

                __ModifiedProperties__.Add("Rbd");
            }
        }


        /// <summary>
        ///     AWSElasticBlockStore represents an AWS Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes#awselasticblockstore
        /// </summary>
        [JsonProperty("awsElasticBlockStore")]
        [YamlMember(Alias = "awsElasticBlockStore")]
        public override Models.AWSElasticBlockStoreVolumeSourceV1 AwsElasticBlockStore
        {
            get
            {
                return base.AwsElasticBlockStore;
            }
            set
            {
                base.AwsElasticBlockStore = value;

                __ModifiedProperties__.Add("AwsElasticBlockStore");
            }
        }


        /// <summary>
        ///     AzureFile represents an Azure File Service mount on the host and bind mount to the pod.
        /// </summary>
        [JsonProperty("azureFile")]
        [YamlMember(Alias = "azureFile")]
        public override Models.AzureFileVolumeSourceV1 AzureFile
        {
            get
            {
                return base.AzureFile;
            }
            set
            {
                base.AzureFile = value;

                __ModifiedProperties__.Add("AzureFile");
            }
        }


        /// <summary>
        ///     FlexVolume represents a generic volume resource that is provisioned/attached using an exec based plugin. This is an alpha feature and may change in future.
        /// </summary>
        [JsonProperty("flexVolume")]
        [YamlMember(Alias = "flexVolume")]
        public override Models.FlexVolumeSourceV1 FlexVolume
        {
            get
            {
                return base.FlexVolume;
            }
            set
            {
                base.FlexVolume = value;

                __ModifiedProperties__.Add("FlexVolume");
            }
        }


        /// <summary>
        ///     PortworxVolume represents a portworx volume attached and mounted on kubelets host machine
        /// </summary>
        [JsonProperty("portworxVolume")]
        [YamlMember(Alias = "portworxVolume")]
        public override Models.PortworxVolumeSourceV1 PortworxVolume
        {
            get
            {
                return base.PortworxVolume;
            }
            set
            {
                base.PortworxVolume = value;

                __ModifiedProperties__.Add("PortworxVolume");
            }
        }


        /// <summary>
        ///     Quobyte represents a Quobyte mount on the host that shares a pod's lifetime
        /// </summary>
        [JsonProperty("quobyte")]
        [YamlMember(Alias = "quobyte")]
        public override Models.QuobyteVolumeSourceV1 Quobyte
        {
            get
            {
                return base.Quobyte;
            }
            set
            {
                base.Quobyte = value;

                __ModifiedProperties__.Add("Quobyte");
            }
        }


        /// <summary>
        ///     Name of StorageClass to which this persistent volume belongs. Empty value means that this volume does not belong to any StorageClass.
        /// </summary>
        [JsonProperty("storageClassName")]
        [YamlMember(Alias = "storageClassName")]
        public override string StorageClassName
        {
            get
            {
                return base.StorageClassName;
            }
            set
            {
                base.StorageClassName = value;

                __ModifiedProperties__.Add("StorageClassName");
            }
        }


        /// <summary>
        ///     VsphereVolume represents a vSphere volume attached and mounted on kubelets host machine
        /// </summary>
        [JsonProperty("vsphereVolume")]
        [YamlMember(Alias = "vsphereVolume")]
        public override Models.VsphereVirtualDiskVolumeSourceV1 VsphereVolume
        {
            get
            {
                return base.VsphereVolume;
            }
            set
            {
                base.VsphereVolume = value;

                __ModifiedProperties__.Add("VsphereVolume");
            }
        }


        /// <summary>
        ///     ClaimRef is part of a bi-directional binding between PersistentVolume and PersistentVolumeClaim. Expected to be non-nil when bound. claim.VolumeName is the authoritative bind between PV and PVC. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#binding
        /// </summary>
        [JsonProperty("claimRef")]
        [YamlMember(Alias = "claimRef")]
        public override Models.ObjectReferenceV1 ClaimRef
        {
            get
            {
                return base.ClaimRef;
            }
            set
            {
                base.ClaimRef = value;

                __ModifiedProperties__.Add("ClaimRef");
            }
        }


        /// <summary>
        ///     HostPath represents a directory on the host. Provisioned by a developer or tester. This is useful for single-node development and testing only! On-host storage is not supported in any way and WILL NOT WORK in a multi-node cluster. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
        /// </summary>
        [JsonProperty("hostPath")]
        [YamlMember(Alias = "hostPath")]
        public override Models.HostPathVolumeSourceV1 HostPath
        {
            get
            {
                return base.HostPath;
            }
            set
            {
                base.HostPath = value;

                __ModifiedProperties__.Add("HostPath");
            }
        }


        /// <summary>
        ///     ISCSI represents an ISCSI Disk resource that is attached to a kubelet's host machine and then exposed to the pod. Provisioned by an admin.
        /// </summary>
        [JsonProperty("iscsi")]
        [YamlMember(Alias = "iscsi")]
        public override Models.ISCSIVolumeSourceV1 Iscsi
        {
            get
            {
                return base.Iscsi;
            }
            set
            {
                base.Iscsi = value;

                __ModifiedProperties__.Add("Iscsi");
            }
        }


        /// <summary>
        ///     AzureDisk represents an Azure Data Disk mount on the host and bind mount to the pod.
        /// </summary>
        [JsonProperty("azureDisk")]
        [YamlMember(Alias = "azureDisk")]
        public override Models.AzureDiskVolumeSourceV1 AzureDisk
        {
            get
            {
                return base.AzureDisk;
            }
            set
            {
                base.AzureDisk = value;

                __ModifiedProperties__.Add("AzureDisk");
            }
        }


        /// <summary>
        ///     GCEPersistentDisk represents a GCE Disk resource that is attached to a kubelet's host machine and then exposed to the pod. Provisioned by an admin. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
        /// </summary>
        [JsonProperty("gcePersistentDisk")]
        [YamlMember(Alias = "gcePersistentDisk")]
        public override Models.GCEPersistentDiskVolumeSourceV1 GcePersistentDisk
        {
            get
            {
                return base.GcePersistentDisk;
            }
            set
            {
                base.GcePersistentDisk = value;

                __ModifiedProperties__.Add("GcePersistentDisk");
            }
        }


        /// <summary>
        ///     PhotonPersistentDisk represents a PhotonController persistent disk attached and mounted on kubelets host machine
        /// </summary>
        [JsonProperty("photonPersistentDisk")]
        [YamlMember(Alias = "photonPersistentDisk")]
        public override Models.PhotonPersistentDiskVolumeSourceV1 PhotonPersistentDisk
        {
            get
            {
                return base.PhotonPersistentDisk;
            }
            set
            {
                base.PhotonPersistentDisk = value;

                __ModifiedProperties__.Add("PhotonPersistentDisk");
            }
        }


        /// <summary>
        ///     Local represents directly-attached storage with node affinity
        /// </summary>
        [JsonProperty("local")]
        [YamlMember(Alias = "local")]
        public override Models.LocalVolumeSourceV1 Local
        {
            get
            {
                return base.Local;
            }
            set
            {
                base.Local = value;

                __ModifiedProperties__.Add("Local");
            }
        }


        /// <summary>
        ///     Cinder represents a cinder volume attached and mounted on kubelets host machine More info: https://releases.k8s.io/HEAD/examples/mysql-cinder-pd/README.md
        /// </summary>
        [JsonProperty("cinder")]
        [YamlMember(Alias = "cinder")]
        public override Models.CinderVolumeSourceV1 Cinder
        {
            get
            {
                return base.Cinder;
            }
            set
            {
                base.Cinder = value;

                __ModifiedProperties__.Add("Cinder");
            }
        }


        /// <summary>
        ///     Flocker represents a Flocker volume attached to a kubelet's host machine and exposed to the pod for its usage. This depends on the Flocker control service being running
        /// </summary>
        [JsonProperty("flocker")]
        [YamlMember(Alias = "flocker")]
        public override Models.FlockerVolumeSourceV1 Flocker
        {
            get
            {
                return base.Flocker;
            }
            set
            {
                base.Flocker = value;

                __ModifiedProperties__.Add("Flocker");
            }
        }


        /// <summary>
        ///     AccessModes contains all ways the volume can be mounted. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#access-modes
        /// </summary>
        [YamlMember(Alias = "accessModes")]
        [JsonProperty("accessModes", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> AccessModes { get; set; } = new List<string>();

        /// <summary>
        ///     CephFS represents a Ceph FS mount on the host that shares a pod's lifetime
        /// </summary>
        [JsonProperty("cephfs")]
        [YamlMember(Alias = "cephfs")]
        public override Models.CephFSVolumeSourceV1 Cephfs
        {
            get
            {
                return base.Cephfs;
            }
            set
            {
                base.Cephfs = value;

                __ModifiedProperties__.Add("Cephfs");
            }
        }


        /// <summary>
        ///     Glusterfs represents a Glusterfs volume that is attached to a host and exposed to the pod. Provisioned by an admin. More info: https://releases.k8s.io/HEAD/examples/volumes/glusterfs/README.md
        /// </summary>
        [JsonProperty("glusterfs")]
        [YamlMember(Alias = "glusterfs")]
        public override Models.GlusterfsVolumeSourceV1 Glusterfs
        {
            get
            {
                return base.Glusterfs;
            }
            set
            {
                base.Glusterfs = value;

                __ModifiedProperties__.Add("Glusterfs");
            }
        }


        /// <summary>
        ///     NFS represents an NFS mount on the host. Provisioned by an admin. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [JsonProperty("nfs")]
        [YamlMember(Alias = "nfs")]
        public override Models.NFSVolumeSourceV1 Nfs
        {
            get
            {
                return base.Nfs;
            }
            set
            {
                base.Nfs = value;

                __ModifiedProperties__.Add("Nfs");
            }
        }


        /// <summary>
        ///     StorageOS represents a StorageOS volume that is attached to the kubelet's host machine and mounted into the pod More info: https://releases.k8s.io/HEAD/examples/volumes/storageos/README.md
        /// </summary>
        [JsonProperty("storageos")]
        [YamlMember(Alias = "storageos")]
        public override Models.StorageOSPersistentVolumeSourceV1 Storageos
        {
            get
            {
                return base.Storageos;
            }
            set
            {
                base.Storageos = value;

                __ModifiedProperties__.Add("Storageos");
            }
        }


        /// <summary>
        ///     A description of the persistent volume's resources and capacity. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#capacity
        /// </summary>
        [YamlMember(Alias = "capacity")]
        [JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Capacity { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     What happens to a persistent volume when released from its claim. Valid options are Retain (default) and Recycle. Recycling must be supported by the volume plugin underlying this persistent volume. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#reclaiming
        /// </summary>
        [JsonProperty("persistentVolumeReclaimPolicy")]
        [YamlMember(Alias = "persistentVolumeReclaimPolicy")]
        public override string PersistentVolumeReclaimPolicy
        {
            get
            {
                return base.PersistentVolumeReclaimPolicy;
            }
            set
            {
                base.PersistentVolumeReclaimPolicy = value;

                __ModifiedProperties__.Add("PersistentVolumeReclaimPolicy");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
