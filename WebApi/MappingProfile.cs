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
                .ForMember(c => c.FullAddress, options =>
                {
                    options.MapFrom(company => string.Join(' ', company.Country, company.Address));
                });

            CreateMap<Employee, EmployeeDto>();
        }
    }
}
