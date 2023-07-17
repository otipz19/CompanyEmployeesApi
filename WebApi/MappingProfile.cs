using AutoMapper;
using Entities.Models;
using Shared.DTO.Company;
using Shared.DTO.Employee;

namespace WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, GetCompanyDto>()
                .ForMember(c => c.FullAddress, options =>
                {
                    options.MapFrom(company => string.Join(' ', company.Country, company.Address));
                });

            CreateMap<CreateCompanyDto, Company>();

            CreateMap<Employee, GetEmployeeDto>();

            CreateMap<CreateEmployeeDto, Employee>();
        }
    }
}
