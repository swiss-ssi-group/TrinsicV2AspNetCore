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
    public string DiplomaTemplateId { get; set; } = string.Empty;

    [BindProperty]
    public List<SelectListItem> Diplomas { get; set; } = new List<SelectListItem>();

    public string CredentialOfferUrl { get; set; } = string.Empty;

    public IssueStudentDiplomaModel(UniversityServices universityServices)
    {
        _universityServices = universityServices;
    }

    public async Task OnGetAsync()
    {
        var user = User!.Identity!.Name;
        Diplomas = await _universityServices.GetUniversityDiplomas(user);
    }

    public async Task OnPostAsync()
    {
        var diploma = await _universityServices.GetUniversityDiploma(DiplomaTemplateId);

        var tid = Convert.ToInt32(DiplomaTemplateId, CultureInfo.InvariantCulture);

        var response = await _universityServices
            .IssuerStudentDiplomaCredentialOffer(diploma, tid);

        CredentialOfferUrl = response!.ShareUrl;
    }
}
