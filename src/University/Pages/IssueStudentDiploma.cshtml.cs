using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trinsic;
using University.Service;

namespace University.Pages;

/// <summary>
/// Create Credential Template for a new universaty diploma
/// https://docs.trinsic.id/reference/services/template-service/
/// https://docs.trinsic.id/reference/services/credential-service/
/// https://docs.trinsic.id/guide/issuance/
/// </summary>
public class IssueStudentDiplomaModel : PageModel
{
    private readonly UniversityServices _universityServices;

    public string CredentialOfferUrl { get; set; } = string.Empty;

    public IssueStudentDiplomaModel(UniversityServices universityServices)
    {
        _universityServices = universityServices;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        // TODO get data from a database using ID from authenticated student

        var diploma = new Diploma
        {
            FirstName = "Damien",
            LastName = "Bod",
            DateOfBirth = "1998-05-23",
            DiplomaTitle = "Swiss SSI FH",
            DiplomaSpecialisation = "governance",
            DiplomaIssuedDate = DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        };

        var response = await _universityServices.IssuerStudentDiplomaCredentialOffer(diploma);

        CredentialOfferUrl = response!.ShareUrl;
    }
}
