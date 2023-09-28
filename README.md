# University Issue VCs, Company HR verify VCs

[![.NET](https://github.com/swiss-ssi-group/TrinsicV2AspNetCore/actions/workflows/dotnet.yml/badge.svg)](https://github.com/swiss-ssi-group/TrinsicV2AspNetCore/actions/workflows/dotnet.yml)

## Use Case

As a university administrator, I want to create BBS+ verifiable credentials templates for university diplomas.

As a student, I want to authenticate using my university account (OpenID Connect Code flow with PKCE), create my verifiable credential and download this credential to my SSI Wallet.

As an HR employee, I want to verify the job candidate has a degree from this university. The HR employee needs verification but does not require to see the data.

- The sample creates University diplomas as BBS+ verifiable credentials.
- The credentials will be issued using OIDC to a candidate mobile wallet.
- The Issuer uses a trust registry so that the verifier can validate that the VC is authentic.
- The verifier will use BBS+ selective disclosure to validate a diploma.
- The university application requires an admin zone and a student zone.

## PoC

Validate that the flow can be implemented using this trinsic.id ID-Tech platform

Provide a .NET Core example

In a second phase, the trusted registry will be used and implemented with an improved verification process


## Debugging, Setup

### TrinsicV2WebWallet

```json
```

### CompanyXHumanResources

The verifier credentials

```json
"TrinsicCompanyXHumanResourcesOptions": {
    "Ecosystem": "--in-youe-secrets--",
    "ApiKey": "--in-youe-secrets--"
},
```

### Univeristy

The University application requires the trinsic uni credentials, the data for the issuer wallet and a SQL database to store the data.

An Azure App registration with a web setup is used to setup the application authentication. (OpenID Connect confidential client code flow with PKCE)

```json
"TrinsicOptions": {
    "Ecosystem": "--in-your-user-secrets--",
    "ApiKey": "--in-your-user-secrets--"
    "IssuerAuthToken": "--in-your-user-secrets--",
    "IssuerWalletId": "--in-your-user-secrets--",
},
"AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "--domain-name-azuread-tenant--",
    "TenantId": "--your-azuread-tenant-id--",
    "ClientId": "--client_id-azure-app-registration--",
    "CallbackPath": "/signin-oidc"
    //"ClientSecret": "--in-your-secrets--"
},
```

## Database

```
Add-Migration "init"

Update-Database
```

## Links

https://dashboard.trinsic.id/ecosystem

https://github.com/trinsic-id/sdk

https://docs.trinsic.id/dotnet/

https://www.youtube.com/watch?v=yKeIx3iE1WM
