using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.Identity.Infrastructure.Keycloak.Clients;

public interface IKeycloakHealthChecker
{
    Task WaitUntilReadyAsync(
        CancellationToken cancellationToken = default);
}
public sealed class KeycloakHealthChecker : IKeycloakHealthChecker
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _options;
    private readonly ILogger<KeycloakHealthChecker> _logger;

    public KeycloakHealthChecker(
        HttpClient httpClient,
        IOptions<KeycloakOptions> options,
        ILogger<KeycloakHealthChecker> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task WaitUntilReadyAsync(
        CancellationToken cancellationToken = default)
    {
        const int maxAttempts = 15;

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    "/realms/master",
                    cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation(
                        "Keycloak is ready.");

                    return;
                }
            }
            catch
            {
                // ignore
            }

            _logger.LogInformation(
                "Waiting for Keycloak... Attempt {Attempt}/{MaxAttempts}",
                attempt,
                maxAttempts);

            await Task.Delay(
                TimeSpan.FromSeconds(5),
                cancellationToken);
        }

        throw new InvalidOperationException(
            "Keycloak did not become ready.");
    }
}