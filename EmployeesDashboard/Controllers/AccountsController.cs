using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeesDashboard.Models;
using EmployeesDashboard.Models.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesDashboard.Controllers {
  public class AccountsController : Controller {
    private static readonly HttpClient HttpClient = new HttpClient();

    [Authorize]
    public async Task<IActionResult> Index() {
      HttpResponseMessage response = await HttpClient.GetAsync("https://localhost:6001/api/Accounts");
      List<Account> accounts = await response.Content.ReadAsAsync<List<Account>>();
      return View(accounts);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id) {
      HttpResponseMessage response = await HttpClient.GetAsync($"https://localhost:6001/api/Accounts/{id}");
      Account account = await response.Content.ReadAsAsync<Account>();
      return View(account);
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    public IActionResult Create() {
      return View();
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AccountCreateModel accountModel) {
      HttpResponseMessage response = await HttpClient.PostAsJsonAsync("https://localhost:6001/api/Accounts/", accountModel);
      if (response.IsSuccessStatusCode) {
        return RedirectToAction(nameof(AccountsController.Index), "Accounts");
      }
      ModelState.AddModelError("", "Could not create new Account");
      return View();
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    public async Task<IActionResult> Delete(int id) {
      HttpResponseMessage response = await HttpClient.GetAsync($"https://localhost:6001/api/Accounts/{id}");
      Account account = await response.Content.ReadAsAsync<Account>();
      return View(account);
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAction(int id) {
      HttpResponseMessage response = await HttpClient.DeleteAsync($"https://localhost:6001/api/Accounts/{id}");
      if (response.IsSuccessStatusCode) return RedirectToAction(nameof(AccountsController.Index), "Accounts");
      return View();
    }
  }
}