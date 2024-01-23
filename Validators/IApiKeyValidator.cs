namespace authorized_api_header.Validators;

public interface IApiKeyValidator
{
    bool IsValid(string apiKey);
}