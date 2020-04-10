using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeesDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using EmployeesDashboard.Models.API;

namespace EmployeesDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient client = new HttpClient();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Clients() {
            HttpResponseMessage response = await client.GetAsync("https://core-2.azurewebsites.net/api/Clients");
            List<Client> clients = await response.Content.ReadAsAsync<List<Client>>();
            return View(clients);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
