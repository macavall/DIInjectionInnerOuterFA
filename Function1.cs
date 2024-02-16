using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly IMyService myService;
        private readonly IInjectService inject;

        public Function1(ILogger<Function1> logger, IMyService _myService, InjectService injectService)
        {
            _logger = logger;
            myService = _myService;
            inject = injectService;
        }

        [Function("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            myService.MyServiceMethod();

            inject.InjectServiceMethod();

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
