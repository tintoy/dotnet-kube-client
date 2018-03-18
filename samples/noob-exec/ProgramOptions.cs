using CommandLine;

namespace KubeClient.Samples.NoobExec
{
    /// <summary>
    ///     Program options for NoobExec.
    /// </summary>
    class ProgramOptions
    {
        /// <summary>
        ///     The name of the target pod.
        /// </summary>
        [Option('p', "pod", Required = true, HelpText = "The name of the target pod.")]
        public string PodName { get; set; }

        /// <summary>
        ///     The command to execute.
        /// </summary>
        [Option('c', "command", Required = true, HelpText = "The command to execute.")]
        public string Command { get; set; }

        /// <summary>
        ///     The name of the target container within the target pod.
        /// </summary>
        /// <remarks>
        ///     Optional, if the pod only has a single container.
        /// </remarks>
        [Option("container", Default = null, HelpText = "The name of the target container within the target pod. Optional, if the pod only has a single container.")]
        public string ContainerName { get; set; }
        
        /// <summary>
        ///     The Kubernetes namespace where the target pod is located.
        /// </summary>
        [Option('n', "namespace", Default = "default", HelpText = "The Kubernetes namespace where the target pod is located.")]
        public string KubeNamespace { get; set; }

        /// <summary>
        ///     The name of the Kubernetes client configuration context to use.
        /// </summary>
        [Option('k', "kube-context", Default = null, HelpText = "The name of the Kubernetes client configuration context to use.")]
        public string KubeContext { get; set; }

        /// <summary>
        ///     Enable verbose logging?
        /// </summary>
        [Option('v', "verbose", Default = false, HelpText = "Enable verbose logging.")]
        public bool Verbose { get; set; }

        /// <summary>
        ///     Parse program options from command-line arguments.
        /// </summary>
        /// <param name="commandLineArguments">
        ///     The command-line arguments
        /// </param>
        /// <returns>
        ///     The parsed <see cref="ProgramOptions"/>, or <c>null</c> if the command-line arguments could not be parsed.
        /// </returns>
        public static ProgramOptions Parse(string[] commandLineArguments)
        {
            ProgramOptions options = null;

            Parser.Default.ParseArguments<ProgramOptions>(commandLineArguments)
                .WithParsed(parsedOptions => options = parsedOptions);

            return options;
        }
    }
}
