using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ERP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiUrl = "https://localhost:7143/api/client";

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // Obtener todos los clientes
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(ApiUrl);

            if (!response.IsSuccessStatusCode) return View(new List<Client>());

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var clients = await JsonSerializer.DeserializeAsync<IEnumerable<Client>>(await response.Content.ReadAsStreamAsync(), options);
            return View(clients);
        }


        // Ver formulario para crear un nuevo cliente
        public IActionResult Create()
        {
            return View();
        }

        // Crear un nuevo cliente
        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            var clientHttp = _clientFactory.CreateClient();
            client.CreatedAt = DateTime.Now;

            var content = new StringContent(JsonSerializer.Serialize(client), Encoding.UTF8, "application/json");
            var response = await clientHttp.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

            return View(client);
        }

        // Ver formulario para editar un cliente
        public async Task<IActionResult> Edit(int id)
        {
            var clientHttp = _clientFactory.CreateClient();
            var response = await clientHttp.GetAsync($"{ApiUrl}/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var client = await JsonSerializer.DeserializeAsync<Client>(await response.Content.ReadAsStreamAsync(), options);
            return View(client);
        }


        // Editar un cliente
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            var clientHttp = _clientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(client), Encoding.UTF8, "application/json");
            var response = await clientHttp.PutAsync($"{ApiUrl}/{id}", content);

            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

            return View(client);
        }

        // Eliminar un cliente
        public async Task<IActionResult> Delete(int id)
        {
            var clientHttp = _clientFactory.CreateClient();
            var response = await clientHttp.DeleteAsync($"{ApiUrl}/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}
