using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

public class InjectService : IInjectService
{
    public readonly IServiceProvider? serviceProvider;

    public InjectService()
    {
        // Creating a new DI container within OuterService
        var services = new ServiceCollection();
        services.AddSingleton<IMyInnerService, MyInnerService>();
        //services.AddAuthenticationCore();
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        serviceProvider.GetService<IMyInnerService>()?.InnerServiceMethod();
    }

    public void InjectServiceMethod()
    {
        Console.WriteLine("InjectServiceMethod has been called!");

        var myInService = serviceProvider?.GetService<IMyInnerService>();

        myInService?.InnerServiceMethod();
    }

    public class MyNewInnerClass
    {
        public readonly IMyInnerService? myInnerService;

        public MyNewInnerClass(IMyInnerService _myInnerService)
        {
            myInnerService = _myInnerService;
        }

        public void MyNewInnerClassMethod()
        {
            Console.WriteLine("MyNewInnerClassMethod has been called!");

            myInnerService?.InnerServiceMethod();
        }
    }
}