using ERPSystem.API.DTOs;

namespace ERPSystem.API.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(int id);
        Task<int> AddClientAsync(ClientDto clientDto);
        Task<bool> UpdateClientAsync(ClientDto clientDto);
        Task<bool> DeleteClientAsync(int id);
    }
}
