using Atcom.Domain;

namespace Atcom.Application.Dto
{
    public class GetClientsDto
    {
        public List<Client> Clients { get; set; } = new List<Client>();
        public int TotalPages { get; set; }
    }
}
