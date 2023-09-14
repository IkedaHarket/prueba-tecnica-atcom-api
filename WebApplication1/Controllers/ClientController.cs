using Atcom.Application.Dto;
using Atcom.Application.Interfaces;
using Atcom.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Atcom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController
    {
        private readonly IClientBusiness _clientBusiness;

        public ClientController( IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetClientsDto))]
        public async Task<IActionResult> GetClients(Paginate paginate)
        {
            var response = await _clientBusiness.GetClients(paginate);
            return new OkObjectResult(response);
        }

        [HttpPost]
        [Route("By-Procedure")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetClientsDto))]
        public async Task<IActionResult> GetClientsByProcedure(Paginate paginate)
        {
            var response = await _clientBusiness.GetClientsByProcedure(paginate);
            return new OkObjectResult(response);
        }
    }
}
