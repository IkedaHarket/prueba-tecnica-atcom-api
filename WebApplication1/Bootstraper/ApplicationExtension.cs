using Atcom.Application.Business;
using Atcom.Application.Interfaces;

namespace Atcom.Bootstraper
{
    public static class ApplicationExtension
    {
        public static IServiceCollection RegisterApplicationExtension(this IServiceCollection services)
        {
            services.AddTransient<IClientBusiness, ClientBusiness>();

            return services;
        }
    }
}
