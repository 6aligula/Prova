using System.Text;
using System.Text.Json;
using ERP.Web.Models;
using ERPSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;


namespace ERP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiUrl;

        public HomeController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _apiUrl = configuration["ApiSettings:BaseUrl"]; // Leer URL de la API desde appsettings.json
        }

        // Obtener todos los clientes
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(_apiUrl);

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
            if (!ModelState.IsValid)
            {
                return View(client); // Devuelve la vista con errores de validación de frontend
            }

            var clientHttp = _clientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(client), Encoding.UTF8, "application/json");
            var response = await clientHttp.PostAsync($"{_apiUrl}", content);


            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    //Debug.WriteLine("Error Content: " + errorContent);

                    try
                    {
                        // Deserializa el JSON para acceder a la propiedad `errors`
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorContent, options);


                        if (errorResponse?.Errors != null)
                        {
                            foreach (var error in errorResponse.Errors)
                            {
                                foreach (var message in error.Value)
                                {
                                    ModelState.AddModelError(error.Key, message);
                                }
                            }
                        }
                    }
                    catch (JsonException)
                    {
                        ModelState.AddModelError(string.Empty, "Error de validación no esperado.");
                    }
                }

                return View(client); // Muestra los errores en la vista
            }

            return RedirectToAction(nameof(Index));
        }


        // Ver formulario para editar un cliente
        public async Task<IActionResult> Edit(int id)
        {
            var clientHttp = _clientFactory.CreateClient();
            var response = await clientHttp.GetAsync($"{_apiUrl}/{id}");

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
            if (!ModelState.IsValid)
            {
                return View(client); // Devuelve la vista con errores de validación
            }

            var clientHttp = _clientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(client), Encoding.UTF8, "application/json");
            var response = await clientHttp.PutAsync($"{_apiUrl}/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        // Deserializa usando las opciones configuradas
                        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorContent, options);

                        if (errorResponse?.Errors != null)
                        {
                            foreach (var error in errorResponse.Errors)
                            {
                                foreach (var message in error.Value)
                                {
                                    ModelState.AddModelError(error.Key, message);
                                }
                            }
                        }
                    }
                    catch (JsonException )
                    {
                        ModelState.AddModelError(string.Empty, "Error de validación no esperado.");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Error al actualizar el cliente.";
                }

                return View(client); // Muestra los errores en la vista
            }

            return RedirectToAction(nameof(Index));
        }


        // Eliminar un cliente
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var clientHttp = _clientFactory.CreateClient();
            var response = await clientHttp.DeleteAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Error al eliminar el cliente.";
            }

            return RedirectToAction(nameof(Index));
        }

        // Ver detalles de un cliente 
        public async Task<IActionResult> Details(int id)
        {
            var clientHttp = _clientFactory.CreateClient();
            var response = await clientHttp.GetAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var client = await JsonSerializer.DeserializeAsync<Client>(await response.Content.ReadAsStreamAsync(), options);
            return View(client);
        }
    }
}
