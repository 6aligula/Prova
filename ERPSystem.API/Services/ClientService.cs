using AutoMapper;
using ERPSystem.API.DTOs;
using ERPSystem.API.Models;
using ERPSystem.API.Repositories;

namespace ERPSystem.API.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllClientsAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<int> AddClientAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            client.CreatedAt = DateTime.Now;
            return await _clientRepository.AddClientAsync(client);
        }

        public async Task<bool> UpdateClientAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            return await _clientRepository.UpdateClientAsync(client);
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            return await _clientRepository.DeleteClientAsync(id);
        }
    }
}
