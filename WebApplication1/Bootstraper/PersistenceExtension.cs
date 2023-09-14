using Atcom.Persistence.Interfaces;
using Atcom.Persistence.Repository;

namespace Atcom.Bootstraper
{
    public static class PersistenceExtension
    {
        public static IServiceCollection RegisterRepositoryExtension(this IServiceCollection services)
        {

            services.AddTransient<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
