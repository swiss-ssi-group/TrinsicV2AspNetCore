using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Trinsic;
using University.Service;

namespace University.Pages;

public class CreateStudentDiplomaModel : PageModel
{
    private readonly UniversityServices _universityServices;

    [BindProperty]
    public List<SelectListItem>? DiplomaTemplates { get; set; }

    [BindProperty]
    public string DiplomaTemplateId { get; set; } = string.Empty;

    public CreateStudentDiplomaModel(UniversityServices universityServices)
    {
        _universityServices = universityServices;
    }

    public async Task OnGetAsync()
    {
        DiplomaTemplates = await _universityServices.GetUniversityDiplomaTemplates();
    }

    public async Task OnPostAsync()
    {
        var diploma = new Diploma
        {
            FirstName = "Damien",
            Email = "dd@d.ch",
            LastName = "Bod",
            DateOfBirth = "1998-05-23",
            DiplomaTitle = "Swiss SSI FH",
            DiplomaSpecialisation = "governance",
            DiplomaIssuedDate = DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        };

        await _universityServices.CreateStudentDiploma(diploma);
    }
}
