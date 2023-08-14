using Entities.LinkModels;
using MediatR;
using Shared.DTO.RequestFeatures.Paging;

namespace Application.Queries.Companies
{
    public sealed record GetCompaniesQuery(LinkCompaniesParameters LinkParameters)
        : IRequest<(LinkResponse response, PagingMetaData metaData)>
    {
    }
}
