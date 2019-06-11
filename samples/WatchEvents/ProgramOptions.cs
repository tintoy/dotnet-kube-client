using CommandLine;

namespace KubeClient.Samples.WatchEvents
{
    /// <summary>
    ///     Program options for WatchEvents.
    /// </summary>
    class ProgramOptions
    {
        /// <summary>
        ///     The name of the namespace where the deployment will be created.
        /// </summary>
        [Option("namespace", Default = "default", HelpText = "The name of the namespace where the deployment will be created.")]
        public string KubeNamespace { get; set; }

        /// <summary>
        ///     Enable verbose logging.
        /// </summary>
        [Option('v', "verbose", Default = false, HelpText = "Enable verbose logging")]
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