# University Issue VCs

[![.NET](https://github.com/swiss-ssi-group/TrinsicV2AspNetCore/actions/workflows/dotnet.yml/badge.svg)](https://github.com/swiss-ssi-group/TrinsicV2AspNetCore/actions/workflows/dotnet.yml)

## Use Case

As a university administrator, I want to create BBS+ verifiable credentials templates for university diplomas.

As a student, I want to authenticate using my university account (OpenID Connect Code flow with PKCE), create my verifiable credential and download this credential to my SSI Wallet.

As an HR employee, I want to verify the job candidate has a degree from this university. The HR employee needs verification but does not require to see the data.

- The sample creates University diplomas as BBS+ verifiable credentials.
- The credentials will be issued using OIDC to a candidate mobile wallet.
- The Issuer uses a trust registry so that the verifier can validate that the VC is authentic.
- The verifier will use BBS+ selective disclosure to validate a diploma.
- The University application requires an admin zone and a student zone.

## PoC

Validate that the flow can be implemented used this ID-Tech

In a second phase, the trusted registry will be used and implemented.

## Notes

- Does OIDC even work with Trinsic?
- Wallets from other providers do not work
- Trinsic wallet does not work
- Platform documentation do not match the APIs (options.AuthToken = configuration["TrinsicOptions:ApiKey"];)
- No clear docs how to implement this basic flow using OIDC
- Weak user authentication

## Links

https://dashboard.trinsic.id/ecosystem

https://github.com/trinsic-id/sdk

https://docs.trinsic.id/dotnet/

https://www.youtube.com/watch?v=yKeIx3iE1WM
