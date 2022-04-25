# Orleans Cluster on Azure Container Apps

## Setup

Create a Service Principal

```bash
az ad sp create-for-rbac --role contributor --scopes /subscription/<your-subscription-id>
```

Copy the resultant JSON written to the screen to a text file. 

```json
{
  "appId": "74xx305-xxxx-xxxx-xxxx-7076xx25exx8",
  "displayName": "azure-cli-xxxx-xx-xx-xx-xx-xx",
  "password": "secret_pwd-asdasdasd_dontshare",
  "tenant": "72f988bf-xxxx-xxxx-xxxx-2dxxd011xxxx"
}
```

Paste the JSON into a new GitHub secret named `AzureSPN`.

Clone the repository. 

Create a new branch named `provision`.
