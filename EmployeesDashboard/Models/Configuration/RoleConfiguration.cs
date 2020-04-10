using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeesDashboard.Models.Configuration {
  public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole> {
    public void Configure(EntityTypeBuilder<IdentityRole> builder) {
      builder.HasData(
        new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
        new IdentityRole { Name = "DataMaintainer", NormalizedName = "DATAMAINTAINER" },
        new IdentityRole { Name = "DataConsultant", NormalizedName = "DATACONSULTANT" }
      );
    }
  }
}