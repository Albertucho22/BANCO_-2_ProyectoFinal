using System;
using System.Collections.Generic;
using System.Text;
using EmployeesDashboard.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeesDashboard.Data {
  public class ApplicationDbContext : IdentityDbContext<Employee> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
    }
  }
}
