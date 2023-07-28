using Contracts.Hateoas;
using Entities.DataShaping;
using Entities.LinkModels;
using Presentation.Controllers;

namespace WebApi.Utility
{
    public class CompanyLinksGenerator : BaseEntityLinksGenerator, ICompanyLinksGenerator
    {
        private readonly LinkGenerator _linkGenerator;

        public CompanyLinksGenerator(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public LinkResponse GenerateLinks(IEnumerable<ShapedObject> shapedCompanies, string? fields, HttpContext context)
        {
            if (IsHateoasRequested(context))
            {
                return GetLinkedCompanies(shapedCompanies, fields, context);
            }

            return GetShapedEntities(shapedCompanies);
        }

        private LinkResponse GetLinkedCompanies(IEnumerable<ShapedObject> shapedCompanies, string? fields, HttpContext context)
        {
            foreach (var shapedCompany in shapedCompanies)
            {
                var links = GetLinksForCompany(context, (Guid)shapedCompany["Id"]!);
                shapedCompany.TryAdd("Links", links);
            }

            var wrapper = new LinkCollectionWrapper<ShapedObject>(shapedCompanies);
            SetLinksInWrapper(wrapper, fields, context);
            return new LinkResponse()
            {
                HasLinks = true,
                LinkedEntities = wrapper
            };
        }

        private List<Link> GetLinksForCompany(HttpContext context, Guid id)
        {
            return new List<Link>()
            {
                new Link()
                {
                    Href = _linkGenerator.GetPathByAction(
                        context,
                        action: nameof(CompaniesController.GetCompany),
                        values: new { id }),
                    Rel = "self",
                    Method = "GET",
                },
                new Link()
                {
                    Href = _linkGenerator.GetPathByAction(
                        context,
                        action: nameof(CompaniesController.DeleteCompany),
                        values: new { id }),
                    Rel = "delete_company",
                    Method = "DELETE",
                },
                new Link()
                {
                    Href = _linkGenerator.GetPathByAction(
                        context,
                        action: nameof(CompaniesController.UpdateCompany),
                        values: new { id }),
                    Rel = "update_company",
                    Method = "PUT",
                },
                new Link()
                {
                    Href = _linkGenerator.GetPathByAction(
                        context,
                        action: nameof(CompaniesController.PartiallyUpdateCompany),
                        values: new { id }),
                    Rel = "partially_update_company",
                    Method = "PATCH",
                },
            };
        }

        private void SetLinksInWrapper(LinkCollectionWrapper<ShapedObject> wrapper, string? fields, HttpContext context)
        {
            wrapper.Links.Add(
            new Link()
            {
                Href = _linkGenerator.GetPathByAction(
                        context,
                        action: nameof(CompaniesController.GetCompanies),
                        values: new { fields }),
                Rel = "self",
                Method = "GET",
            });
        }
    }
}
