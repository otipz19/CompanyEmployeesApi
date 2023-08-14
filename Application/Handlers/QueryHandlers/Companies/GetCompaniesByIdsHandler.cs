using Application.Queries.Companies;
using Contracts.Repository;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using MediatR;
using Service.Contracts.GetHelpers;
using Shared.DTO.RequestFeatures.Paging;

namespace Application.Handlers.QueryHandlers.Companies
{
    internal sealed class GetCompaniesByIdsHandler
        : IRequestHandler<GetCompaniesByIdsQuery, (LinkResponse response, PagingMetaData metaData)>
    {
        private readonly IGetCompanyHelper _getHelper;
        private readonly IRepositoryManager _repositories;

        public GetCompaniesByIdsHandler(IGetCompanyHelper getHelper, IRepositoryManager repositories)
        {
            _getHelper = getHelper;
            _repositories = repositories;
        }

        public async Task<(LinkResponse response, PagingMetaData metaData)> Handle(
            GetCompaniesByIdsQuery request,
            CancellationToken cancellationToken)
        {
            if (request.Ids is null)
                throw new IdParametersBadRequestException();

            PagedList<Company> pagedCompanies = await _repositories.Companies
                .GetCompaniesByIds(request.Ids, asNoTracking: true, request.LinkParameters.RequestParameters);

            if (pagedCompanies.MetaData.TotalCount != request.Ids.Count())
                throw new CollectionByIdsBadRequestException();

            return _getHelper.ShapeAndGenerateLinks(pagedCompanies, request.LinkParameters);
        }
    }
}
