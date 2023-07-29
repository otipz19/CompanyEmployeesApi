using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult GetRoot([FromHeader(Name = "Accept")] string? mediaType)
        {
            if(!string.IsNullOrEmpty(mediaType) && mediaType.Contains("application/vnd.codemaze.apiroot"))
            {
                var links = new List<Link>()
                {
                    new Link()
                    {
                        Href = _linkGenerator.GetPathByAction(HttpContext, action: nameof(RootController.GetRoot)),
                        Rel = "self",
                        Method = "GET",
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetPathByAction(
                            HttpContext,
                            controller: "Companies",
                            action: nameof(CompaniesController.GetCompanies)),
                        Rel = "get_companies",
                        Method = "GET",
                    },
                    new Link()
                    {
                        Href = _linkGenerator.GetPathByAction(
                            HttpContext,
                            controller: "Companies",
                            action: nameof(CompaniesController.CreateCompany)),
                        Rel = "create_company",
                        Method = "POST",
                    },
                };

                return Ok(links);
            }

            return NoContent();
        }
    }
}
