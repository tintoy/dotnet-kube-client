# KubeClient

[![Build Status (Travis CI)](https://travis-ci.org/tintoy/dotnet-kube-client.svg?branch=develop)](https://travis-ci.org/tintoy/dotnet-kube-client)

KubeClient is an extensible Kubernetes API client for .NET Core (targets `netstandard1.4`).

Note - there is also an [official](https://github.com/kubernetes-client/csharp/) .NET client for Kubernetes (both clients actually share code in a couple of places). These two clients are philosophically-different (from a design perspective) but either can be bent to fit your needs. For more information about how KubeClient differs from the official client, see the section below on [extensibility](#extensibility).

## Prerequisites

**Note:** If you need WebSocket / `exec` you'll need to target `netstandard2.1`.

## Packages

* `KubeClient` (`netstandard1.4` or newer)    
  The main client and models.  
  [![KubeClient](https://img.shields.io/nuget/v/KubeClient.svg)](https://www.nuget.org/packages/KubeClient)
* `KubeClient.Extensions.Configuration` (`netstandard2.0` or newer)  
  Support for sourcing `Microsoft.Extensions.Configuration` data from Kubernetes Secrets and ConfigMaps.  
  [![KubeClient.Extensions.KubeConfig](https://img.shields.io/nuget/v/KubeClient.Extensions.Configuration.svg)](https://www.nuget.org/packages/KubeClient.Extensions.Configuration)
* `KubeClient.Extensions.DependencyInjection` (`netstandard2.0` or newer)  
  Dependency-injection support.  
  [![KubeClient.Extensions.KubeConfig](https://img.shields.io/nuget/v/KubeClient.Extensions.DependencyInjection.svg)](https://www.nuget.org/packages/KubeClient.Extensions.DependencyInjection)  
* `KubeClient.Extensions.KubeConfig` (`netstandard1.4` or newer)  
  Support for loading and parsing configuration from `~/.kube/config`.  
  [![KubeClient.Extensions.KubeConfig](https://img.shields.io/nuget/v/KubeClient.Extensions.KubeConfig.svg)](https://www.nuget.org/packages/KubeClient.Extensions.KubeConfig)
* `KubeClient.Extensions.WebSockets` (`netstandard2.1` or newer)  
  Support for multiplexed WebSocket connections used by Kubernetes APIs (such as [exec](src/KubeClient.Extensions.WebSockets/ResourceClientWebSocketExtensions.cs#L56)).   
  This package also extends resource clients to add support for those APIs.  
  
  Note that, due to a dependency on the new managed WebSockets implementation in .NET Core, this package targets `netcoreapp2.1` (which requires SDK version `2.1.300` or newer) and therefore only works on _.NET Core_ 2.1 or newer (it won't work on the full .NET Framework / UWP / Xamarin until they support `netstandard2.1`).  
  [![KubeClient.Extensions.WebSockets](https://img.shields.io/nuget/v/KubeClient.Extensions.WebSockets.svg)](https://www.nuget.org/packages/KubeClient.Extensions.WebSockets)

If you want to use the latest (development) builds of KubeClient, add the following feed to `NuGet.config`: https://www.myget.org/F/dotnet-kube-client/api/v3/index.json

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
    AuthStrategy = KubeAuthStrategy.BearerToken,
    AccessToken = "my-access-token",
    AllowInsecure = true // Don't validate server certificate
});
```

You can enable logging of requests and responses by passing an `ILoggerFactory` to `KubeApiClient.Create()` or `KubeClientOptions.LoggerFactory`:

```csharp
ILoggerFactory loggers = new LoggerFactory();
loggers.AddConsole();

KubeApiClient client = KubeApiClient.Create("http://localhost:8001", loggers);
```

### Configure the client from ~/.kube/config

```csharp
using KubeClient.Extensions.KubeConfig;

KubeClientOptions clientOptions = K8sConfig.Load(kubeConfigFile).ToKubeClientOptions(
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
        AuthStrategy = KubeAuthStrategy.BearerToken,
        AccessToken = "my-access-token",
        AllowInsecure = true // Don't validate server certificate
    });
}
```

To add a named instance of the client:

```csharp
void ConfigureServices(IServiceCollection services)
{
    services.AddNamedKubeClients();
    services.AddKubeClientOptions("my-cluster", clientOptions =>
    {
        clientOptions.ApiEndPoint = new Uri("http://localhost:8001");
        clientOptions.AuthStrategy = KubeAuthStrategy.BearerToken;
        clientOptions.AccessToken = "my-access-token";
        clientOptions.AllowInsecure = true; // Don't validate server certificate
    });
    
    // OR:

    services.AddKubeClient("my-cluster", clientOptions =>
    {
        clientOptions.ApiEndPoint = new Uri("http://localhost:8001");
        clientOptions.AuthStrategy = KubeAuthStrategy.BearerToken;
        clientOptions.AccessToken = "my-access-token";
        clientOptions.AllowInsecure = true; // Don't validate server certificate
    });
}

// To use named instances of KubeApiClient, inject INamedKubeClients.

class MyClass
{
    public MyClass(INamedKubeClients namedKubeClients)
    {
        KubeClient1 = namedKubeClients.Get("my-cluster");
        KubeClient2 = namedKubeClients.Get("another-cluster");
    }

    IKubeApiClient KubeClient1 { get; }
    IKubeApiClient KubeClient2 { get; }
}
```

## Design philosophy

Use of code generation is limited; generated clients tend to wind up being non-idiomatic and, for a Swagger spec as large as that of Kubernetes, wind up placing too many methods directly on the client class.

KubeClient's approach is to generate model classes (see `src/swagger` for the Python script that does this) and hand-code the actual operation methods to provide an improved consumer experience (i.e. useful and consistent exception types).

### KubeResultV1

Some operations in the Kubernetes API can return a different response depending on the arguments passed in. For example, a request to delete a `v1/Pod` returns the existing `v1/Pod` (as a `PodV1` model) if the caller specifies `DeletePropagationPolicy.Foreground` but returns a `v1/Status` (as a `StatusV1` model) if any other type of `DeletePropagationPolicy` is specified.

To handle this type of polymorphic response KubeClient uses the `KubeResultV1` model (and its derived implementations, `KubeResourceResultV1<TResource>` and `KubeResourceListResultV1<TResource>`).

`KubeResourceResultV1<TResource>` can be implicitly cast to a `TResource` or a `StatusV1`, so consuming code can continue to use the client as if it expects an operation to return only a resource or expects it to return only a `StatusV1`:

```csharp
PodV1 existingPod = await client.PodsV1().Delete("mypod", propagationPolicy: DeletePropagationPolicy.Foreground);
// OR:
StatusV1 deleteStatus = await client.PodsV1().Delete("mypod", propagationPolicy: DeletePropagationPolicy.Background);
```

If an attempt is made to cast a `KubeResourceResultV1<TResource>` that contains a non-success `StatusV1` to a `TResource`, a `KubeApiException` is thrown, based on the information in the `StatusV1`:

```csharp
PodV1 existingPod;

try
{
    existingPod = await client.PodsV1().Delete("mypod", propagationPolicy: DeletePropagationPolicy.Foreground);
}
catch (KubeApiException kubeApiError)
{
    Log.Error(kubeApiError, "Failed to delete Pod: {ErrorMessage}", kubeApiError.Status.Message);
}
```

For more information about the behaviour of `KubeResultV1` and its derived implementations, see [KubeResultTests.cs](test/KubeClient.Tests/KubeResultTests.cs).

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
            .ReadContentAsObjectV1Async<PodListV1>();

        return matchingPods.Items;
    }

    public static class Requests
    {
        public static readonly HttpRequest Collection = KubeRequest.Create("api/v1/namespaces/{Namespace}/pods?labelSelector={LabelSelector?}&watch={Watch?}");
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
public static async Task<K8sMultiplexer> ExecAndConnect(this IPodClientV1 podClient, string podName, string command, bool stdin = false, bool stdout = true, bool stderr = false, bool tty = false, string container = null, string kubeNamespace = null, CancellationToken cancellation = default)
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

#### Working out what APIs to call

If you want to replicate the behaviour of a `kubectl` command you can pass the flag `--v=10` to `kubectl` and it will dump out (for each request that it makes) the request URI, request body, and response body.

### Building

You will need to use v2.1.300 (or newer) of the .NET Core SDK to build KubeClient.

## Questions / feedback

Feel free to [get in touch](https://github.com/tintoy/dotnet-kube-client/issues/new) if you have questions, feedback, or would like to contribute.
