using EmployeesDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesDashboard.Controllers {
public class EmployeesController : Controller
{
    private readonly UserManager<Employee> _userManager;

    public EmployeesController(UserManager<Employee> userManager)
    {
        _userManager = userManager;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Index() {
      var users = _userManager.Users;
      return View(users);
    }
}
}