using Atcom.Domain;
using Atcom.Domain.Repository;

namespace Atcom.Persistence.Interfaces
{
    public interface IClientRepository
    {
        Task<GetClientsRepository> GetClients(Paginate paginate);

        Task<GetClientsRepository> GetClientsByProcedure(Paginate paginate);
    }
}
