using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeesDashboard.Models.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesDashboard.Controllers {
  public class AccountsController : Controller {
    private static readonly HttpClient HttpClient = new HttpClient();

    [Authorize]
    public async Task<IActionResult> Index() {
      HttpResponseMessage response = await HttpClient.GetAsync("https://core-2.azurewebsites.net/api/Accounts");
      List<Account> accounts = await response.Content.ReadAsAsync<List<Account>>();
      return View(accounts);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id) {
      HttpResponseMessage response = await HttpClient.GetAsync($"https://core-2.azurewebsites.net/api/Accounts/{id}");
      Account account = await response.Content.ReadAsAsync<Account>();
      return View(account);
    }
  }
}