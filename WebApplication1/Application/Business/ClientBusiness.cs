using Atcom.Application.Dto;
using Atcom.Application.Interfaces;
using Atcom.Domain;
using Atcom.Persistence.Interfaces;

namespace Atcom.Application.Business
{
    public class ClientBusiness: IClientBusiness
    {
        private readonly IClientRepository _clientBusiness;

        public ClientBusiness(IClientRepository clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        public async Task<GetClientsDto> GetClients(Paginate paginate)
        {
            var result = await _clientBusiness.GetClients(paginate);

            return new GetClientsDto { Clients = result.Clients, TotalPages = result.TotalPages };
        }

        public async Task<GetClientsDto> GetClientsByProcedure(Paginate paginate)
        {
            var result = await _clientBusiness.GetClientsByProcedure(paginate);

            return new GetClientsDto { Clients = result.Clients, TotalPages = result.TotalPages };
        }
    }
}
