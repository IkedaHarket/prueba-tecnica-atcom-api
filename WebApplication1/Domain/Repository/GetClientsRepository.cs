namespace Atcom.Domain.Repository
{
    public class GetClientsRepository
    {
        public List<Client> Clients {  get; set; } = new List<Client>();
        public int TotalPages { get; set; }

    }
}
