using AutoMapper;
using EmployeesDashboard.Models.API;

namespace EmployeesDashboard.Models.Configuration {
  public class MappingProfile : Profile {
    public MappingProfile() {
      CreateMap<EmployeeRegistrationModel, Employee>();
      CreateMap<Employee, EmployeeViewModel>();
      CreateMap<ClientEditModel, Client>();
    }
  }
}