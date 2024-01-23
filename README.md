In this project, authorization is done using an API key that is passed in the header of the HTTP request. Here's a step-by-step guide on how it works:

1. **API Key Generation**: An API key is generated and shared with the client. This key is used to authenticate the client's requests. The key should be kept secret and not exposed in public places like GitHub or client-side code.

2. **Passing the API Key**: The client includes the API key in the header of each HTTP request. The key is included in the `X-Api-Key` header field.

3. **API Key Validation**: On the server side, the `ApiKeyAuthenticationHandler` class is responsible for validating the API key. It retrieves the API key from the request header and checks its validity using the `IApiKeyValidator` service.

4. **Handling Invalid API Keys**: If the API key is invalid or not provided, the `HandleAuthenticateAsync` method of `ApiKeyAuthenticationHandler` returns an `AuthenticateResult` indicating failure. This results in an HTTP 401 Unauthorized response.

5. **Handling Valid API Keys**: If the API key is valid, `HandleAuthenticateAsync` creates a `ClaimsPrincipal` with the API key as its identity and returns an `AuthenticateResult` indicating success. The `ClaimsPrincipal` is then available throughout the rest of the request's lifetime.

Here's the key part of the code that handles this:

```csharp
protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
{
    if (!Request.Headers.TryGetValue(ApiHeaderKeyConstants.AuthorizationHeaderKey, out var apiKeyHeaderValues))
    {
        return AuthenticateResult.Fail("API Key was not provided.");
    }

    var providedApiKey = apiKeyHeaderValues.ToString();

    if (!_apiKeyValidator.IsValid(providedApiKey))
    {
        return AuthenticateResult.Fail("Invalid API Key provided.");
    }

    var claims = new[] { new Claim(ClaimTypes.Name, providedApiKey) };
    var identity = new ClaimsIdentity(claims, Scheme.Name);
    var principal = new ClaimsPrincipal(identity);
    var ticket = new AuthenticationTicket(principal, Scheme.Name);
    return AuthenticateResult.Success(ticket);
}
```

This approach to authorization ensures that only clients with a valid API key can access the protected resources.
