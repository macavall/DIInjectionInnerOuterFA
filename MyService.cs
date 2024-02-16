public class MyService : IMyService
{
    public MyService(IInjectService injectService)
    {
        injectService.InjectServiceMethod();
    }
    public void MyServiceMethod()
    {
        Console.WriteLine("MyServiceMethod");
    }
}