using Application.Queries.Company;
using AutoMapper;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;
using Shared.DTO.Company;

namespace Application.Handlers.QueryHandlers.Companies
{
    internal sealed class GetCompanyHandler : IRequestHandler<GetCompanyQuery, GetCompanyDto>
    {
        private readonly IGetCompanyHelper _getHelper;
        private readonly IMapper _mapper;

        public GetCompanyHandler(IGetCompanyHelper getHelper, IMapper mapper)
        {
            _getHelper = getHelper;
            _mapper = mapper;
        }

        public async Task<GetCompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            Company company = await _getHelper.GetCompanyIfExists(request.Id);

            var companieDto = _mapper.Map<GetCompanyDto>(company);
            return companieDto;
        }
    }
}
