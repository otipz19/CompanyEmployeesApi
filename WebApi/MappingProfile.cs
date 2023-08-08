using AutoMapper;
using Entities.Models;
using Shared.DTO.Authentication;
using Shared.DTO.Company;
using Shared.DTO.Employee;

namespace WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapCompany();
            MapEmployee();
            MapUser();
        }

        private void MapEmployee()
        {
            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
        }

        private void MapCompany()
        {
            CreateMap<Company, GetCompanyDto>()
                            .ForMember(c => c.FullAddress, options =>
                            {
                                options.MapFrom(company => string.Join(' ', company.Country, company.Address));
                            });
            CreateMap<CreateCompanyDto, Company>();
            CreateMap<UpdateCompanyDto, Company>().ReverseMap();
        }

        private void MapUser()
        {
            CreateMap<RegisterUserDto, User>();
        }
    }
}
