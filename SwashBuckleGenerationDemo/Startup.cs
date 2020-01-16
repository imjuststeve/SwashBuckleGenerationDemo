using System.Net;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SwashBuckleGenerationDemo;
using SwashBuckleGenerationDemo.Controllers;

[assembly: WebJobsStartup(typeof(Startup))]
namespace SwashBuckleGenerationDemo
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {

            var context = builder.Services.BuildServiceProvider()
                .GetService<IOptions<ExecutionContextOptions>>().Value;

            var currentDirectory = context.AppDirectory;

            var configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder.SetBasePath(currentDirectory)
                .AddJsonFile("host.json", optional: false, reloadOnChange: true)
                .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
                .Build();

            ConfigureServices(builder.Services, configuration);
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
        }

        private void ConfigureServices(IServiceCollection builder, IConfigurationRoot configuration)
        {
            // Register other project assemblies
            builder.AddSingleton<Function1ApiController>();
            builder.AddMvc();
            builder.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo());
            });

            builder.AddSwaggerGen();
        }
    }
}
