using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trinsic;
using Trinsic.Services.TrustRegistry.V1;
using Trinsic.Services.UniversalWallet.V1;
using Trinsic.Services.VerifiableCredentials.Templates.V1;
using Trinsic.Services.VerifiableCredentials.V1;
using University.Service;

namespace University;

public class UniversityServices
{
    private readonly TrinsicService _trinsicService;
    private readonly IConfiguration _configuration;
    private readonly UniversityDbContext _context;

    public UniversityServices(TrinsicService trinsicService,
        IConfiguration configuration,
        UniversityDbContext context)
    {
        _trinsicService = trinsicService;
        _configuration = configuration;
        _context = context;
    }

    public async Task<string?> GetUniversityDiplomaTemplateId(int id)
    {
        var template = await _context.DiplomaTemplates
            .FirstOrDefaultAsync(t => t.Id == id);

        return template!.TemplateId;
    }

    public async Task<GetCredentialTemplateResponse> GetUniversityDiplomaTemplate(string universityTemplateId)
    {
        // Auth token from trinsic.id root API KEY provider
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:ApiKey"];

        var templateResponse = await _trinsicService.Template
            .GetAsync(new GetCredentialTemplateRequest
            {
                Id = universityTemplateId
            });

        return templateResponse;
    }

    public async Task<List<SelectListItem>> GetUniversityDiplomaTemplates()
    {
        var items = await _context.DiplomaTemplates.Select(s => new SelectListItem
        {
            Text = s.Name,
            Value = s.Id.ToString(CultureInfo.InvariantCulture)
        }).ToListAsync();

        return items;
    }

    /// <summary>
    /// Templates are issued from our university wallet
    /// </summary>
    public async Task<RegisterMemberResponse> RegisterIssuer(string schemaUri)
    {
        var issuerWalletId = _configuration["TrinsicOptions:IssuerWalletId"];

        // Auth token from trinsic.id root API KEY provider
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:ApiKey"];

        return await _trinsicService.TrustRegistry.RegisterMemberAsync(new()
        {
            WalletId = issuerWalletId,
            SchemaUri = schemaUri,
        });
    }

    public async Task CreateStudentDiploma(Diploma diploma)
    {
        await _context.AddAsync(diploma);
        await _context.SaveChangesAsync();
    }

    public async Task<CreateCredentialOfferResponse?> IssuerStudentDiplomaCredentialOffer(Diploma diploma, int universityDiplomaTemplateId)
    {
        var templateId = await GetUniversityDiplomaTemplateId(universityDiplomaTemplateId);
        // get the template from the id-tech solution
        var templateResponse = await GetUniversityDiplomaTemplate(templateId!);

        // Auth token from University issuer wallet
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:IssuerAuthToken"];

        var response = await _trinsicService.Credential.CreateCredentialOfferAsync(
            new CreateCredentialOfferRequest
            {
                TemplateId = templateResponse.Template.Id,
                ValuesJson = JsonSerializer.Serialize(diploma),
                GenerateShareUrl = true
            });

        return response;
    }

    public async Task<CreateCredentialTemplateResponse> CreateDiplomaTemplate(CreateCredentialTemplateRequest diplomaTemplate)
    {
        // Auth token from University issuer wallet
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:IssuerAuthToken"];

        var template = await _trinsicService.Template
            .CreateAsync(diplomaTemplate);

        await _context.AddAsync(new DiplomaTemplate
        {
            SchemaUri = template.Data.SchemaUri,
            Name = template.Data.Name,
            TemplateId = template.Data.Id
        });
        await _context.SaveChangesAsync();

        return template;
    }

    public async Task<CreateWalletResponse?> CreateUniversityWalletToIssueDiplomas(CreateWalletRequest request)
    {
        // Auth token from trinsic.id root API KEY provider
        _trinsicService.Options.AuthToken = _configuration["TrinsicOptions:ApiKey"];

        var createWalletResponse = await _trinsicService.Wallet.CreateWalletAsync(request);

        return createWalletResponse;
    }

  
}
