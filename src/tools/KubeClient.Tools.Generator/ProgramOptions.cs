using CommandLine;

namespace KubeClient.Tools.Generator
{
    /// <summary>
    ///     Program options for the generator tool.
    /// </summary>
    class ProgramOptions
    {
        /// <summary>
        ///     The name of the target Kubernetes client context to use (from ~/kube/config).
        /// </summary>
        [Option('c', "context", Required = true, HelpText = "")]
        public string KubeContextName { get; set; } = String.Empty;

        /// <summary>
        ///     The name of the target API group.
        /// </summary>
        [Option('g', "group", Required = true, HelpText = "The name of the target API group.")]
        public string Group { get; set; } = String.Empty;

        /// <summary>
        ///     The name of the target API version.
        /// </summary>
        [Option('v', "version", Required = true, HelpText = "The name of the target API version.")]
        public string Version { get; set; } = String.Empty;

        /// <summary>
        ///     The name of the target resource kind.
        /// </summary>
        [Option('k', "kind", Required = true, HelpText = "The name of the target resource kind.")]
        public string Kind { get; set; } = String.Empty;

        /// <summary>
        ///     The namespace for generated code.
        /// </summary>
        [Option('n', "namespace", Required = true, HelpText = "The namespace for generated code.")]
        public string Namespace { get; set; } = String.Empty;

        /// <summary>
        ///     The name of the output file for generated code.
        /// </summary>
        [Option('o', "out", Required = true, HelpText = "The name of the output file for generated code.")]
        public string OutputFile { get; set; } = String.Empty;

        /// <summary>
        ///     Enable verbose logging.
        /// </summary>
        [Option("verbose", Default = false, HelpText = "Enable verbose logging.")]
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
        public static ProgramOptions? Parse(string[] commandLineArguments)
        {
            ProgramOptions? options = null;

            Parser.Default.ParseArguments<ProgramOptions>(commandLineArguments)
                .WithParsed(parsedOptions => options = parsedOptions);

            return options;
        }
    }
}
