using FunctionApp1;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {

        var host = new HostBuilder()
        .ConfigureFunctionsWebApplication()
        .ConfigureServices(services =>
        {
            services.AddApplicationInsightsTelemetryWorkerService();
            services.ConfigureFunctionsApplicationInsights();
            services.AddScoped<IMyService, MyService>();
        })
        .Build();

        await host.RunAsync();
    }
}

public class MyService : IMyService
{
    public static bool IsInitialized = false;
    public static IServiceProvider? serviceProvider;
    public static IInnerService myClass;

    public void MyServiceMethod()
    {
        Console.WriteLine("MyServiceMethod");

        InjectServiceMethod();
    }

    public void InjectServiceMethod()
    {
        if (serviceProvider is null)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IInnerService, InnerService>();

            serviceProvider = services.BuildServiceProvider();

            myClass = serviceProvider.GetRequiredService<InnerService>();
        }
        else
        {
            for(int x = 0; x < 3; x++)
            {
                myClass.MyClassMethod();
            }
        }
    }
}

public class MyClass : IInnerService
{
    private readonly IInnerService _injectedService;

    // Constructor injection
    public MyClass(IInnerService injectedService)
    {
        _injectedService = injectedService;
    }

    public void MyClassMethod()
    {
        _injectedService.InnerServiceMethod();
    }
}

public class InnerService : IInnerService
{
    public void InnerServiceMethod()
    {
        Console.WriteLine("InnerServiceMethod");
    }
}

public interface IInnerService
{
    void InnerServiceMethod();
}

public interface IMyService
{
    void MyServiceMethod();
}