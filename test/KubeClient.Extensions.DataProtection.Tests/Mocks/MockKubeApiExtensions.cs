using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

using NJ = Newtonsoft.Json;

namespace KubeClient.Extensions.DataProtection.Tests.Mocks
{
    using ApiMetadata;
    using Models;

    /// <summary>
    ///     Extensions for configuring <see cref="MockKubeApi"/>.
    /// </summary>
    public static class MockKubeApiExtensions
    {
        /// <summary>
        ///     Configure the mock API to handle all standard requests for the specified resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model (derived from <see cref="KubeResourceListV1"/>) used to represent a list of <typeparamref name="TResource"/>s.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resources">
        ///     A <see cref="ConcurrentDictionary{TKey, TValue}"/> that will be used as a backing store to represent resource instances.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResources<TResource, TResourceList>(this WebApplication mockKubeApiApp, ConcurrentDictionary<string, TResource> resources)
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>, new()
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            Subject<TResource> watchSubject = new Subject<TResource>();

            mockKubeApiApp.Lifetime.ApplicationStopping.Register(() =>
            {
                using (watchSubject)
                {
                    watchSubject.OnCompleted();
                }
            });

            mockKubeApiApp.HandleResources<TResource, TResourceList>(
                listResources: (string resourceNamespace) =>
                {
                    IEnumerable<TResource> matchingResources = resources.Values;

                    if (resourceNamespace != null)
                    {
                        matchingResources = matchingResources.Where(
                            resource => resource.Metadata?.Namespace == resourceNamespace
                        );
                    }

                    return matchingResources
                        .OrderBy(
                            resource => resource.Metadata?.Name ?? String.Empty
                        )
                        .ToList();
                },
                loadResource: (string resourceName, string resourceNamespace) =>
                {
                    string persistenceKey = MockKubeApi.GetResourceKey(resourceName, resourceNamespace);
                    if (resources.TryGetValue(persistenceKey, out TResource persistedResource))
                        return persistedResource;

                    return null;
                },
                saveResource: (TResource resource) =>
                {
                    string persistenceKey = MockKubeApi.GetResourceKey(resource.Metadata);
                    TResource persistedResource = resources.AddOrUpdate(persistenceKey,
                        addValue: resource,
                        updateValueFactory: (string persistenceKey, TResource updatedResource) => resource
                    );

                    return persistedResource;
                },
                watchSubject
            );

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle all standard requests for the specified resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model (derived from <see cref="KubeResourceListV1"/>) used to represent a list of <typeparamref name="TResource"/>s.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resources">
        ///     A <see cref="ConcurrentDictionary{TKey, TValue}"/> that will be used as a backing store to represent resource instances.
        /// </param>
        /// <param name="watchSubject">
        ///     A <see cref="ISubject{T}"/> that can be used to publish or observe resource-change notifications.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResources<TResource, TResourceList>(this WebApplication mockKubeApiApp, ConcurrentDictionary<string, TResource> resources, ISubject<TResource> watchSubject)
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>, new()
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            if (watchSubject == null)
                throw new ArgumentNullException(nameof(watchSubject));

            mockKubeApiApp.HandleResources<TResource, TResourceList>(
                listResources: (string resourceNamespace) =>
                {
                    IEnumerable<TResource> matchingResources = resources.Values;

                    if (resourceNamespace != null)
                    {
                        matchingResources = matchingResources.Where(
                            resource => resource.Metadata?.Namespace == resourceNamespace
                        );
                    }
                    
                    return matchingResources
                        .OrderBy(
                            resource => resource.Metadata?.Name ?? String.Empty
                        )
                        .ToList();
                },
                loadResource: (string resourceName, string resourceNamespace) =>
                {
                    string persistenceKey = MockKubeApi.GetResourceKey(resourceName, resourceNamespace);
                    if (resources.TryGetValue(persistenceKey, out TResource persistedResource))
                        return persistedResource;

                    return null;
                },
                saveResource: (TResource resource) =>
                {
                    string persistenceKey = MockKubeApi.GetResourceKey(resource.Metadata);
                    TResource persistedResource = resources.AddOrUpdate(persistenceKey,
                        addValue: resource,
                        updateValueFactory: (string persistenceKey, TResource updatedResource) => resource
                    );

                    return persistedResource;
                },
                watchSubject
            );

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle all standard requests for the specified resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model (derived from <see cref="KubeResourceListV1"/>) used to represent a list of <typeparamref name="TResource"/>s.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="listResources">
        ///     A delegate that is called to obtain a list of all resources of the specified type.
        ///     
        ///     <para>
        ///         Return value is a sequence of resources.
        ///     </para>
        /// </param>
        /// <param name="loadResource">
        ///     A delegate that is called to retrieve a single persisted resource of the specified type.
        ///     
        ///     <para>
        ///         Arguments are the name namespace of the target resource, return value is the persisted resource.
        ///     </para>
        /// </param>
        /// <param name="saveResource">
        ///     A delegate that is called to persist a single resource of the specified type.
        ///     
        ///     <para>
        ///         Argument is resource to persist, return value is the persisted resource.
        ///     </para>
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResources<TResource, TResourceList>(this WebApplication mockKubeApiApp, Func<string, List<TResource>> listResources, Func<string, string, TResource> loadResource, Func<TResource, TResource> saveResource, ISubject<TResource> watchSubject)
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>, new()
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (listResources == null)
                throw new ArgumentNullException(nameof(listResources));

            if (loadResource == null)
                throw new ArgumentNullException(nameof(loadResource));

            if (saveResource == null)
                throw new ArgumentNullException(nameof(saveResource));

            if (watchSubject == null)
                throw new ArgumentNullException(nameof(watchSubject));

            KubeApiMetadataCache apiMetadataCache = LoadMetadata<TResource>();

            (string resourceKind, string resourceApiVersion) = KubeObjectV1.GetKubeKind<TResource>();
            KubeApiMetadata resourceMetadata = apiMetadataCache.Get(resourceKind, resourceApiVersion);
            if (resourceMetadata == null)
                throw new InvalidOperationException($"Failed to load metadata for the resource type represented by model '{typeof(TResource).FullName}.'.");

            (string listKind, string listApiVersion) = KubeObjectV1.GetKubeKind<TResourceList>();
            
            KubeApiPathMetadata listPath = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.List);
            if (listPath == null)
                throw new InvalidOperationException($"Cannot determine the resource-list path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata getPath = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Get);
            if (getPath == null)
                throw new InvalidOperationException($"Cannot determine the resource-get path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata createPath = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Create);
            if (createPath == null)
                throw new InvalidOperationException($"Cannot determine the resource-create path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata updatePath = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Update);
            if (updatePath == null)
                throw new InvalidOperationException($"Cannot determine the resource-update path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata patchPath = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Patch);
            if (patchPath == null)
                throw new InvalidOperationException($"Cannot determine the resource-patch path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata watchPath = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Watch);
            if (watchPath == null)
                throw new InvalidOperationException($"Cannot determine the resource-create path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            // We need to handle namespaced paths differently because of how ASP.NET Core Minimal APIs does route matching / parameter binding on path segments.
            bool isNamespaced = listPath.IsNamespaced;
            if (isNamespaced)
            {
                mockKubeApiApp.HandleResourceList<TResource, TResourceList>(listPath.Path, (string resourceNamespace) =>
                {
                    List<TResource> resources = listResources(resourceNamespace);
                    if (resources == null)
                        throw new InvalidOperationException($"Resource-list handler for {resourceApiVersion}/{resourceKind} returned null.");

                    TResourceList resourceList = new TResourceList();
                    resourceList.Items.AddRange(resources);

                    return resourceList;
                });

                mockKubeApiApp.HandleResourceGet(getPath.Path, (string resourceName, string resourceNamespace) =>
                {
                    TResource resource = loadResource(resourceName, resourceNamespace);

                    return resource;
                });

                mockKubeApiApp.HandleResourcePatch(patchPath.Path, (string resourceName, string resourceNamespace, JsonPatchOperation[] patchRequest) =>
                {
                    TResource persistedResource = loadResource(resourceName, resourceNamespace);
                    if (persistedResource == null)
                        return null;

                    TResource patchedResource = persistedResource.ApplyJsonPatch(patchRequest);
                    persistedResource = saveResource(patchedResource);

                    watchSubject.OnNext(persistedResource);

                    return persistedResource;
                });
            }
            else
            {
                mockKubeApiApp.HandleResourceList<TResource, TResourceList>(listPath.Path, () =>
                {
                    string resourceNamespace = null;
                    List<TResource> resources = listResources(resourceNamespace);
                    if (resources == null)
                        throw new InvalidOperationException($"Resource-list handler for {resourceApiVersion}/{resourceKind} returned null.");

                    TResourceList resourceList = new TResourceList();
                    resourceList.Items.AddRange(resources);

                    return resourceList;
                });

                mockKubeApiApp.HandleResourceGet(getPath.Path, (string resourceName) =>
                {
                    string resourceNamespace = null;
                    TResource resource = loadResource(resourceName, resourceNamespace);

                    return resource;
                });

                mockKubeApiApp.HandleResourcePatch(patchPath.Path, (string resourceName, string resourceNamespace, JsonPatchOperation[] patchRequest) =>
                {
                    TResource persistedResource = loadResource(resourceName, resourceNamespace);
                    if (persistedResource == null)
                        return null;

                    TResource patchedResource = persistedResource.ApplyJsonPatch(patchRequest);
                    persistedResource = saveResource(patchedResource);

                    watchSubject.OnNext(persistedResource);

                    return persistedResource;
                });
            }
            
            mockKubeApiApp.HandleResourceCreate(createPath.Path, (TResource initialResource) =>
            {
                TResource persistedResource = saveResource(initialResource);
                watchSubject.OnNext(persistedResource);

                return persistedResource;
            });
            mockKubeApiApp.HandleResourceUpdate(updatePath.Path, (TResource updatedResource) =>
            {
                TResource persistedResource = saveResource(updatedResource);
                watchSubject.OnNext(persistedResource);

                return persistedResource;
            });
            mockKubeApiApp.HandleResourceWatch(watchPath.Path, watchSubject);

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle all standard (CRUD) requests for a single resource of the specified type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <param name="resourceNamespace">
        ///     The namespace of the target resource (if applicable).
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleSingleResource<TResource>(this WebApplication mockKubeApiApp, string resourceName, string resourceNamespace)
            where TResource : KubeResourceV1
        {
            if (String.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourceName)}.", nameof(resourceName));

            Subject<TResource> watchSubject = new Subject<TResource>();

            mockKubeApiApp.Lifetime.ApplicationStopping.Register(() =>
            {
                using (watchSubject)
                {
                    watchSubject.OnCompleted();
                }
            });

            mockKubeApiApp.HandleSingleResource(resourceName, resourceNamespace, watchSubject);

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle all standard (CRUD, Watch) requests for a single resource of the specified type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <param name="resourceNamespace">
        ///     The namespace of the target resource (if applicable).
        /// </param>
        /// <param name="watchSubject">
        ///     An <see cref="ISubject{T}"/> that can be used to publish / subscribe to resource-watch notifications for the target resource.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleSingleResource<TResource>(this WebApplication mockKubeApiApp, string resourceName, string resourceNamespace, ISubject<TResource> watchSubject)
            where TResource : KubeResourceV1
        {
            if (String.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourceName)}.", nameof(resourceName));

            TResource resourceState = null;

            KubeApiMetadataCache apiMetadataCache = LoadMetadata<TResource>();

            (string resourceKind, string resourceApiVersion) = KubeObjectV1.GetKubeKind<TResource>();
            KubeApiMetadata resourceMetadata = apiMetadataCache.Get(resourceKind, resourceApiVersion);
            if (resourceMetadata == null)
                throw new InvalidOperationException($"Failed to load metadata for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata getPathMetadata = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Get);
            if (getPathMetadata == null)
                throw new InvalidOperationException($"Cannot determine the resource-get path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata createPathMetadata = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Create);
            if (createPathMetadata == null)
                throw new InvalidOperationException($"Cannot determine the resource-create path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata updatePathMetadata = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Update);
            if (updatePathMetadata == null)
                throw new InvalidOperationException($"Cannot determine the resource-update path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata patchPathMetadata = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Patch);
            if (patchPathMetadata == null)
                throw new InvalidOperationException($"Cannot determine the resource-patch path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            KubeApiPathMetadata watchPathMetadata = resourceMetadata.FindApiPathPreferringNamespaced(KubeAction.Watch);
            if (watchPathMetadata == null)
                throw new InvalidOperationException($"Cannot determine the resource-create path for the resource type represented by model '{typeof(TResource).FullName}.'.");

            // We need to handle namespaced paths differently because of how ASP.NET Core Minimal APIs does route matching / parameter binding on path segments.
            bool isNamespaced = getPathMetadata.IsNamespaced;
            if (isNamespaced)
            {
                if (String.IsNullOrWhiteSpace(resourceNamespace))
                    throw new ArgumentException($"Must supply a valid resource namespace (resource type '{resourceApiVersion}/{resourceKind}' is namespaced).", nameof(resourceNamespace));

                mockKubeApiApp.HandleResourceGet(getPathMetadata.Path, (string requestedResourceName, string requestedResourceNamespace) =>
                {
                    if (requestedResourceName != resourceName || requestedResourceNamespace != resourceNamespace)
                        return null;

                    return resourceState;
                });

                mockKubeApiApp.HandleResourcePatch(patchPathMetadata.Path, (string requestedResourceName, string requestedResourceNamespace, JsonPatchOperation[] patchRequest) =>
                {
                    Assert.Equal(resourceName, requestedResourceName);
                    Assert.Equal(resourceNamespace, requestedResourceNamespace);

                    resourceState = resourceState.ApplyJsonPatch(patchRequest);

                    return resourceState;
                });
            }
            else
            {
                mockKubeApiApp.HandleResourceGet(getPathMetadata.Path, (string requestedResourceName) =>
                {
                    if (requestedResourceName != resourceName)
                        return null;

                    return resourceState;
                });

                mockKubeApiApp.HandleResourcePatch(patchPathMetadata.Path, (string requestedResourceName, JsonPatchOperation[] patchRequest) =>
                {
                    Assert.Equal(resourceName, requestedResourceName);

                    resourceState = resourceState.ApplyJsonPatch(patchRequest);

                    return resourceState;
                });
            }

            mockKubeApiApp.HandleResourceCreate(createPathMetadata.Path, (TResource initialResource) =>
            {
                Assert.NotNull(initialResource.Metadata);
                Assert.Equal(resourceName, initialResource.Metadata.Name);
                Assert.Equal(resourceNamespace, initialResource.Metadata.Namespace);

                resourceState = initialResource;

                return resourceState;
            });
            mockKubeApiApp.HandleResourceUpdate(updatePathMetadata.Path, (TResource updatedResource) =>
            {
                Assert.NotNull(updatedResource.Metadata);
                Assert.Equal(resourceName, updatedResource.Metadata.Name);
                Assert.Equal(resourceNamespace, updatedResource.Metadata.Namespace);

                resourceState = updatedResource;

                return resourceState;
            });

            mockKubeApiApp.HandleResourceWatch(watchPathMetadata.Path, watchSubject);

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle list requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model (derived from <see cref="KubeResourceListV1"/>) used to represent a list of <typeparamref name="TResource"/>s.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcesPath">
        ///     The path where resource-list requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     A handler delegate that returns all instances of the specified resource type.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceList<TResource, TResourceList>(this WebApplication mockKubeApiApp, string resourcesPath, MockKubeApi.ListResourcesHandler<TResource, TResourceList> handler)
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>, new()
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcesPath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcesPath)}.", nameof(resourcesPath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.HandleResourceListAsync<TResource, TResourceList>(resourcesPath, () =>
            {
                TResourceList response = handler();

                return Task.FromResult(response);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle list requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model (derived from <see cref="KubeResourceListV1"/>) used to represent a list of <typeparamref name="TResource"/>s.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcesPath">
        ///     The path where resource-list requests should be handled.
        /// </param>
        /// <param name="asyncHandler">
        ///     An asynchronous handler delegate that returns all instances of the specified resource type.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceList<TResource, TResourceList>(this WebApplication mockKubeApiApp, string resourcesPath, MockKubeApi.ListNamespacedResourcesHandler<TResource, TResourceList> handler)
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>, new()
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcesPath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcesPath)}.", nameof(resourcesPath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.HandleResourceListAsync<TResource, TResourceList>(resourcesPath, (string resourceNamespace) =>
            {
                TResourceList response = handler(resourceNamespace);

                return Task.FromResult(response);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle list requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model (derived from <see cref="KubeResourceListV1"/>) used to represent a list of <typeparamref name="TResource"/>s.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcesPath">
        ///     The path where resource-list requests should be handled.
        /// </param>
        /// <param name="asyncHandler">
        ///     An asynchronous handler delegate that returns all instances of the specified resource type.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceListAsync<TResource, TResourceList>(this WebApplication mockKubeApiApp, string resourcesPath, MockKubeApi.AsyncListResourcesHandler<TResource, TResourceList> asyncHandler)
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>, new()
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcesPath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcesPath)}.", nameof(resourcesPath));

            if (asyncHandler == null)
                throw new ArgumentNullException(nameof(asyncHandler));

            mockKubeApiApp.MapGet(resourcesPath, async (HttpRequest request) =>
            {
                ILogger logger = request.CreateLogger("MockKubeApi.ResourceListHandler");

                TResourceList responseBody = await asyncHandler();
                if (responseBody == null)
                {
                    return Results.Content(
                        content: NJ.JsonConvert.SerializeObject(
                            StatusV1.Failure($"Resource-list handler for '{resourcesPath}' returned null.",
                                reason: "InternalError",
                                code: StatusCodes.Status500InternalServerError
                            )
                        ),
                        contentType: "application/json",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                (string resourceKind, string resourceApiVersion) = KubeObjectV1.GetKubeKind<TResource>();

                logger.LogInformation("List {ResourceApiVersion}/{ResourceKind} resources: {ResourceCount} resource(s) found.",
                    resourceApiVersion,
                    resourceKind,
                    responseBody.Items.Count
                );

                return Results.Content(
                    content: NJ.JsonConvert.SerializeObject(responseBody),
                    contentType: "application/json",
                    statusCode: StatusCodes.Status200OK
                );
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle list requests for a (namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model (derived from <see cref="KubeResourceListV1"/>) used to represent a list of <typeparamref name="TResource"/>s.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcesPath">
        ///     The path where resource-list requests should be handled.
        /// </param>
        /// <param name="asyncHandler">
        ///     An asynchronous handler delegate that returns all instances of the specified resource type in a given namespace.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceListAsync<TResource, TResourceList>(this WebApplication mockKubeApiApp, string resourcesPath, MockKubeApi.AsyncListNamespacedResourcesHandler<TResource, TResourceList> asyncHandler)
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>, new()
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcesPath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcesPath)}.", nameof(resourcesPath));

            if (asyncHandler == null)
                throw new ArgumentNullException(nameof(asyncHandler));

            mockKubeApiApp.MapGet(resourcesPath, async (string @namespace, HttpRequest request) =>
            {
                ILogger logger = request.CreateLogger("MockKubeApi.ResourceListHandler");

                TResourceList responseBody = await asyncHandler(@namespace);
                if (responseBody == null)
                {
                    return Results.Content(
                        content: NJ.JsonConvert.SerializeObject(
                            StatusV1.Failure($"Resource-list handler for '{resourcesPath}' returned null.",
                                reason: "InternalError",
                                code: StatusCodes.Status500InternalServerError
                            )
                        ),
                        contentType: "application/json",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                (string resourceKind, string resourceApiVersion) = KubeObjectV1.GetKubeKind<TResource>();

                logger.LogInformation("List {ResourceApiVersion}/{ResourceKind} resources in namespace {ResourceNamespace}: {ResourceCount} resource(s) found.",
                    resourceApiVersion,
                    resourceKind,
                    @namespace,
                    responseBody.Items.Count
                );

                return MockApiResults.Ok(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle get requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-get requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     A handler delegate that returns the resource matching a given resource name (or <c>null</c>, if no resource matches that name).
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceGet<TResource>(this WebApplication mockKubeApiApp, string resourcePathTemplate, MockKubeApi.LoadResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePathTemplate))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePathTemplate)}.", nameof(resourcePathTemplate));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.HandleResourceGetAsync(resourcePathTemplate, (string resourceName) =>
            {
                TResource response = handler(resourceName);

                return Task.FromResult(response);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle get requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-get requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     A handler delegate that returns the resource matching a given resource name and namespace (or <c>null</c>, if no resource matches that name and namespace).
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceGet<TResource>(this WebApplication mockKubeApiApp, string resourcePathTemplate, MockKubeApi.LoadNamespacedResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePathTemplate))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePathTemplate)}.", nameof(resourcePathTemplate));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.HandleResourceGetAsync(resourcePathTemplate, (string resourceName, string resourceNamespace) =>
            {
                TResource response = handler(resourceName, resourceNamespace);

                return Task.FromResult(response);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle get requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-get requests should be handled.
        /// </param>
        /// <param name="asyncHandler">
        ///     An asynchronous handler delegate that returns the resource matching a given resource name (or <c>null</c>, if no resource matches that name).
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceGetAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePathTemplate, MockKubeApi.AsyncLoadResourceHandler<TResource> asyncHandler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePathTemplate))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePathTemplate)}.", nameof(resourcePathTemplate));

            if (asyncHandler == null)
                throw new ArgumentNullException(nameof(asyncHandler));

            mockKubeApiApp.MapGet(resourcePathTemplate, async (string name) =>
            {
                TResource responseBody = await asyncHandler(name);
                if (responseBody == null)
                {
                    (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();

                    return MockApiResults.NotFound(
                        StatusV1.Failure($"{apiVersion}/{kind} resource '{name}' was not found.", "NotFound")
                    );
                }

                return MockApiResults.Ok(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle get requests for a (namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-get requests should be handled.
        /// </param>
        /// <param name="asyncHandler">
        ///     An asynchronous handler delegate that returns the resource matching a given resource name and namespace (or <c>null</c>, if no resource matches that name and namespace).
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceGetAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePathTemplate, MockKubeApi.AsyncLoadNamespacedResourceHandler<TResource> asyncHandler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePathTemplate))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePathTemplate)}.", nameof(resourcePathTemplate));

            if (asyncHandler == null)
                throw new ArgumentNullException(nameof(asyncHandler));

            mockKubeApiApp.MapGet(resourcePathTemplate, async (string name, string @namespace) =>
            {
                TResource responseBody = await asyncHandler(name, @namespace);
                if (responseBody == null)
                {
                    (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
                    string resourceName = !String.IsNullOrWhiteSpace(@namespace) ? $"{@namespace}/{name}" : name;

                    return MockApiResults.NotFound(
                        StatusV1.Failure($"{apiVersion}/{kind} resource '{resourceName}' was not found.", "NotFound")
                    );
                }

                return MockApiResults.Ok(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle create requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-create requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     A handler delegate that persists state for the supplied resource, returning the persisted resource state.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceCreate<TResource>(this WebApplication mockKubeApiApp, string resourcePathTemplate, MockKubeApi.SaveResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePathTemplate))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePathTemplate)}.", nameof(resourcePathTemplate));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.HandleResourceCreateAsync(resourcePathTemplate, (TResource request) =>
            {
                TResource responseBody = handler(request);

                return Task.FromResult(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle create requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-create requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     An asynchronous handler delegate that persists state for the supplied resource, returning the persisted resource state.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceCreateAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePath, MockKubeApi.AsyncSaveResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
            
            mockKubeApiApp.MapPost(resourcePath, async (HttpRequest request) =>
            {
                TResource requestBody = request.ReadAsNewtonsoftJson<TResource>();
                TResource responseBody = await handler(requestBody);
                if (responseBody == null)
                {
                    (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
                    string resourceName = Path.GetFileNameWithoutExtension(resourcePath);

                    return MockApiResults.NotFound(
                        StatusV1.Failure($"{apiVersion}/{kind} resource '{resourceName}' was not found.", "NotFound")
                    );
                }

                return MockApiResults.Ok(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle update requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-create requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     A handler delegate that persists state for the supplied resource, returning the persisted resource state.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceUpdate<TResource>(this WebApplication mockKubeApiApp, string resourcePath, MockKubeApi.SaveResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.HandleResourceUpdateAsync(resourcePath, (TResource request) =>
            {
                TResource response = handler(request);

                return Task.FromResult(response);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle update requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-create requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     An asynchronous handler delegate that persists state for the supplied resource, returning the persisted resource state.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceUpdateAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePath, MockKubeApi.AsyncSaveResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.MapPut(resourcePath, async (HttpRequest request) =>
            {
                TResource requestBody = request.ReadAsNewtonsoftJson<TResource>();
                TResource responseBody = await handler(requestBody);
                if (responseBody == null)
                {
                    (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
                    string resourceName = Path.GetFileNameWithoutExtension(resourcePath);

                    return MockApiResults.NotFound(
                        StatusV1.Failure($"{apiVersion}/{kind} resource '{resourceName}' was not found.", "NotFound")
                    );
                }

                return MockApiResults.Ok(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle patch requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-create requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     An asynchronous handler delegate that applies one or more patch operations to persisted resource state, returning the (patched) persisted resource state.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourcePatch<TResource>(this WebApplication mockKubeApiApp, string resourcePath, MockKubeApi.PatchResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.HandleResourcePatchAsync(resourcePath, (string name, JsonPatchOperation[] patchRequest) =>
            {
                TResource responseBody = handler(name, patchRequest);

                return Task.FromResult(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle patch requests for a (namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-create requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     A handler delegate that applies one or more patch operations to persisted resource state, returning the (patched) persisted resource state.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourcePatch<TResource>(this WebApplication mockKubeApiApp, string resourcePath, MockKubeApi.PatchNamespacedResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.HandleResourcePatchAsync(resourcePath, (string name, string @namespace, JsonPatchOperation[] patchRequest) =>
            {
                TResource responseBody = handler(name, @namespace, patchRequest);

                return Task.FromResult(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle patch requests for a (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-create requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     An asynchronous handler delegate that applies one or more patch operations to persisted resource state, returning the (patched) persisted resource state.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourcePatchAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePath, MockKubeApi.AsyncPatchResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.MapPatch(resourcePath, async (string name, HttpRequest request) =>
            {
                ILogger logger = request.CreateLogger("MockKubeApi.ResourcePatchHandler");

                JsonPatchOperation[] patchRequest = request.ReadAsJsonPatchOperations();
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    JArray patchRequestJson = request.ReadAsNewtonsoftJArray();
                    logger.LogDebug("Patch request: {PatchRequest}", patchRequestJson.ToString(NJ.Formatting.None));
                }

                TResource responseBody = await handler(name, patchRequest);
                if (responseBody == null)
                {
                    (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();

                    return MockApiResults.NotFound(
                        StatusV1.Failure($"{apiVersion}/{kind} resource '{name}' was not found.", "NotFound")
                    );
                }

                return MockApiResults.Ok(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle patch requests for a (namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-create requests should be handled.
        /// </param>
        /// <param name="handler">
        ///     An asynchronous handler delegate that applies one or more patch operations to persisted resource state, returning the (patched) persisted resource state.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourcePatchAsync<TResource>(this WebApplication mockKubeApiApp, string resourcePath, MockKubeApi.AsyncPatchNamespacedResourceHandler<TResource> handler)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourcePath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourcePath)}.", nameof(resourcePath));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mockKubeApiApp.MapPatch(resourcePath, async (string name, string @namespace, HttpRequest request) =>
            {
                ILogger logger = request.CreateLogger("MockKubeApi.ResourcePatchHandler");

                JsonPatchOperation[] patchRequest = request.ReadAsJsonPatchOperations();
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    JArray patchRequestJson = request.ReadAsNewtonsoftJArray();
                    logger.LogDebug("Patch request: {PatchRequest}", patchRequestJson.ToString(NJ.Formatting.None));
                }

                TResource responseBody = await handler(name, @namespace, patchRequest);
                if (responseBody == null)
                {
                    (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();

                    return MockApiResults.NotFound(
                        StatusV1.Failure($"{apiVersion}/{kind} resource '{name}' was not found.", "NotFound")
                    );
                }

                return MockApiResults.Ok(responseBody);
            });

            return mockKubeApiApp;
        }

        /// <summary>
        ///     Configure the mock API to handle watch requests for a (namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model (derived from <see cref="KubeResourceV1"/>) used to represent the resource.
        /// </typeparam>
        /// <param name="mockKubeApiApp">
        ///     The <see cref="WebApplication"/> that will host the mock API.
        /// </param>
        /// <param name="resourcePathTemplate">
        ///     The path template where resource-watch requests should be handled.
        /// </param>
        /// <param name="resourceStates">
        ///     An <see cref="IObservable{T}"/> sequence of resource states that will be published to watchers.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
        public static WebApplication HandleResourceWatch<TResource>(this WebApplication mockKubeApiApp, string resourceWatchPath, IObservable<TResource> resourceStates)
            where TResource : KubeResourceV1
        {
            if (mockKubeApiApp == null)
                throw new ArgumentNullException(nameof(mockKubeApiApp));

            if (String.IsNullOrWhiteSpace(resourceWatchPath))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourceWatchPath)}.", nameof(resourceWatchPath));

            if (resourceStates == null)
                throw new ArgumentNullException(nameof(resourceStates));

            mockKubeApiApp.MapGet(resourceWatchPath, async context =>
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
                        string resourceJson = NJ.JsonConvert.SerializeObject(resource);
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

        /// <summary>
        ///     Create a named logger.
        /// </summary>
        /// <param name="request">
        ///     The <see cref="HttpRequest"/> whose <see cref="IServiceProvider"/> will be used to create the logger.
        /// </param>
        /// <param name="name">
        ///     The name of the logger to create.
        /// </param>
        /// <returns>
        ///     The logger.
        /// </returns>
        public static ILogger CreateLogger(this HttpRequest request, string name)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(name)}.", nameof(name));

            return request.HttpContext.CreateLogger(name);
        }

        /// <summary>
        ///     Create a named logger.
        /// </summary>
        /// <param name="request">
        ///     The <see cref="HttpContext"/> whose request-level <see cref="IServiceProvider"/> will be used to create the logger.
        /// </param>
        /// <param name="name">
        ///     The name of the logger to create.
        /// </param>
        /// <returns>
        ///     The logger.
        /// </returns>
        public static ILogger CreateLogger(this HttpContext httpContext, string name)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(name)}.", nameof(name));


            return httpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(name);
        }

        /// <summary>
        ///     Deserialise the request body from JSON (using Newtonsoft.Json).
        /// </summary>
        /// <typeparam name="TBody">
        ///     The type that the request body should be deserialised into.
        /// </typeparam>
        /// <param name="request">
        ///     A <see cref="HttpRequest"/> representing the request.
        /// </param>
        /// <returns>
        ///     The deserialised request body (as <typeparamref name="TBody"/>).
        /// </returns>
        public static TBody ReadAsNewtonsoftJson<TBody>(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (StreamReader requestReader = new StreamReader(request.Body, leaveOpen: true))
            using (NJ.JsonTextReader requestJsonReader = new NJ.JsonTextReader(requestReader))
            {
                return NJ.JsonSerializer.Create().Deserialize<TBody>(requestJsonReader);
            }
        }

        /// <summary>
        ///     Read the request body as JSON (using Newtonsoft.Json).
        /// </summary>
        /// <param name="request">
        ///     A <see cref="HttpRequest"/> representing the request.
        /// </param>
        /// <returns>
        ///     The deserialised request body (as a <see cref="JToken"/>).
        /// </returns>
        public static JToken ReadAsNewtonsoftJToken(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (StreamReader requestReader = new StreamReader(request.Body, leaveOpen: true))
            using (NJ.JsonTextReader requestJsonReader = new NJ.JsonTextReader(requestReader))
            {
                return JToken.Load(requestJsonReader);
            }
        }

        /// <summary>
        ///     Read the request body as JSON (using Newtonsoft.Json).
        /// </summary>
        /// <param name="request">
        ///     A <see cref="HttpRequest"/> representing the request.
        /// </param>
        /// <returns>
        ///     The deserialised request body (as a <see cref="JObject"/>).
        /// </returns>
        public static JObject ReadAsNewtonsoftJObject(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (StreamReader requestReader = new StreamReader(request.Body, leaveOpen: true))
            using (NJ.JsonTextReader requestJsonReader = new NJ.JsonTextReader(requestReader))
            {
                return JObject.Load(requestJsonReader);
            }
        }

        /// <summary>
        ///     Read the request body as JSON (using Newtonsoft.Json).
        /// </summary>
        /// <param name="request">
        ///     A <see cref="HttpRequest"/> representing the request.
        /// </param>
        /// <returns>
        ///     The deserialised request body (as a <see cref="JArray"/>).
        /// </returns>
        public static JArray ReadAsNewtonsoftJArray(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (StreamReader requestReader = new StreamReader(request.Body, leaveOpen: true))
            using (NJ.JsonTextReader requestJsonReader = new NJ.JsonTextReader(requestReader))
            {
                return JArray.Load(requestJsonReader);
            }
        }

        /// <summary>
        ///     Read the request body as as <see cref="JsonPatchOperation"/>s.
        /// </summary>
        /// <param name="request">
        ///     A <see cref="HttpRequest"/> representing the request.
        /// </param>
        /// <returns>
        ///     The deserialised request body (as an array of <see cref="JsonPatchOperation"/>s).
        /// </returns>
        public static JsonPatchOperation[] ReadAsJsonPatchOperations(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (StreamReader requestReader = new StreamReader(request.Body, leaveOpen: true))
            using (NJ.JsonTextReader requestJsonReader = new NJ.JsonTextReader(requestReader))
            {
                return NJ.JsonSerializer.Create().Deserialize<JsonPatchOperation[]>(requestJsonReader);
            }
        }

        /// <summary>
        ///     Apply one or more JSON Patch operations to the target resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The target resource type.
        /// </typeparam>
        /// <param name="resource">
        ///     The target resource.
        /// </param>
        /// <param name="patchOperations">
        ///     A <see cref="JArray"/> containing the JSON Patch operations to perform.
        /// </param>
        /// <returns>
        ///     The patched <typeparamref name="TResource"/>.
        /// </returns>
        public static TResource ApplyJsonPatch<TResource>(this TResource resource, params JsonPatchOperation[] patchOperations)
            where TResource : KubeResourceV1
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            if (patchOperations == null)
                throw new ArgumentNullException(nameof(patchOperations));

            JObject resourceJson = JObject.FromObject(resource);

            foreach (JsonPatchOperation patchOperation in patchOperations)
            {
                patchOperation.EnsureValid();

                JToken target = resourceJson.SelectJsonPatchPath(patchOperation.Path);
                
                switch (patchOperation.Kind)
                {
                    case JsonPatchOperationKind.Delete:
                    {
                        if (target != null)
                            target.Remove();

                        break;
                    }
                    case JsonPatchOperationKind.Replace:
                    {
                        switch ((target, patchOperation.Value))
                        {
                            case (JObject targetObject, JObject replacementObject):
                            {
                                targetObject.Replace(replacementObject);

                                break;
                            }
                            case (null, JObject newObject):
                            {
                                string parentPath = Path.GetDirectoryName(patchOperation.Path);
                                
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
                                        string propertyName = Path.GetFileName(patchOperation.Path);
                                        if (String.IsNullOrWhiteSpace(propertyName))
                                            throw new NJ.JsonException($"Failed to apply JSON-patch '{patchOperation.Kind}' operation (cannot determine property name from target path '{patchOperation.Path}').");

                                        parentObject[propertyName] = newObject;
                                        
                                        break;
                                    }
                                    case null:
                                    {
                                        throw new NJ.JsonException($"Failed to apply JSON-patch '{patchOperation.Kind}' operation (parent of target token '{patchOperation.Path}' was not found).");
                                    }
                                    default:
                                    {
                                        throw new NJ.JsonException($"Failed to apply JSON-patch '{patchOperation.Kind}' operation (parent of target token is of type '{parent.Type}', which is not a container).");
                                    }
                                }

                                break;
                            }
                            default:
                            {
                                throw new NJ.JsonException($"Failed to apply JSON-patch '{patchOperation.Kind}' operation (target token '{patchOperation.Path}' is of type {target.Type}, which is not supported when the patch value is of type '{patchOperation.Value.Type}').");
                            }
                        }

                        break;
                    }
                    default:
                    {
                        throw new NJ.JsonException($"Failed to apply JSON-patch '{patchOperation.Kind}' operation (not a supported operation).");
                    }
                }
            }

            TResource patchedResource;
            using (NJ.JsonReader patchedJsonReader = resourceJson.CreateReader())
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

        static KubeApiMetadataCache LoadMetadata<TResource>()
            where TResource : KubeResourceV1
        {
            KubeApiMetadataCache apiMetadataCache = new KubeApiMetadataCache();
            apiMetadataCache.LoadFromMetadata(
                typeof(PodV1).Assembly
            );
            apiMetadataCache.LoadFromMetadata(
                typeof(TResource).Assembly
            );

            return apiMetadataCache;
        }

        static KubeApiPathMetadata FindApiPathPreferringNamespaced(this KubeApiMetadata apiMetadata, KubeAction action)
        {
            if (apiMetadata == null)
                throw new ArgumentNullException(nameof(apiMetadata));

            List<KubeApiPathMetadata> candidatePaths = apiMetadata.PathMetadata
                .Where(
                    path => path.SupportsAction(action)
                )
                .ToList();

            KubeApiPathMetadata path =
                candidatePaths.FirstOrDefault(path => path.IsNamespaced)
                ??
                candidatePaths.FirstOrDefault(path => !path.IsNamespaced);

            return path;
        }
    }
}
