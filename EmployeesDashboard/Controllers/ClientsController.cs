using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesDashboard.Data;
using EmployeesDashboard.Models;
using EmployeesDashboard.Models.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesDashboard.Controllers {
  public class ClientsController : Controller {
    private static readonly HttpClient httpClient = new HttpClient();
    private readonly IMapper _mapper;

    private readonly string ClientsAPIEndpoint = API.URL("Clients");

    public ClientsController(IMapper mapper) {
      _mapper = mapper;
    }

    [Authorize]
    public async Task<IActionResult> Index() {
      HttpResponseMessage response = await httpClient.GetAsync(ClientsAPIEndpoint);
      List<Client> clients = await response.Content.ReadAsAsync<List<Client>>();
      return View(clients);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id) {
      HttpResponseMessage response = await httpClient.GetAsync($"{ClientsAPIEndpoint}/{id}");
      Client client = await response.Content.ReadAsAsync<Client>();
      return View(client);
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    public IActionResult Create() {
      return View();
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientCreateModel clientModel) {
      if (!ModelState.IsValid) return View();

      HttpResponseMessage response = await httpClient.PostAsJsonAsync(ClientsAPIEndpoint, clientModel);
      Client client = await response.Content.ReadAsAsync<Client>();
      return RedirectToAction(nameof(ClientsController.Details), "Clients", new { id = client.Id });
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    public async Task<IActionResult> Edit(int id) {
      HttpResponseMessage response = await httpClient.GetAsync($"{ClientsAPIEndpoint}/{id}");
      Client client = await response.Content.ReadAsAsync<Client>();
      return View(client);
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClientEditModel clientModel) {
      if (!ModelState.IsValid) return View();

      HttpResponseMessage getResponse = await httpClient.GetAsync($"{ClientsAPIEndpoint}/{id}");
      Client ogClient = await getResponse.Content.ReadAsAsync<Client>();

      // Client mappedClient = _mapper.Map(clientModel, ogClient);
      ogClient.FirstName = clientModel.FirstName;
      ogClient.LastName = clientModel.LastName;
      ogClient.Email = clientModel.Email;
      ogClient.Password = clientModel.Password;

        HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{ClientsAPIEndpoint}/{id}", ogClient);
      try {
        response.EnsureSuccessStatusCode();
        return RedirectToAction(nameof(ClientsController.Index));
      } catch (System.Exception) {
        ModelState.AddModelError("", await response.Content.ReadAsStringAsync());
        return View();
      }
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    public async Task<IActionResult> Delete(int id) {
      HttpResponseMessage response = await httpClient.GetAsync($"{ClientsAPIEndpoint}/{id}");
      Client client = await response.Content.ReadAsAsync<Client>();
      return View(client);
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAction(int id) {
      HttpResponseMessage response = await httpClient.DeleteAsync($"{ClientsAPIEndpoint}/{id}");
      if (response.IsSuccessStatusCode) return RedirectToAction(nameof(ClientsController.Index), "Clients");
      // errors
      return View();
    }
  }
}