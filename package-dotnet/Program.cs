using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main (string[] args)
    {
        // string subscriptionId = "c8ec073b-e265-49d2-95d2-65ee82096ecf";
        string resourceGroupName = "my-sdk-dp";
        AzureLocation location = AzureLocation.UKWest;

        // Authenticate with Azure
        var credential = new DefaultAzureCredential();

        // Initialize Azure ARM Client
        var armClient = new ArmClient(credential);

        // Create or get access to the resource group
        SubscriptionResource subscription = await armClient.GetDefaultSubscriptionAsync();
        ResourceGroupCollection resourceGroups = subscription.GetResourceGroups();
        ResourceGroupData resourceGroupData = new ResourceGroupData(location);
        ArmOperation<ResourceGroupResource> operation = await resourceGroups.CreateOrUpdateAsync(WaitUntil.Completed, resourceGroupName, resourceGroupData);
        ResourceGroupResource resourceGroup = operation.Value;

        Console.WriteLine("Resource Group created successfully");
    }
}