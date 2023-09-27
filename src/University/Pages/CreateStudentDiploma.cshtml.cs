using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Service;

namespace University.Pages;

public class CreateStudentDiplomaModel : PageModel
{
    private readonly UniversityServices _universityServices;

    [BindProperty]
    public List<SelectListItem>? DiplomaTemplates { get; set; }

    [BindProperty]
    public Diploma Diploma { get; set; } = new Diploma();

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
        Diploma.DiplomaIssuedDate = DateTime.UtcNow
            .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        await _universityServices.CreateStudentDiploma(Diploma);
    }
}
