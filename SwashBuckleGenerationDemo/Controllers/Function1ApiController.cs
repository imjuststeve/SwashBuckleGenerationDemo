using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace SwashBuckleGenerationDemo.Controllers
{
    [ApiController]
    [Route("[buddies]")]
    public class Function1ApiController : ControllerBase
    {
        private readonly ILogger<Function1ApiController> logger;

        public Function1ApiController(ILogger<Function1ApiController> logger)
        {
            this.logger = logger;
        }

        [FunctionName(nameof(Get))]
        [HttpGet]
        public async Task Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "function1")] HttpRequest req)
        {
        }
    }
}
