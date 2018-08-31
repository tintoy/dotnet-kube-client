using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Volume represents a named volume in a pod that may be accessed by any container in the pod.
    /// </summary>
    public partial class VolumeV1 : Models.VolumeV1
    {
        /// <summary>
        ///     DownwardAPI represents downward API about the pod that should populate this volume
        /// </summary>
        [JsonProperty("downwardAPI")]
        [YamlMember(Alias = "downwardAPI")]
        public override Models.DownwardAPIVolumeSourceV1 DownwardAPI
        {
            get
            {
                return base.DownwardAPI;
            }
            set
            {
                base.DownwardAPI = value;

                __ModifiedProperties__.Add("DownwardAPI");
            }
        }


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
        ///     Items for all in one resources secrets, configmaps, and downward API
        /// </summary>
        [JsonProperty("projected")]
        [YamlMember(Alias = "projected")]
        public override Models.ProjectedVolumeSourceV1 Projected
        {
            get
            {
                return base.Projected;
            }
            set
            {
                base.Projected = value;

                __ModifiedProperties__.Add("Projected");
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
        ///     Volume's name. Must be a DNS_LABEL and unique within the pod. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                __ModifiedProperties__.Add("Name");
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
        ///     HostPath represents a pre-existing file or directory on the host machine that is directly exposed to the container. This is generally used for system agents or other privileged things that are allowed to see the host machine. Most containers will NOT need this. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
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
        ///     ISCSI represents an ISCSI Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://releases.k8s.io/HEAD/examples/volumes/iscsi/README.md
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
        ///     GCEPersistentDisk represents a GCE Disk resource that is attached to a kubelet's host machine and then exposed to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
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
        ///     PersistentVolumeClaimVolumeSource represents a reference to a PersistentVolumeClaim in the same namespace. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [JsonProperty("persistentVolumeClaim")]
        [YamlMember(Alias = "persistentVolumeClaim")]
        public override Models.PersistentVolumeClaimVolumeSourceV1 PersistentVolumeClaim
        {
            get
            {
                return base.PersistentVolumeClaim;
            }
            set
            {
                base.PersistentVolumeClaim = value;

                __ModifiedProperties__.Add("PersistentVolumeClaim");
            }
        }


        /// <summary>
        ///     GitRepo represents a git repository at a particular revision.
        /// </summary>
        [JsonProperty("gitRepo")]
        [YamlMember(Alias = "gitRepo")]
        public override Models.GitRepoVolumeSourceV1 GitRepo
        {
            get
            {
                return base.GitRepo;
            }
            set
            {
                base.GitRepo = value;

                __ModifiedProperties__.Add("GitRepo");
            }
        }


        /// <summary>
        ///     ConfigMap represents a configMap that should populate this volume
        /// </summary>
        [JsonProperty("configMap")]
        [YamlMember(Alias = "configMap")]
        public override Models.ConfigMapVolumeSourceV1 ConfigMap
        {
            get
            {
                return base.ConfigMap;
            }
            set
            {
                base.ConfigMap = value;

                __ModifiedProperties__.Add("ConfigMap");
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
        ///     EmptyDir represents a temporary directory that shares a pod's lifetime. More info: https://kubernetes.io/docs/concepts/storage/volumes#emptydir
        /// </summary>
        [JsonProperty("emptyDir")]
        [YamlMember(Alias = "emptyDir")]
        public override Models.EmptyDirVolumeSourceV1 EmptyDir
        {
            get
            {
                return base.EmptyDir;
            }
            set
            {
                base.EmptyDir = value;

                __ModifiedProperties__.Add("EmptyDir");
            }
        }


        /// <summary>
        ///     Flocker represents a Flocker volume attached to a kubelet's host machine. This depends on the Flocker control service being running
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
        ///     Glusterfs represents a Glusterfs mount on the host that shares a pod's lifetime. More info: https://releases.k8s.io/HEAD/examples/volumes/glusterfs/README.md
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
        ///     NFS represents an NFS mount on the host that shares a pod's lifetime More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
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
        ///     StorageOS represents a StorageOS volume attached and mounted on Kubernetes nodes.
        /// </summary>
        [JsonProperty("storageos")]
        [YamlMember(Alias = "storageos")]
        public override Models.StorageOSVolumeSourceV1 Storageos
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
        ///     Secret represents a secret that should populate this volume. More info: https://kubernetes.io/docs/concepts/storage/volumes#secret
        /// </summary>
        [JsonProperty("secret")]
        [YamlMember(Alias = "secret")]
        public override Models.SecretVolumeSourceV1 Secret
        {
            get
            {
                return base.Secret;
            }
            set
            {
                base.Secret = value;

                __ModifiedProperties__.Add("Secret");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
