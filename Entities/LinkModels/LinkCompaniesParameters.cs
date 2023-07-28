using Microsoft.AspNetCore.Http;
using Shared.DTO.RequestFeatures;

namespace Entities.LinkModels
{
    public record LinkCompaniesParameters(CompanyRequestParameters RequestParameters, HttpContext Context)
    {
    }
}
