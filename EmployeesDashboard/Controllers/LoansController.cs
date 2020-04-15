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
  public class LoansController : Controller {
    private static readonly HttpClient httpClient = new HttpClient();
    private readonly IMapper _mapper;
    private readonly string LoansAPIEndpoint = API.URL("Loans");

    public LoansController(IMapper mapper) {
      _mapper = mapper;
    }

    [Authorize]
    public async Task<IActionResult> Index() {
      HttpResponseMessage response = await httpClient.GetAsync(LoansAPIEndpoint);
      List<Loan> loans = await response.Content.ReadAsAsync<List<Loan>>();
      return View(loans);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id) {
      HttpResponseMessage response = await httpClient.GetAsync($"{LoansAPIEndpoint}/{id}");
      Loan loan = await response.Content.ReadAsAsync<Loan>();
      return View(loan);
    }

    [Authorize(Roles="Admin,DataMaintainer")]
    public IActionResult Create() {
      return View();
    }

    [Authorize(Roles = "Admin,DataMaintainer")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LoanCreateModel loanModel) {
      HttpResponseMessage response = await httpClient.PostAsJsonAsync(LoansAPIEndpoint, loanModel);
      Loan loan = await response.Content.ReadAsAsync<Loan>();
      return RedirectToAction(nameof(LoansController.Details), "Loans", new {id = loan.Id});
    }

    [Authorize(Roles="Admin,DataMaintainer")]
    public async Task<IActionResult> Edit(int id) {
      HttpResponseMessage response = await httpClient.GetAsync($"{LoansAPIEndpoint}/{id}");
      Loan loan = await response.Content.ReadAsAsync<Loan>();
      return View(loan);
    }
  }
}