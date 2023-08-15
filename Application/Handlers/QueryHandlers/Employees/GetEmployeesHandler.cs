using Application.Queries.Employees;
using AutoMapper;
using Contracts.Hateoas;
using Contracts.Repository;
using Entities.DataShaping;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using MediatR;
using Service.Contracts.DataShaping;
using Service.Contracts.GetHelpers;
using Shared.DTO.Employee;
using Shared.DTO.RequestFeatures.Paging;

namespace Application.Handlers.QueryHandlers.Employees
{
    internal sealed class GetEmployeesHandler
        : IRequestHandler<GetEmployeesQuery, (LinkResponse response, PagingMetaData metaData)>
    {
        private readonly IGetCompanyHelper _getCompanyHelper;
        private readonly IRepositoryManager _repositories;
        private readonly IEmployeeLinksGenerator _linksGenerator;
        private readonly IDataShaper _dataShaper;
        private readonly IMapper _mapper;

        public GetEmployeesHandler(
            IGetCompanyHelper getHelper,
            IRepositoryManager repositories,
            IEmployeeLinksGenerator linksGenerator,
            IDataShaper dataShaper,
            IMapper mapper)
        {
            _getCompanyHelper = getHelper;
            _repositories = repositories;
            _linksGenerator = linksGenerator;
            _dataShaper = dataShaper;
            _mapper = mapper;
        }

        public async Task<(LinkResponse response, PagingMetaData metaData)> Handle(
            GetEmployeesQuery request,
            CancellationToken cancellationToken)
        {
            if (!request.LinkRequestParameters.RequestParameters.IsValidAgeRange)
            {
                throw new AgeRangeBadRequestException();
            }

            await _getCompanyHelper.GetCompanyIfExistsAsNoTracking(request.CompanyId);

            PagedList<Employee> pagedEmployees = await _repositories.Employees
                .GetEmployeesOfCompany(request.CompanyId, request.LinkRequestParameters.RequestParameters, asNoTracking: true);

            IEnumerable<GetEmployeeDto> employeeDtos = _mapper.Map<IEnumerable<GetEmployeeDto>>(pagedEmployees.Items);

            IEnumerable<ShapedObject> shapedEmployees = _dataShaper.ShapeData(
                employeeDtos,
                request.LinkRequestParameters.RequestParameters.Fields);

            var linkResponse = _linksGenerator.GenerateLinks(
                shapedEmployees,
                request.LinkRequestParameters.RequestParameters.Fields,
                request.CompanyId,
                request.LinkRequestParameters.Context);

            return (linkResponse, pagedEmployees.MetaData);
        }
    }
}
