# Orleans Cluster on Azure Container Apps

## Setup

Fork this repository to your own GitHub organization. 

Create an Azure Service Principal

```bash
az ad sp create-for-rbac --sdk-auth --role contributor --scopes /subscription/<your-subscription-id>
```

Copy the resultant JSON written to the screen. 

```json
{
  "clientId": "",
  "clientSecret": "",
  "subscriptionId": "",
  "tenantId": "",
  "activeDirectoryEndpointUrl": "https://login.microsoftonline.com/",
  "resourceManagerEndpointUrl": "https://brazilus.management.azure.com",
  "activeDirectoryGraphResourceId": "https://graph.windows.net/",
  "sqlManagementEndpointUrl": "https://management.core.windows.net:8443/",
  "galleryEndpointUrl": "https://gallery.azure.com",
  "managementEndpointUrl": "https://management.core.windows.net"
}
```

Create a new GitHub secret in your fork of this repository named `AzureSPN`. Paste the JSON returned from the Azure CLI into this new secret. 

> Note: Never save the JSON to disk, for it will enable anyone who obtains this JSON code to create or edit resources in your Azure subscription. 

Create a new local branch named `provision`.

```bash
git checkout -b provision
```

Open the `.github/workflows/provision.yml` file and find this section:

```yaml
env:
  RESOURCE_GROUP_NAME: orleansoncontainerapps
  REGION: eastus
```

Change the `RESOURCE_GROUP_NAME` variable to be what you'd like your Azure resource group to be named. 

```yaml
env:
  RESOURCE_GROUP_NAME: bradyg-sampleapp
  REGION: eastus
```

Commit the changes and push them back to GitHub, on the `provision` branch you just created. 

```bash
git add .
git commit -m 'provisioning'
git push origin provision
```

Then browse to the `Actions` tab in your GitHub repository to see if the `provision` CI/CD process started.

Once the `provision` process completes, create a local branch named `deploy`. Find the `deploy.yml` GitHub workflow file in the local repository, specifically this code which sets the resource group and Azure Container Registry variable names. 

```yaml
env:
  CONTAINER_APP_RESOURCE_GROUP_NAME: orleansoncontainerapps
  CONTAINER_REGISTRY_LOGIN_SERVER: orleansoncontainerapps.azurecr.io
```

Customize these values so they match the Azure resource names you just created.

```yaml
env:
  CONTAINER_APP_RESOURCE_GROUP_NAME: orleansonaca01
  CONTAINER_REGISTRY_LOGIN_SERVER: orleansonaca01acr.azurecr.io
```

Set the `helloorleansregistry_USERNAME_FFFF` and `helloorleansregistry_PASSWORD_FFFF` GitHub secrets to match the Azure Container Registry's username and password. 

Commit your changes and push them to the `deploy` branch.

```bash
git add .
git commit -m 'deploying'
git push origin deploy
```

Then browse to the `Actions` tab in your GitHub repository to see if the `deploy` CI/CD process started.

