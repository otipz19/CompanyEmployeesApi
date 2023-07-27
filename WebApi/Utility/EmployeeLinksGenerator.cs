using Contracts.Hateoas;
using Entities.DataShaping;
using Entities.LinkModels;
using Service.Contracts.DataShaping;
using Shared.DTO;
using Shared.DTO.Employee;
using Microsoft.Net.Http.Headers;
using Presentation.Controllers;

namespace WebApi.Utility
{
    public class EmployeeLinksGenerator : IEmployeeLinksGenerator
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper _dataShaper;

        public EmployeeLinksGenerator(LinkGenerator linkGenerator, IDataShaper dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse GenerateLinks(IEnumerable<GetEmployeeDto> employeeDtos, string fields,
           Guid companyId, HttpContext context)
        {
            var shapedEmployees = _dataShaper.ShapeData(employeeDtos, fields);

            if (IsHateoasRequested(context))
            {
                return GetLinkedEmployees(companyId, fields, context, shapedEmployees);
            }

            return GetShapedEmployees(shapedEmployees);
        }

        private bool IsHateoasRequested(HttpContext context)
        {
            MediaTypeHeaderValue? mediaType = context.Items[Constants.MediaTypeItemsKey] as MediaTypeHeaderValue;

            return mediaType?.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.OrdinalIgnoreCase) ?? false;
        }

        private LinkResponse GetShapedEmployees(IEnumerable<ShapedEntity> items)
        {
            return new LinkResponse()
            {
                ShapedEntities = items,
                HasLinks = false,
            };
        }

        private LinkResponse GetLinkedEmployees(
            Guid companyId,
            string fields,
            HttpContext context,
            IEnumerable<ShapedEntity> shapedEmployees)
        {
            foreach(var shapedEmployee in shapedEmployees)
            {
                List<Link> employeeLinks = CreateLinksForEmployee(context, companyId, shapedEmployee.Id, fields);
                shapedEmployee.ShapedObject.TryAdd("Links", employeeLinks);
            }

            var employeeCollectionWrapper = new LinkCollectionWrapper<ShapedEntity>(shapedEmployees);
            SetLinksInWrapper(employeeCollectionWrapper, context, companyId, fields);
            return new LinkResponse()
            {
                HasLinks = true,
                LinkedEntities = employeeCollectionWrapper,
            };
        }

        private List<Link> CreateLinksForEmployee(HttpContext context, Guid companyId, Guid id, string fields = "")
        {
            return new List<Link>()
            {
                new Link(_linkGenerator.GetUriByAction(
                    context,
                    action: nameof(EmployeesController.GetEmployeeOfCompany),
                    values: new { companyId, id, fields})!,
                    "self",
                    "GET"),
                new Link(_linkGenerator.GetUriByAction(
                    context,
                    action: nameof(EmployeesController.DeleteEmployee),
                    values: new { companyId, id})!,
                    "delete_employee",
                    "DELETE"),
                new Link(_linkGenerator.GetUriByAction(
                    context,
                    action: nameof(EmployeesController.UpdateEmployee),
                    values: new { companyId, id})!,
                    "update",
                    "PUT"),
                new Link(_linkGenerator.GetUriByAction(
                    context,
                    action: nameof(EmployeesController.PartiallyUpdateEmployee),
                    values: new { companyId, id})!,
                    "partially_update_employee",
                    "PATCH"),
            };
        }

        private void SetLinksInWrapper(
            LinkCollectionWrapper<ShapedEntity> wrapper,
            HttpContext context,
            Guid companyId,
            string fields = "")
        {
            wrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(
                   context,
                   action: nameof(EmployeesController.GetEmployeesOfCompany),
                   values: new { companyId, fields })!,
                   "self",
                   "GET"));
        }
    }
}
