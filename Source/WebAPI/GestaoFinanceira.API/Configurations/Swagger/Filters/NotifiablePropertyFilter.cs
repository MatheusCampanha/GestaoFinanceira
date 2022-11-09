using GestaoFinanceira.Domain.Core.Notifications;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GestaoFinanceira.API.Configurations.Filters
{
    public class NotifiablePropertyFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var type = typeof(Notifiable);
            var ignoredProperties = type.GetProperties().Select(x => x.Name);

            operation.Parameters
                .Where(param => ignoredProperties.Any(exc => string.Equals(exc, param.Name, StringComparison.OrdinalIgnoreCase)))
                .Select(prExclude => prExclude)
                .ToList()
                .ForEach(key => operation.Parameters.Remove(key));
        }
    }
}
