using Microsoft.AspNetCore.Mvc.RazorPages;
using Trinsic.Services.VerifiableCredentials.Templates.V1;

namespace University.Pages;

/// <summary>
/// Create Credential Template for a new universaty diploma
/// https://docs.trinsic.id/reference/services/template-service/
/// </summary>
public class CreateNewDiplomaModel : PageModel
{
    private readonly UniversityServices _universityServices;

    public CreateNewDiplomaModel(UniversityServices universityServices)
    {
        _universityServices = universityServices;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        CreateCredentialTemplateRequest createRequest = new()
        {
            Name = "Diploma Credential for Swiss Self Sovereign Identity SSI",
            Title = "Diploma SSI degree",
            Description = "A University Diploma Credential for Swiss Self Sovereign Identity SSI",
            AllowAdditionalFields = false,
            Fields =
            {
                { "firstName", new() { Title = "First Name", Description = "first name" } },
                { "lastName", new() { Title = "Last Name", Description = "Surname" } },
                { "dateOfBirth", new() { Title = "Date of Birth", Description = "Date of birth" } },
                { "diplomaTitle", new() { Title = "Title of Diploma", Description = "Title of Diploma" } },
                { "diplomaSpecialisation", new() { Title = "Specialisation", Description = "Specialisation", Optional=true } },
                { "diplomaIssuedDate", new() { Title = "Diploma issued date", Description = "date when diploma was issued, or awrded" } },
                
            },
            FieldOrdering =
            {
                { "firstName", new() { Order = 0, Section = "Name" } },
                { "lastName", new() { Order = 1, Section = "Name" } },
                { "dateOfBirth", new() { Order = 2, Section = "Name" } },
                { "diplomaTitle", new() { Order = 3, Section = "Diploma" } },
                { "diplomaSpecialisation", new() { Order = 4, Section = "Diploma" } },
                { "diplomaIssuedDate", new() { Order = 5, Section = "Diploma" } }
            }, 
            AppleWalletOptions = new()
            {
                PrimaryField = "firstName",
                SecondaryFields = { "lastName", "dateOfBirth"},
                AuxiliaryFields = {  "diplomaTitle", "diplomaIssuedDate" }
            }
        };

        var createdTemplate = await _universityServices.CreateDiplomaTemplate(createRequest);

        // We need to allow the univeristy wallet to issue the VCs using this template
        await _universityServices.RegisterIssuer(createdTemplate.Data.SchemaUri);
    }
}
