// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#pragma warning disable AZPROVISION001

using Aspire.Hosting.Azure;

using Azure.Provisioning.WebPubSub;

namespace Aspire.Hosting.ApplicationModel;

/// <summary>
/// Represents an Azure Web PubSub resource.
/// </summary>
/// <param name="name">The name of the resource.</param>
/// <param name="configureConstruct">Callback to populate the construct with Azure resources.</param>
public class AzureWebPubSubResource(string name, Action<ResourceModuleConstruct> configureConstruct) :
    AzureConstructResource(name, configureConstruct),
    IResourceWithConnectionString
{
    internal List<(string Name, Action<IResourceBuilder<AzureWebPubSubResource>, ResourceModuleConstruct, WebPubSubHub>? Configure)> HubSettings { get; } = [];

    /// <summary>
    /// Gets the "endpoint" output reference from the bicep template for Azure Web PubSub.
    /// </summary>
    public BicepOutputReference Endpoint => new("endpoint", this);

    /// <summary>
    /// Gets the connection string template for the manifest for Azure Web PubSub.
    /// </summary>
    public ReferenceExpression ConnectionStringExpression => ReferenceExpression.Create($"{Endpoint}");
}
