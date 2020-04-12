using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesDashboard.Controllers {
  [Authorize(Roles="Admin")]
  public class EmployeesController : Controller {
    private readonly UserManager<Employee> _userManager;
    private readonly IMapper _mapper;

    public EmployeesController(UserManager<Employee> userManager, IMapper mapper) {
      _userManager = userManager;
      _mapper = mapper;
    }
    public IActionResult Index() {
      List<Employee> employees = _userManager.Users.ToList();
      List<EmployeeViewModel> employeeModels = _mapper.Map<List<Employee>, List<EmployeeViewModel>>(employees);
      return View(employeeModels);
    }
    public async Task<IActionResult> Details(string id) {
      Employee employee = await _userManager.FindByIdAsync(id);
      IList<string> employeeRoles = await _userManager.GetRolesAsync(employee);

      EmployeeViewModel employeeModel = _mapper.Map<EmployeeViewModel>(employee);
      employeeModel.Roles = employeeRoles;
      return View(employeeModel);
    }

    public async Task<IActionResult> Delete(string id) {
      Employee employee = await _userManager.FindByIdAsync(id);
      EmployeeViewModel employeeModel = _mapper.Map<EmployeeViewModel>(employee);
      return View(employeeModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAction(string id) {
      Employee employee = await _userManager.FindByIdAsync(id);
      await _userManager.DeleteAsync(employee);
      return RedirectToAction(nameof(EmployeesController.Index));
    }
  }
}