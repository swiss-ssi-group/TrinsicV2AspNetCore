using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trinsic;
using Trinsic.Services.VerifiableCredentials.Templates.V1;

namespace University.Pages;

/// <summary>
/// Create Credential Template for a new universaty diploma
/// https://docs.trinsic.id/reference/services/template-service/
/// </summary>
public class IssueDiplomaModel : PageModel
{
    private readonly TrinsicService _trinsicService;

    public IssueDiplomaModel(TrinsicService trinsicService)
    {
        _trinsicService = trinsicService;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        
    }
}
