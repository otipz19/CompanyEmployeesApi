using Entities.LinkModels;
using MediatR;
using Shared.DTO.RequestFeatures.Paging;

namespace Application.Queries.Company
{
    public sealed record GetCompaniesByIdsQuery(IEnumerable<Guid>? Ids, LinkCompaniesParameters LinkParameters)
        : IRequest<(LinkResponse response, PagingMetaData metaData)>
    {
    }
}
