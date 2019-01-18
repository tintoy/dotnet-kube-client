using System;
using System.Linq;
using KubeClient;
using KubeClient.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ExtensionsSample
{
    /// <summary>
    /// Make sure to run: <code>kubectl apply -f ./thing-configmap.yaml</code> to create the extensions-sample configMap before running this sample.  You also need to be running <code>kubectl proxy</code>
    /// </summary>
    static class Program
    {
        static void Main()
        {
            var configBuilder = new ConfigurationBuilder();
            using (var client = KubeApiClient.Create("http://localhost:8001"))
            {
                configBuilder.AddKubeConfigMap(client, "extensions-sample", reloadOnChange: true);

                var root = configBuilder.Build();
                var thing = new Thing();
                root.GetSection("Thing").Bind(thing);
                Console.WriteLine("Thing: {0}", JsonConvert.SerializeObject(thing, Formatting.Indented));
            }
        }
    }

    class Thing
    {
        public string Environment { get; set; }
        public NestedThing Nested { get; set; }
    }

    class NestedThing
    {
        public string Name { get; set; }
        public string Other { get; set; }
    }
}
