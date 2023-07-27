using Shared.DTO.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace Entities.LinkModels
{
    public record LinkEmployeesParameters (EmployeeRequestParameters RequestParameters, HttpContext Context)
    {
    }
}
