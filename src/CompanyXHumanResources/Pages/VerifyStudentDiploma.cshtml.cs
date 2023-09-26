using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrinsicV2WebWallet.Pages;

public class VerifyStudentDiplomaModel : PageModel
{
    private readonly DiplomaVerifyService _diplomaVerifyService;

    public bool UniversityCredentialIsValid { get; set; }

    public VerifyStudentDiplomaModel(DiplomaVerifyService diplomaVerifyService)
    {
        _diplomaVerifyService = diplomaVerifyService;
    }

    public void Get()
    {
    }

    public async Task OnPostAsync()
    {
        var userCreateProof = "{\"@context\":[\"https://www.w3.org/2018/credentials/v1\",\"https://w3id.org/bbs/v1\",{\"@vocab\":\"https://trinsic.cloud/peaceful-booth-zrpufxfp6l3c/\"}],\"id\":\"urn:vc\",\"type\":[\"DiplomaCredentialForSwissSelfSovereignIdentitySSI\",\"VerifiableCredential\"],\"credentialSchema\":{\"id\":\"https://schema.trinsic.cloud/peaceful-booth-zrpufxfp6l3c/diploma-credential-for-swiss-self-sovereign-identity-ssi\",\"type\":\"JsonSchemaValidator2018\"},\"credentialStatus\":{\"id\":\"urn:revocation-registry:peaceful-booth-zrpufxfp6l3c:2aacQmX1s554b6JD4aByH9#0\",\"type\":\"RevocationList2020Status\",\"revocationListCredential\":\"urn:revocation-registry:peaceful-booth-zrpufxfp6l3c:2aacQmX1s554b6JD4aByH9\",\"revocationListIndex\":\"0\"},\"credentialSubject\":{\"id\":\"urn:vc:subject:0\",\"DateOfBirth\":\"1998-05-23\",\"DiplomaIssuedDate\":\"2023-09-19\",\"DiplomaSpecialisation\":\"governance\",\"DiplomaTitle\":\"Swiss SSI FH\",\"FirstName\":\"Damien\",\"LastName\":\"Bod\"},\"issuanceDate\":\"2023-09-19T08:15:01.2684003Z\",\"issuer\":\"did:web:peaceful-booth-zrpufxfp6l3c.connect.trinsic.cloud:zV9t25XybyBV7qEB1v6u9Bb\",\"proof\":{\"type\":\"BbsBlsSignatureProof2020\",\"created\":\"2023-09-19T08:15:01Z\",\"nonce\":\"/XPfsMHM8RJv5OcxNWiENrhTKcY86TFOTghNS9LK1P4=\",\"proofPurpose\":\"assertionMethod\",\"proofValue\":\"ABUf//\\u002BObrYIZicyLWV2JbgKoE10TldD/3GWID/PL2TGL94EdSUzZ9JFMLXoT6P9MzIKpNq0glviJ\\u002Btj7Ij3G6n5SjIKvvX9OKR4GwafN6qKtZ\\u002BvQCEpfVBqZbWHx9oUZCwYtXe3G\\u002Bz5lBPoVtmLJidHfR\\u002BYDWupBEhq9afsOHaL\\u002B8nchyj7GLftHoJyVu7cYZhx548AAAB0mX6qWPEyQMYpUMj7QbNHLMuvOJl51t3iVHQXiAT6Hc\\u002BB1lmB7\\u002Bwnqn75BJa6LNM/AAAAAgpSvFNGpyQombkRs2xQ4BBcFJrhizyAuqOE5M0jJdKIJhOEO0f0\\u002BAtU/5KPP6YdVEFPO6eXAx/AgbAjRHgrAqmgKPpFN9ixqtmxKISPOI\\u002BkIH5bU28W88RorgRWq5hUqSum9mkq66Dt1lStg9UBd\\u002BkAAAACKXNGwmqofwI1Fmc2fx5sMOGsldmc1MxYkJTETansKKVDTZCkRnw1EfuDq4UCc1AHqu2E9fugh9bQJIEX1ui4Tg==\",\"verificationMethod\":\"did:web:peaceful-booth-zrpufxfp6l3c.connect.trinsic.cloud:zV9t25XybyBV7qEB1v6u9Bb#6_QJhVbI4CuYgKtLEPpkRpXszWxNl05CGTG7WitO-Qg\"}}"; 

        var verifyUserCreatedProof = await _diplomaVerifyService
            .Verify(userCreateProof);

        var isValid = verifyUserCreatedProof.IsValid;

        UniversityCredentialIsValid = isValid;
    }
}
