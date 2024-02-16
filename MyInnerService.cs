public class MyInnerService : IMyInnerService
{
    public MyInnerService(IInjectService injectService)
    {
        injectService.InjectServiceMethod();
    }
    public void InnerServiceMethod()
    {
        Console.WriteLine("InnerServiceMethod");
    }
}