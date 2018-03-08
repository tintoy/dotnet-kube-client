# KubeClient

KubeClient is an extensible Kubernetes API client for .NET Core.

## Prerequisites

The KubeClient packages target `netstandard2.0`; if you need `netstandard1.3` support then install v0.x of the packages.
If you need WebSocket / `exec` you'll need the code from the `feature/websockets` branch (targets `netcoreapp2.1`, which is required for cross-platform WebSockets support).

## Packages

* `KubeClient`  
  The main client and models.
* `KubeClient.Extensions.DependencyInjection`  
  Dependency-injection support.
* `KubeClient.Extensions.KubeConfig`  
  Support for loading and parsing configuration from `~/.kube/config`.
* `KubeClient.Extensions.WebSockets`  
  Support for multiplexed WebSocket connections used by Kubernetes APIs (such as [exec](src/KubeClient.Extensions.WebSockets/ResourceClientWebSocketExtensions.cs#L56)).  
  This package also extends resource clients to add support for those APIs.

## Usage

The client can be used directly or injected via `Microsoft.Extensions.DependencyInjection`.

### Use the client directly

The simplest way to create a client is to call `KubeApiClient.Create()`. There are overloads if you want to provide an access token, client certificate, or customise validation of the server's certificate:

```csharp
// Assumes you're using "kubectl proxy", and no authentication is required.
KubeApiClient client = KubeApiClient.Create("http://localhost:8001");

PodListV1 pods = await client.PodsV1().List(
    labelSelector: "k8s-app=my-app"
);
```

For more flexible configuration, use the overload that takes `KubeClientOptions`:

```csharp
KubeApiClient client = KubeApiClient.Create(new KubeClientOptions
{
    ApiEndPoint = new Uri("http://localhost:8001"),
    AccessToken = "my-access-token",
    AllowInsecure = true // Don't validate server certificate
});
```

You can enable logging of requests and responses by passing an `ILoggerFactory` to `KubeApiClient.Create()`:

```csharp
ILoggerFactory loggers = new LoggerFactory();
loggers.AddConsole();

KubeApiClient client = KubeApiClient.Create("http://localhost:8001", loggers);
```

### Configure the client from ~/.kube/config

```csharp
using KubeClient.Extensions.KubeConfig;

KubeClientOptions clientOptions = Config.Load(kubeConfigFile).ToKubeClientOptions(
    kubeContextName: "my-cluster",
    defaultKubeNamespace: "kube-system"
);

KubeApiClient client = KubeApiClient.Create(clientOptions);
```

### Make the client available for dependency injection

The client can be configured for dependency injection in a variety of ways.

To use a fixed set of options for the client, use the overload of `AddKubeClient()` that takes `KubeClientoptions`:

```csharp
void ConfigureServices(IServiceCollection services)
{
    services.AddKubeClient(new KubeClientOptions
    {
        ApiEndPoint = new Uri("http://localhost:8001"),
        AccessToken = "my-access-token",
        AllowInsecure = true // Don't validate server certificate
    });
}
```

Feel free to create an issue if you have any questions.

## Extensibility

KubeClient is designed to be easily extensible. The `KubeApiClient` provides the top-level entry point for the Kubernetes API and extension methods are used to expose more specific resource clients.

Simplified version of [PodClientV1.cs](src/KubeClient/ResourceClients/PodClientV1.cs):

```csharp
public class PodClientV1 : KubeResourceClient
{
    public PodClientV1(KubeApiClient client) : base(client)
    {
    }

    public async Task<List<PodV1>> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
    {
        PodListV1 matchingPods =
            await Http.GetAsync(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            )
            .ReadContentAsAsync<PodListV1, StatusV1>();

        return matchingPods.Items;
    }

    public static class Requests
    {
        public static readonly HttpRequest Collection = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/pods?labelSelector={LabelSelector?}&watch={Watch?}", SerializerSettings);
    }
}
```

Simplified version of [ClientFactoryExtensions.cs](src/KubeClient/ClientFactoryExtensions.cs#L97):

```csharp
public static PodClientV1 PodsV1(this KubeApiClient kubeClient)
{
    return kubeClient.ResourceClient(
        client => new PodClientV1(client)
    );
}
```

This enables the following usage of `KubeApiClient`:

```csharp
KubeApiClient client;
PodListV1 pods = await client.PodsV1().List(kubeNamespace: "kube-system");
```

Through the use of extension methods, resource clients (or additional operations) can be declared in any assembly and used as if they are part of the `KubeApiClient`. For example, the `KubeClient.Extensions.WebSockets` package adds an `ExecAndConnect` method to `PodClientV1`.

Simplified version of [ResourceClientWebSocketExtensions.cs](src/KubeClient.Extensions.WebSockets/ResourceClientWebSocketExtensions.cs#L56):

```csharp
public static async Task<K8sMultiplexer> ExecAndConnect(this PodClientV1 podClient, string podName, string command, bool stdin = false, bool stdout = true, bool stderr = false, bool tty = false, string container = null, string kubeNamespace = null, CancellationToken cancellation = default)
{
    byte[] outputStreamIndexes = stdin ? new byte[1] { 0 } : new byte[0];
    byte[] inputStreamIndexes;
    if (stdout && stderr)
        inputStreamIndexes = new byte[2] { 1, 2 };
    else if (stdout)
        inputStreamIndexes = new byte[1] { 1 };
    else if (stderr)
        inputStreamIndexes = new byte[1] { 2 };
    else if (!stdin)
        throw new InvalidOperationException("Must specify at least one of STDIN, STDOUT, or STDERR.");
    else
        inputStreamIndexes = new byte[0];
    
    return await podClient.KubeClient
        .ConnectWebSocket("api/v1/namespaces/{KubeNamespace}/pods/{PodName}/exec?stdin={StdIn?}&stdout={StdOut?}&stderr={StdErr?}&tty={TTY?}&command={Command}&container={Container?}", new
        {
            PodName = podName,
            Command = command,
            StdIn = stdin,
            StdOut = stdout,
            StdErr = stderr,
            TTY = tty,
            Container = container,
            KubeNamespace = kubeNamespace ?? podClient.KubeClient.DefaultNamespace
        }, cancellation)
        .Multiplexed(inputStreamIndexes, outputStreamIndexes,
            loggerFactory: podClient.KubeClient.LoggerFactory()
        );
}
```

Example usage of `ExecAndConnect`:

```csharp
KubeApiClient client;
K8sMultiplexer connection = await client.PodsV1().ExecAndConnect(
    podName: "my-pod",
    command: "/bin/bash",
    stdin: true,
    stdout: true,
    tty: true
);
using (connection)
using (StreamWriter stdin = new StreamWriter(connection.GetOutputStream(0), Encoding.UTF8))
using (StreamReader stdout = new StreamReader(connection.GetInputStream(1), Encoding.UTF8))
{
    await stdin.WriteLineAsync("ls -l /");
    await stdin.WriteLineAsync("exit");

    // Read from STDOUT until process terminates.
    string line;
    while ((line = await stdout.ReadLineAsync()) != null)
    {
        Console.WriteLine(line);
    }
}
```

For information about `HttpRequest`, `UriTemplate`, and other features used to implement the client take a look at the [HTTPlease](https://tintoy.github.io/HTTPlease/) documentation.
