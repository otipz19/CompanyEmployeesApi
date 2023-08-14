using Application.Queries.Companies;
using AutoMapper;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;
using Shared.DTO.Company;

namespace Application.Handlers.QueryHandlers.Companies
{
    internal sealed class GetCompanyForPatchHandler
        : IRequestHandler<GetCompanyForPatchQuery, (UpdateCompanyDto dto, Company entity)>
    {
        private readonly IGetCompanyHelper _getHelper;
        private readonly IMapper _mapper;

        public GetCompanyForPatchHandler(IGetCompanyHelper getHelper, IMapper mapper)
        {
            _getHelper = getHelper;
            _mapper = mapper;
        }

        public async Task<(UpdateCompanyDto dto, Company entity)> Handle(
            GetCompanyForPatchQuery request,
            CancellationToken cancellationToken)
        {
            Company company = await _getHelper.GetCompanyIfExists(request.Id);
            UpdateCompanyDto dto = _mapper.Map<UpdateCompanyDto>(company);
            return (dto, company);
        }
    }
}
