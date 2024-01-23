namespace authorized_api_header.Validators;

public class ApiKeyValidator : IApiKeyValidator
{
    private readonly IConfiguration _configuration;

    public ApiKeyValidator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool IsValid(string apiKey)
    {
        var validApiKey = _configuration.GetValue<string>("Secret");
        return apiKey == validApiKey;
    }
}