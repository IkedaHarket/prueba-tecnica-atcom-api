using Atcom.Domain;
using Atcom.Domain.Repository;
using Atcom.Persistence.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace Atcom.Persistence.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConfiguration _configuration;

        public ClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GetClientsRepository> GetClients(Paginate paginate)
        {
            try
            {
                string conexionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(conexionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                        SELECT C.[IdCliente], C.[Nombre], C.[Telefono], C.Email, C.[CodigoPais], P.[Descripcion],
                        (SELECT CEILING(COUNT(*) * 1.0 / @Limit) FROM [atcom].[dbo].[Clientes]) AS TotalPages
                        FROM [atcom].[dbo].[Clientes] as C
                        INNER JOIN [atcom].[dbo].[Pais] as P ON C.[CodigoPais] = P.[Codigo]
                        ORDER BY C.[IdCliente]
                        OFFSET @Page * @Limit ROWS 
                        FETCH NEXT @Limit ROWS ONLY;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Page", paginate.Page - 1);
                        command.Parameters.AddWithValue("@Limit", paginate.Limit);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            GetClientsRepository result = new GetClientsRepository();
                            List<Client> clients = new List<Client>();

                            while (await reader.ReadAsync())
                            {
                                clients.Add(new Client
                                {
                                    Id = (int)reader.GetDecimal(reader.GetOrdinal("IdCliente")),
                                    Name = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Phone = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Country = new Country
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("CodigoPais")),
                                        Name = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    },
                                });

                                result.TotalPages = Convert.ToInt32(reader["TotalPages"]);
                            }

                            result.Clients = clients;
                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<GetClientsRepository> GetClientsByProcedure(Paginate paginate)
        {
            try
            {
                string conexionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(conexionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("GetPaginatedClients", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure; 

                        command.Parameters.AddWithValue("@Page", paginate.Page);
                        command.Parameters.AddWithValue("@Limit", paginate.Limit);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            GetClientsRepository result = new GetClientsRepository();
                            List<Client> clients = new List<Client>();

                            while (await reader.ReadAsync())
                            {
                                clients.Add(new Client
                                {
                                    Id = (int)reader.GetDecimal(reader.GetOrdinal("IdCliente")),
                                    Name = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Phone = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Country = new Country
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("CodigoPais")),
                                        Name = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    },
                                });

                                result.TotalPages = Convert.ToInt32(reader["TotalPages"]);
                            }

                            result.Clients = clients;
                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
