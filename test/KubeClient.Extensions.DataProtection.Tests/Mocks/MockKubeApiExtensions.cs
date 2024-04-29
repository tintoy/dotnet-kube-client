using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reactive.Subjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using NJ = Newtonsoft.Json;

namespace KubeClient.Extensions.DataProtection.Tests.Mocks
{
    using Models;

    public static class MockKubeApiExtensions
    {
        public static WebApplication HandleSingleResource<TResource>(this WebApplication mockKubeApiApp, string resourcePath)
            where TResource : KubeResourceV1
        {
            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            TResource resourceState = null;

            string resourcesPath = GetPathParent(resourcePath);

            mockKubeApiApp.HandleResourceGet(resourcePath, () => resourceState);
            mockKubeApiApp.HandleResourceCreate(resourcesPath, (TResource initialResource) =>
            {
                resourceState = initialResource;

                return resourceState;
            });
            mockKubeApiApp.HandleResourceUpdate(resourcesPath, (TResource updatedResource) =>
            {
                resourceState = updatedResource;

                return resourceState;
            });
            mockKubeApiApp.HandleResourcePatch(resourcePath, (JArray patchRequest) =>
            {
                resourceState = resourceState.ApplyJsonPatch(patchRequest);

                return resourceState;
            });

            return mockKubeApiApp;
        }

        public static WebApplication HandleSingleResource<TResource>(this WebApplication mockKubeApiApp, string resourcePath, string watchPath)
            where TResource : KubeResourceV1
        {
            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (String.IsNullOrWhiteSpace(watchPath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(watchPath)}.", nameof(watchPath));

            TResource resourceState = null;

            string resourcesPath = GetPathParent(resourcePath);

            Subject<TResource> watchSubject = new Subject<TResource>();

            mockKubeApiApp.Lifetime.ApplicationStopping.Register(() =>
            {
                using (watchSubject)
                {
                    watchSubject.OnCompleted();
                }
            });

            mockKubeApiApp.HandleResourceGet(resourcePath, () => resourceState);
            mockKubeApiApp.HandleResourceCreate(resourcesPath, (TResource initialResource) =>
            {
                resourceState = initialResource;
                watchSubject.OnNext(resourceState);

                return resourceState;
            });
            mockKubeApiApp.HandleResourceUpdate(resourcesPath, (TResource updatedResource) =>
            {
                resourceState = updatedResource;
                watchSubject.OnNext(resourceState);

                return resourceState;
            });
            mockKubeApiApp.HandleResourcePatch(resourcePath, (JArray patchRequest) =>
            {
                resourceState = resourceState.ApplyJsonPatch(patchRequest);
                watchSubject.OnNext(resourceState);

                return resourceState;
            });
            mockKubeApiApp.HandleResourceWatch(watchPath, watchSubject);

            return mockKubeApiApp;
        }

        public static WebApplication HandleSingleResource<TResource>(this WebApplication mockKubeApiApp, string resourcePath, Func<TResource> loadResource, Func<TResource, TResource> saveResource)
            where TResource : KubeResourceV1
        {
            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            string resourcesPath = GetPathParent(resourcePath);

            mockKubeApiApp.HandleResourceGet(resourcePath, handler: loadResource);
            mockKubeApiApp.HandleResourceCreate(resourcesPath, (TResource initialResource) =>
            {
                return saveResource(initialResource);
            });
            mockKubeApiApp.HandleResourceUpdate(resourcesPath, (TResource updatedResource) =>
            {
                return saveResource(updatedResource);
            });
            mockKubeApiApp.HandleResourcePatchAsync(resourcePath, (JArray patchRequest) =>
            {
                TResource resource = loadResource();
                TResource patchedResource = resource.ApplyJsonPatch(patchRequest);
                TResource persistedResource = saveResource(patchedResource);

                return Task.FromResult(persistedResource);
            });

            return mockKubeApiApp;
        }

        public static WebApplication HandleSingleResource<TResource>(this WebApplication mockKubeApiApp, string resourcePath, string watchPath, Func<TResource> loadResource, Func<TResource, TResource> saveResource, ISubject<TResource> watchSubject)
            where TResource : KubeResourceV1
        {
            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (String.IsNullOrWhiteSpace(watchPath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(watchPath)}.", nameof(watchPath));

            if (watchSubject == null)
                throw new ArgumentNullException(nameof(watchSubject));

            string resourcesPath = GetPathParent(resourcePath);

            mockKubeApiApp.HandleResourceGet(resourcePath, handler: loadResource);
            mockKubeApiApp.HandleResourceCreate(resourcesPath, (TResource initialResource) =>
            {
                TResource persistedResource = saveResource(initialResource);
                watchSubject.OnNext(persistedResource);

                return persistedResource;
            });
            mockKubeApiApp.HandleResourceUpdate(resourcesPath, (TResource updatedState) =>
            {
                TResource persistedResource = saveResource(updatedState);
                watchSubject.OnNext(persistedResource);

                return persistedResource;
            });
            mockKubeApiApp.HandleResourcePatchAsync(resourcePath, (JArray patchRequest) =>
            {
                TResource resource = loadResource();
                TResource patchedResource = resource.ApplyJsonPatch(patchRequest);
                TResource persistedResource = saveResource(patchedResource);

                return Task.FromResult(persistedResource);
            });

            mockKubeApiApp.HandleResourceWatch(watchPath, watchSubject);

            return mockKubeApiApp;
        }

        public static WebApplication HandleResourceGet<TResource>(this WebApplication mockKubeApiApp, string resourcePath, Func<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp
                .HandleResourceGetAsync(resourcePath, () =>
                {
                    TResource response = handler();

                    return Task.FromResult(response);
                });

            return mockKubeApiApp;
        }

        public static WebApplication HandleResourceGetAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePath, Func<Task<TResource>> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp
                .MapGet(resourcePath, async () =>
                {
                    TResource responseBody = await handler();
                    if (responseBody == null)
                    {
                        (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
                        string resourceName = Path.GetFileNameWithoutExtension(resourcePath);

                        return Results.Content(
                            content: JsonConvert.SerializeObject(
                                StatusV1.Failure($"{apiVersion}/{kind} resource '{resourceName}' was not found.", "NotFound")
                            ),
                            contentType: "application/json",
                            statusCode: StatusCodes.Status404NotFound
                        );
                    }

                    return Results.Content(
                        content: JsonConvert.SerializeObject(responseBody),
                        contentType: "application/json",
                        statusCode: StatusCodes.Status200OK
                    );
                });

            return mockKubeApiApp;
        }

        public static WebApplication HandleResourceCreate<TResource>(this WebApplication mockKubeApiApp, string resourcePath, Func<TResource, TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp
                .HandleResourceCreateAsync(resourcePath, (TResource request) =>
                {
                    TResource responseBody = handler(request);

                    return Task.FromResult(responseBody);
                });

            return mockKubeApiApp;
        }

        public static WebApplication HandleResourceCreateAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePath, Func<TResource, Task<TResource>> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
            
            mockKubeApiApp
                .MapPost(resourcePath, async (HttpRequest request) =>
                {
                    TResource requestBody = request.ReadAsNewtonsoftJson<TResource>();
                    TResource responseBody = await handler(requestBody);
                    if (responseBody == null)
                    {
                        (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
                        string resourceName = Path.GetFileNameWithoutExtension(resourcePath);

                        return Results.Content(
                            content: JsonConvert.SerializeObject(
                                StatusV1.Failure($"{apiVersion}/{kind} resource '{resourceName}' was not found.", "NotFound")
                            ),
                            contentType: "application/json",
                            statusCode: StatusCodes.Status404NotFound
                        );
                    }

                    return Results.Content(
                        content: JsonConvert.SerializeObject(responseBody),
                        contentType: "application/json",
                        statusCode: StatusCodes.Status200OK
                    );
                });

            return mockKubeApiApp;
        }

        public static WebApplication HandleResourceUpdate<TResource>(this WebApplication mockKubeApiApp, string resourcePath, Func<TResource, TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp
                .HandleResourceUpdateAsync(resourcePath, (TResource request) =>
                {
                    TResource response = handler(request);

                    return Task.FromResult(response);
                });

            return mockKubeApiApp;
        }

        public static WebApplication HandleResourceUpdateAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePath, Func<TResource, Task<TResource>> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp
                .MapPut(resourcePath, async (HttpRequest request) =>
                {
                    TResource requestBody = request.ReadAsNewtonsoftJson<TResource>();
                    TResource responseBody = await handler(requestBody);
                    if (responseBody == null)
                    {
                        (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
                        string resourceName = Path.GetFileNameWithoutExtension(resourcePath);

                        return Results.Content(
                            content: JsonConvert.SerializeObject(
                                StatusV1.Failure($"{apiVersion}/{kind} resource '{resourceName}' was not found.", "NotFound")
                            ),
                            contentType: "application/json",
                            statusCode: StatusCodes.Status404NotFound
                        );
                    }

                    return Results.Content(
                        content: JsonConvert.SerializeObject(responseBody),
                        contentType: "application/json",
                        statusCode: StatusCodes.Status200OK
                    );
                });

            return mockKubeApiApp;
        }

        public static WebApplication HandleResourcePatch<TResource>(this WebApplication mockKubeApiApp, string resourcePath, Func<JArray, TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp
                .HandleResourcePatchAsync(resourcePath, (JArray patchRequest) =>
                {
                    TResource responseBody = handler(patchRequest);

                    return Task.FromResult(responseBody);
                });

            return mockKubeApiApp;
        }

        public static WebApplication HandleResourcePatchAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePath, Func<JArray, Task<TResource>> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp
                .MapPatch(resourcePath, async (HttpRequest request) =>
                {
                    ILogger logger = request.CreateLogger("MockKubeApi.ResourcePatchHandler");

                    JArray patchRequest = request.ReadAsNewtonsoftJArray();
                    logger.LogInformation("Patch request: {PatchRequest}", patchRequest.ToString(Formatting.None));

                    TResource responseBody = await handler(patchRequest);
                    if (responseBody == null)
                    {
                        (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
                        string resourceName = Path.GetFileNameWithoutExtension(resourcePath);

                        return Results.Content(
                            content: JsonConvert.SerializeObject(
                                StatusV1.Failure($"{apiVersion}/{kind} resource '{resourceName}' was not found.", "NotFound")
                            ),
                            contentType: "application/json",
                            statusCode: StatusCodes.Status404NotFound
                        );
                    }

                    return Results.Content(
                        content: JsonConvert.SerializeObject(responseBody),
                        contentType: "application/json",
                        statusCode: StatusCodes.Status200OK
                    );
                });

            return mockKubeApiApp;
        }

        public static WebApplication HandleResourceWatch<TResource>(this WebApplication mockKubeApiApp, string resourceWatchPath, IObservable<TResource> resourceStates)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourceWatchPath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourceWatchPath)}.", nameof(resourceWatchPath));

            if (resourceStates == null)
                throw new ArgumentNullException(nameof(resourceStates));

            mockKubeApiApp
                .MapGet(resourceWatchPath, async context =>
                {
                    // Used to block the request from completing until either the resourceStates sequence is completed/faulted, or the request is aborted.
                    using SemaphoreSlim requestCompleted = new SemaphoreSlim(initialCount: 0, maxCount: 1);
                    
                    void EndRequest()
                    {
                        try
                        {
                            requestCompleted.Release();
                        }
                        catch (SemaphoreFullException)
                        {
                            // Either the resource-state sequence has completed/faulted, or the request was canceled.
                        }
                    }

                    context.RequestAborted.Register(
                        () => EndRequest()
                    );

                    context.Response.Headers.ContentType = "application/json";
                    context.Response.StatusCode = 200;

                    using StreamWriter bodyWriter = new StreamWriter(context.Response.Body)
                    {
                        AutoFlush = true
                    };

                    resourceStates.Subscribe(
                        onNext: (TResource resource) =>
                        {
                            string resourceJson = JsonConvert.SerializeObject(resource);
                            bodyWriter.WriteLine(resourceJson);
                        },
                        onError: (Exception exception) =>
                        {
                            mockKubeApiApp.Logger.LogError(exception, "Unexpected error while streaming watched resource state.");

                            EndRequest();
                        },
                        onCompleted: () =>
                        {
                            EndRequest();
                        },
                        token: context.RequestAborted
                    );

                    try
                    {
                        // Don't complete the request until we're done streaming events.
                        await requestCompleted.WaitAsync(context.RequestAborted);
                    }
                    catch (OperationCanceledException waitCanceled)
                    {
                        if (waitCanceled.CancellationToken != context.RequestAborted)
                            throw;
                    }
                });

            return mockKubeApiApp;
        }

        public static ILogger CreateLogger(this HttpRequest request, string name)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(name)}.", nameof(name));

            return request.HttpContext.CreateLogger(name);
        }

        public static ILogger CreateLogger(this HttpContext httpContext, string name)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(name)}.", nameof(name));


            return httpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(name);
        }

        public static TBody ReadAsNewtonsoftJson<TBody>(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (StreamReader requestReader = new StreamReader(request.Body, leaveOpen: true))
            using (JsonTextReader requestJsonReader = new JsonTextReader(requestReader))
            {
                return NJ.JsonSerializer.Create().Deserialize<TBody>(requestJsonReader);
            }
        }

        public static JToken ReadAsNewtonsoftJToken(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (StreamReader requestReader = new StreamReader(request.Body, leaveOpen: true))
            using (JsonTextReader requestJsonReader = new JsonTextReader(requestReader))
            {
                return JToken.Load(requestJsonReader);
            }
        }

        public static JObject ReadAsNewtonsoftJObjkect(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (StreamReader requestReader = new StreamReader(request.Body, leaveOpen: true))
            using (JsonTextReader requestJsonReader = new JsonTextReader(requestReader))
            {
                return JObject.Load(requestJsonReader);
            }
        }

        public static JArray ReadAsNewtonsoftJArray(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (StreamReader requestReader = new StreamReader(request.Body, leaveOpen: true))
            using (JsonTextReader requestJsonReader = new JsonTextReader(requestReader))
            {
                return JArray.Load(requestJsonReader);
            }
        }

        public static TResource ApplyJsonPatch<TResource>(this TResource resource, JArray patchOperations)
            where TResource : KubeResourceV1
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            if (patchOperations == null)
                throw new ArgumentNullException(nameof(patchOperations));

            JObject resourceJson = JObject.FromObject(resource);

            foreach (JObject patchOperation in patchOperations.OfType<JObject>())
            {
                string path = patchOperation.Value<string>("path");
                if (String.IsNullOrWhiteSpace(path))
                    throw new NJ.JsonException($"Failed to apply JSON-patch operation (missing 'path' directive).");

                string operation = patchOperation.Value<string>("op");
                if (String.IsNullOrWhiteSpace(operation))
                    throw new NJ.JsonException($"Failed to apply JSON-patch operation (missing 'op' directive).");

                JToken value = patchOperation.Value<JToken>("value");
                if (value == null && operation != "delete")
                    throw new NJ.JsonException($"Failed to apply JSON-patch operation (missing 'value' directive but operation is '{operation}', not 'delete').");

                JToken target = resourceJson.SelectJsonPatchPath(path);
                
                switch (operation)
                {
                    case "delete":
                    {
                        if (target != null)
                            target.Remove();

                        break;
                    }
                    case "replace":
                    {
                        switch ((target, value))
                        {
                            case (JObject targetObject, JObject replacementObject):
                            {
                                targetObject.Replace(replacementObject);

                                break;
                            }
                            case (null, JObject newObject):
                            {
                                string parentPath = Path.GetDirectoryName(path);
                                
                                JToken parent = resourceJson.SelectJsonPatchPath(parentPath);

                                switch (parent)
                                {
                                    case JArray parentArray:
                                    {
                                        parentArray.Add(target);

                                        break;
                                    }
                                    case JObject parentObject:
                                    {
                                        string propertyName = Path.GetFileName(path);
                                        if (String.IsNullOrWhiteSpace(propertyName))
                                            throw new NJ.JsonException($"Failed to apply JSON-patch '{operation}' operation (cannot determine property name from target path '{path}').");

                                        parentObject[propertyName] = newObject;

                                        break;
                                    }
                                    case null:
                                    {
                                        throw new NJ.JsonException($"Failed to apply JSON-patch '{operation}' operation (parent of target token '{path}' was not found).");
                                    }
                                    default:
                                    {
                                        throw new NJ.JsonException($"Failed to apply JSON-patch '{operation}' operation (parent of target token is of type '{parent.Type}', which is not a container).");
                                    }
                                }

                                break;
                            }
                            default:
                            {
                                throw new NJ.JsonException($"Failed to apply JSON-patch '{operation}' operation (target token '{path}' is of type {target.Type}, which is not supported when the patch value is of type '{value.Type}').");
                            }
                        }

                        break;
                    }
                    default:
                    {
                        throw new NJ.JsonException($"Failed to apply JSON-patch '{operation}' operation (not a supported operation).");
                    }
                }
            }


            TResource patchedResource;
            using (JsonReader patchedJsonReader = resourceJson.CreateReader())
            {
                patchedResource = NJ.JsonSerializer.Create().Deserialize<TResource>(patchedJsonReader);
            }

            return patchedResource;
        }

        static JToken SelectJsonPatchPath(this JToken token, string jsonPatchPath)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            string jsonPath = JsonPatchPathToJsonPath(jsonPatchPath);
            if (String.IsNullOrWhiteSpace(jsonPatchPath))
                return jsonPath;

            return token.SelectToken(jsonPath);
        }

        static string JsonPatchPathToJsonPath(string jsonPatchPath)
        {
            if (jsonPatchPath == null)
                throw new ArgumentNullException(nameof(jsonPatchPath));

            jsonPatchPath = jsonPatchPath.Trim().Replace('\\', '/');

            switch (jsonPatchPath)
            {
                case "/":
                {
                    return String.Empty;
                }
                default:
                {
                    return '$' + jsonPatchPath.Replace('/', '.');
                }
            }
        }

        static string GetPathParent(string path)
        {
            ReadOnlySpan<char> parent = GetPathParent(path.AsSpan());
            if (parent.Length == path.Length)
                return path;

            return new String(parent);
        }

        static ReadOnlySpan<char> GetPathParent(ReadOnlySpan<char> path)
        {
            path = path.TrimEnd('/');

            if (path.IsEmpty)
                return path;

            int lastSeparatorIndex = path.LastIndexOf('/');
            if (lastSeparatorIndex == -1)
                return path;
            
            return path[..lastSeparatorIndex];
        }
    }
}
