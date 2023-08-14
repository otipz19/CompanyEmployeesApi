using Application.Queries.Company;
using Contracts.Repository;
using Entities.LinkModels;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;
using Shared.DTO.RequestFeatures.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.QueryHandlers.Companies
{
    internal sealed class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, (LinkResponse response, PagingMetaData metaData)>
    {
        private readonly IRepositoryManager _repositories;
        private readonly IGetCompanyHelper _getHelper;

        public GetCompaniesHandler(IRepositoryManager repositories, IGetCompanyHelper getHelper)
        {
            _repositories = repositories;
            _getHelper = getHelper;
        }

        public async Task<(LinkResponse response, PagingMetaData metaData)> Handle(
            GetCompaniesQuery request,
            CancellationToken cancellationToken)
        {
            PagedList<Company> pagedCompanies = await _repositories.Companies
                .GetCompanies(asNoTracking: true, request.LinkParameters.RequestParameters);

            return _getHelper.ShapeAndGenerateLinks(pagedCompanies, request.LinkParameters);
        }
    }
}
