using AutoMapper;
using Entities.Models;
using Shared.DTO;

namespace WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForCtorParam("FullAddress", options =>
                {
                    options.MapFrom(company => string.Join(' ', company.Country, company.Address));
                });

            CreateMap<Employee, EmployeeDto>();
        }
    }
}
