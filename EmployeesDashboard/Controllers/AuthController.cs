using System;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesDashboard.Controllers {
  public class AuthController : Controller {
    private readonly IMapper _mapper;
    private readonly UserManager<Employee> _userManager;
    private readonly SignInManager<Employee> _signInManager;

    public AuthController(IMapper mapper, UserManager<Employee> userManager, SignInManager<Employee> signInManager) {
      _mapper = mapper;
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null) {
      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(EmployeeLoginModel employeeModel, string returnUrl) {
      if (!ModelState.IsValid) return View(employeeModel);

      var result = await _signInManager.PasswordSignInAsync(employeeModel.Username, employeeModel.Password, employeeModel.RememberMe, false);

      if (result.Succeeded) return RedirectToLocal(returnUrl);

      ModelState.AddModelError("", "Invalid Username or password");
      return View();
    }

    private IActionResult RedirectToLocal(string returnUrl) {
      if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
      return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public IActionResult Register() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(EmployeeRegistrationModel employeeModel) {
      if (!ModelState.IsValid) return View();

      var employee = _mapper.Map<Employee>(employeeModel);
      var result = await _userManager.CreateAsync(employee, employeeModel.Password);

      if (!result.Succeeded) {
        foreach (var error in result.Errors) {
          ModelState.TryAddModelError(error.Code, error.Description);
        }
        return View(employeeModel);
      }
      await _userManager.AddToRoleAsync(employee, employeeModel.Role);
      return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() {
      await _signInManager.SignOutAsync();
      return RedirectToAction(nameof(HomeController.Index), "Home");
    }
  }

  public class MappingProfile : Profile {
    public MappingProfile() {
      CreateMap<EmployeeRegistrationModel, Employee>();
    }
  }
}