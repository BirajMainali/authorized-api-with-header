using authorized_api_header.Constants;
using authorized_api_header.Handler;
using authorized_api_header.OpenApiParameters;
using authorized_api_header.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.OperationFilter<AddRequiredHeaderParameter>();
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = ApiHeaderKeyConstants.AuthorizationHeaderKey;
    options.DefaultChallengeScheme = ApiHeaderKeyConstants.AuthorizationHeaderKey;
}).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(ApiHeaderKeyConstants.AuthorizationHeaderKey, null);

builder.Services.AddTransient<IApiKeyValidator, ApiKeyValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Make sure this line is before UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();