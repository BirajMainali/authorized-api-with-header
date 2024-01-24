using authorized_api_header.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace authorized_api_header.OpenApiParameters;

public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = ApiHeaderKeyConstants.AuthorizationHeaderKey,
            In = ParameterLocation.Header,
            Required = false
        });
    }
}