using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Logging;
using Serilog;
using University;

namespace AspNetCoreAzureLogging;

internal static class HostingExtensions
{
    private static IWebHostEnvironment? _env;
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        _env = builder.Environment;

        services.Configure<TrinsicOptions>(options => 
            configuration.Bind(TrinsicOptions.Trinsic, options));

        services.AddDistributedMemoryCache();

        services.AddMicrosoftIdentityWebAppAuthentication(configuration)
            .EnableTokenAcquisitionToCallDownstreamApi() // required to force PKCE
            .AddDistributedTokenCaches();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });
        
        services.AddRazorPages()
            .AddMicrosoftIdentityUI();

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        IdentityModelEventSource.ShowPII = true;

        app.UseSerilogRequestLogging();

        if (_env!.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();

        return app;
    }
}