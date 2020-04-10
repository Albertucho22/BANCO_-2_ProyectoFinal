using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeesDashboard.Models.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesDashboard.Controllers {
  public class ClientsController : Controller {
    private static readonly HttpClient httpClient = new HttpClient();

    [Authorize]
    public async Task<IActionResult> Index() {
      HttpResponseMessage response = await httpClient.GetAsync("https://core-2.azurewebsites.net/api/Clients");
      List<Client> clients = await response.Content.ReadAsAsync<List<Client>>();
      return View(clients);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id) {
      HttpResponseMessage response = await httpClient.GetAsync($"https://core-2.azurewebsites.net/api/Clients/{id}");
      Client client = await response.Content.ReadAsAsync<Client>();
      return View(client);
    }
  }
}