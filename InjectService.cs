using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

public class InjectService : IInjectService
{
    public void InjectServiceMethod()
    {
        Console.WriteLine("InjectServiceMethod has been called!");
    }
    public InjectService()
    {
        // Creating a new DI container within OuterService
        var services = new ServiceCollection();
        services.AddAuthenticationCore(options =>
        {
            // Configure authentication options
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        });
        var serviceProvider = services.BuildServiceProvider();

        // Retrieve the authentication scheme
        var authenticationSchemeProvider = serviceProvider.GetRequiredService<IAuthenticationSchemeProvider>();
        var defaultScheme = authenticationSchemeProvider.GetDefaultAuthenticateSchemeAsync().Result;

        Console.WriteLine($"Default authentication scheme: {defaultScheme?.Name ?? "None"}");
    }
}