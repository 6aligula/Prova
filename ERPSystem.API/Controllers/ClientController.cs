using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Api.Repositories;
using ERPSystem.API.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly ClientRepository _clientRepository;

    public ClientController(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        var clients = await _clientRepository.GetAllClientsAsync();
        return Ok(clients);
    }
}
