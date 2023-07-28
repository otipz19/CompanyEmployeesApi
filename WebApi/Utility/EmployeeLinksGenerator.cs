using Contracts.Hateoas;
using Entities.DataShaping;
using Entities.LinkModels;
using Presentation.Controllers;

namespace WebApi.Utility
{
    public class EmployeeLinksGenerator : BaseEntityLinksGenerator, IEmployeeLinksGenerator
    {
        private readonly LinkGenerator _linkGenerator;

        public EmployeeLinksGenerator(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public LinkResponse GenerateLinks(IEnumerable<ShapedObject> shapedEmployees, string? fields,
           Guid companyId, HttpContext context)
        {
            if (IsHateoasRequested(context))
            {
                return GetLinkedEmployees(companyId, fields, context, shapedEmployees);
            }

            return GetShapedEntities(shapedEmployees);
        }

        private LinkResponse GetLinkedEmployees(
            Guid companyId,
            string? fields,
            HttpContext context,
            IEnumerable<ShapedObject> shapedEmployees)
        {
            foreach (var shapedEmployee in shapedEmployees)
            {
                List<Link> employeeLinks = CreateLinksForEmployee(context, companyId, (Guid)shapedEmployee["Id"]!);
                shapedEmployee.TryAdd("Links", employeeLinks);
            }

            var employeeCollectionWrapper = new LinkCollectionWrapper<ShapedObject>(shapedEmployees);
            SetLinksInWrapper(employeeCollectionWrapper, context, companyId, fields);
            return new LinkResponse()
            {
                HasLinks = true,
                LinkedEntities = employeeCollectionWrapper,
            };
        }

        private List<Link> CreateLinksForEmployee(HttpContext context, Guid companyId, Guid id)
        {
            return new List<Link>()
            {
                new Link()
                {
                    Href = _linkGenerator.GetUriByAction(
                        context,
                        action: nameof(EmployeesController.GetEmployeeOfCompany),
                        values: new { companyId, id }),
                    Rel = "self",
                    Method = "GET",
                },
                new Link()
                {
                    Href = _linkGenerator.GetUriByAction(
                        context,
                        action: nameof(EmployeesController.DeleteEmployee),
                        values: new { companyId, id}),
                    Rel = "delete_employee",
                    Method = "DELETE",
                },
                new Link()
                {
                    Href = _linkGenerator.GetUriByAction(
                        context,
                        action: nameof(EmployeesController.UpdateEmployee),
                        values: new { companyId, id}),
                    Rel = "update_employee",
                    Method = "PUT",
                },
                new Link()
                {
                    Href = _linkGenerator.GetUriByAction(
                        context,
                        action: nameof(EmployeesController.PartiallyUpdateEmployee),
                        values: new { companyId, id}),
                    Rel = "partially_update_employee",
                    Method = "PATCH",
                }
            };
        }

        private void SetLinksInWrapper(
            LinkCollectionWrapper<ShapedObject> wrapper,
            HttpContext context,
            Guid companyId,
            string? fields)
        {
            wrapper.Links.Add(new Link()
            {
                Href = _linkGenerator.GetUriByAction(
                   context,
                   action: nameof(EmployeesController.GetEmployeesOfCompany),
                   values: new { companyId, fields }),
                Rel = "self",
                Method = "GET",
            });
        }
    }
}
