using AutoMapper;
using ERPSystem.API.DTOs;
using ERPSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }

    // Obtener todos los clientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
    {
        var clientsDto = await _clientService.GetAllClientsAsync();
        return Ok(clientsDto);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetClientById(int id)
    {
        var clientDto = await _clientService.GetClientByIdAsync(id);
        if (clientDto == null)
        {
            return NotFound();
        }
        return Ok(clientDto);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> AddClient(ClientDto clientDto)
    {
        var newClientId = await _clientService.AddClientAsync(clientDto);
        clientDto.Id = newClientId;
        return CreatedAtAction(nameof(GetClientById), new { id = clientDto.Id }, clientDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, ClientDto clientDto)
    {
        if (id != clientDto.Id)
        {
            return BadRequest("Client ID mismatch");
        }

        var clientExists = await _clientService.GetClientByIdAsync(id);
        if (clientExists == null)
        {
            return NotFound();
        }

        var result = await _clientService.UpdateClientAsync(clientDto);
        if (!result)
        {
            return StatusCode(500, "Error updating client");
        }

        return NoContent();
    }

    // Eliminar un cliente
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var clientExists = await _clientService.GetClientByIdAsync(id);
        if (clientExists == null)
        {
            return NotFound();
        }

        var result = await _clientService.DeleteClientAsync(id);
        if (!result)
        {
            return StatusCode(500, "Error deleting client");
        }

        return NoContent();
    }
}
