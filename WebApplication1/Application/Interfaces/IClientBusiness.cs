using Atcom.Domain;
using Atcom.Application.Dto;

namespace Atcom.Application.Interfaces
{
    public interface IClientBusiness
    {
        Task<GetClientsDto> GetClients(Paginate paginate);
        Task<GetClientsDto> GetClientsByProcedure(Paginate paginate);
    }
}
