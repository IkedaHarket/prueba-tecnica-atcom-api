using Atcom.Bootstraper;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Atcom.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es-CL");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("es-CL") };
                options.RequestCultureProviders = new List<IRequestCultureProvider>();
            });

            var cultureInfo = new CultureInfo("es-CL");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            services.AddControllers();

            //Bootstrapper
            services.RegisterApplicationExtension();
            services.RegisterInfraestructureExtension(Configuration);
            services.RegisterRepositoryExtension();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Content-Disposition"));

            string pathBase = Configuration["API_BASE_PATH"];
            logger.LogInformation("PATH BASE = {pathBase}", pathBase);

            if (!string.IsNullOrWhiteSpace(pathBase)) app.UsePathBase($"/{pathBase.TrimStart('/')}");
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"{pathBase}/swagger/v1/swagger.json", "Atcom.API v1"));
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}

