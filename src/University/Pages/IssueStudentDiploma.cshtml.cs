using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    [BindProperty]
    public List<SelectListItem>? DiplomaTemplates { get; set; }

    [BindProperty]
    public string DiplomaTemplateId { get; set; } = string.Empty;

    public string CredentialOfferUrl { get; set; } = string.Empty;

    public IssueStudentDiplomaModel(UniversityServices universityServices)
    {
        _universityServices = universityServices;
    }

    public async Task OnGetAsync()
    {
        DiplomaTemplates = await _universityServices.GetUniversityDiplomaTemplates();
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

        var templateId = Convert.ToInt32(DiplomaTemplateId);

        var response = await _universityServices.IssuerStudentDiplomaCredentialOffer(diploma, templateId);

        CredentialOfferUrl = response!.ShareUrl;
    }
}
