# dotnet-kube-client

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

TODO: Document client extensibility points.
