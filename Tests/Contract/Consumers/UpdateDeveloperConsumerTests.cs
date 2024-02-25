using PactNet;
using System.Net;
using Tests.Contract.Common;
using Tests.Contract.Providers;
using Xunit.Abstractions;
using Xunit;

namespace Tests.Contract.Consumers
{
    public class UpdateDeveloperConsumerTests
    {
        private readonly IPactBuilderV3 _pactBuilder;

        public UpdateDeveloperConsumerTests(ITestOutputHelper output)
        {
            var config = new PactConfig
            {
                Outputters = new[] { new XUnitOutput(output) }
            };
            // Use default pact directory ..\..\pacts and default log
            // directory ..\..\logs
            var pact = Pact.V3(DeveloperConsumerConfig.DEVELOPER_CONSUMER_NAME, DeveloperProviderConfig.DEVELOPER_PROVIDER_NAME, config);

            // Initialize Rust backend
            _pactBuilder = pact.WithHttpInteractions();
        }

        [Fact]
        public async Task UpdateDeveloper_WithNoIssues_ShouldReturn200()
        {
            // Arrange
            var expectedStatus = HttpStatusCode.OK;
            var apiRelativePath = "/api/Developer";
            var developerId = new Guid("91bc91a6-95be-4d68-a781-ce2b7682db5c");

            var body = new
            {
                name = "string",
                contact = new
                {
                    email = "string",
                    phone = "string"
                },
                stackId = 0,
                login = "string",
                password = "string",
            };

            _pactBuilder
                 .UponReceiving("A PUT to update a developer with no issues")
                     .Given($"There is a Developer with the uId '{developerId}'")
                     .WithRequest(HttpMethod.Put, $"{apiRelativePath}")
                     .WithHeader("Accept", "application/json")
                     .WithHeader("Authorization", "Bearer Token")
                     .WithJsonBody(body)
                 .WillRespond()
                     .WithStatus(expectedStatus);

            await _pactBuilder.VerifyAsync(async ctx =>
            {
                // Act
                var client = new HttpClient() { BaseAddress = ctx.MockServerUri };
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer Token");

                var response = await client.PutAsJsonAsync(apiRelativePath, body);

                // Assert
                Assert.Equal(expectedStatus, response.StatusCode);
            });
        }

        [Fact]
        public async Task UpdateDeveloper_WithNoAuthorizationToken_ShouldReturn401()
        {
            // Arrange
            var expectedStatus = HttpStatusCode.Unauthorized;
            var apiRelativePath = "/api/Developer";
            var developerId = new Guid("91bc91a6-95be-4d68-a781-ce2b7682db5c");

            var body = new
            {
                name = "string",
                contact = new
                {
                    email = "string",
                    phone = "string"
                },
                stackId = 0,
                login = "string",
                password = "string",
            };

            _pactBuilder
                 .UponReceiving("A PUT to update a developer with no authorization header")
                     .Given($"There is a Developer with the uId '{developerId}'")
                     .WithRequest(HttpMethod.Put, $"{apiRelativePath}")
                     .WithHeader("Accept", "application/json")
                     .WithJsonBody(body)
                 .WillRespond()
                     .WithStatus(expectedStatus);

            await _pactBuilder.VerifyAsync(async ctx =>
            {
                // Act
                var client = new HttpClient() { BaseAddress = ctx.MockServerUri };
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.PutAsJsonAsync(apiRelativePath, body);

                // Assert
                Assert.Equal(expectedStatus, response.StatusCode);
            });
        }
    }
}
