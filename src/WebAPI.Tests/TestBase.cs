using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using PapoDeDev.TDD.WebAPI;
using System;
using System.Net.Http;
using WebAPI.Tests.Setup;
using Xunit;

namespace WebAPI.Tests
{
    public abstract class TestBase : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly WebApplicationFactory<StartupTest> _Factory;

        protected TestBase(CustomWebApplicationFactory factory)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            _Factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });
            });
        }

        protected HttpClient CreateHttpClient()
        {
            return _Factory.CreateClient();
        }
    }
}
