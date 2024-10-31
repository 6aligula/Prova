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

    // Obtener todos los clientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        var clients = await _clientRepository.GetAllClientsAsync();
        return Ok(clients);
    }

    // Obtener un cliente por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClientById(int id)
    {
        var client = await _clientRepository.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }

    // Crear un nuevo cliente
    [HttpPost]
    public async Task<ActionResult<Client>> AddClient(Client client)
    {
        client.CreatedAt = DateTime.Now;
        var newClientId = await _clientRepository.AddClientAsync(client);
        client.Id = newClientId;
        return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
    }

    // Actualizar un cliente existente
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, Client client)
    {
        if (id != client.Id)
        {
            return BadRequest("Client ID mismatch");
        }

        var clientExists = await _clientRepository.GetClientByIdAsync(id);
        if (clientExists == null)
        {
            return NotFound();
        }

        var result = await _clientRepository.UpdateClientAsync(client);
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
        var clientExists = await _clientRepository.GetClientByIdAsync(id);
        if (clientExists == null)
        {
            return NotFound();
        }

        var result = await _clientRepository.DeleteClientAsync(id);
        if (!result)
        {
            return StatusCode(500, "Error deleting client");
        }

        return NoContent();
    }
}
