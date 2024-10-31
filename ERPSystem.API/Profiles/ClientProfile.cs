using AutoMapper;
using ERPSystem.API.DTOs;
using ERPSystem.API.Models;

namespace ERPSystem.API.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();
        }
    }
} 