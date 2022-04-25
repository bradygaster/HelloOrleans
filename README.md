# Orleans Cluster on Azure Container Apps

## Setup

Fork this repository to your own GitHub organization. 

Create an Azure Service Principal

```bash
az ad sp create-for-rbac --role contributor --scopes /subscription/<your-subscription-id>
```

Copy the resultant JSON written to the screen. 

```json
{
  "appId": "74xx305-xxxx-xxxx-xxxx-7076xx25exx8",
  "displayName": "azure-cli-xxxx-xx-xx-xx-xx-xx",
  "password": "secret_pwd-42please_dontshare",
  "tenant": "72f988bf-xxxx-xxxx-xxxx-2dxxd011xxxx"
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