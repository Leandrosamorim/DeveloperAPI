using PactNet;
using System.Net;
using Tests.Contract.Common;
using Xunit.Abstractions;
using Xunit;
using Tests.Contract.Providers;
using PactNet.Matchers;

namespace Tests.Contract.Consumers
{
    public class CreateDeveloperConsumerTests
    {
        private readonly IPactBuilderV3 _pactBuilder;

        public CreateDeveloperConsumerTests(ITestOutputHelper output)
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
        public async Task CreateDeveloper_WithNoAuthorizationToken_ShouldReturn200()
        {
            // Arrange
            var expectedStatus = HttpStatusCode.OK;
            var apiRelativePath = "/api/Developer";

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

            var expectedResponse = Match.Type(new
            {
                uId = PactNet.Matchers.Match.Type(Guid.Empty),
                name = Match.Type("string"),
                contact = Match.Type(new
                {
                    email = Match.Type("string"),
                    phone = Match.Type("string")
                }),
                stackId = Match.Type(default(int)),
                login = Match.Type("string"),
                password = Match.Type("string")
            });


            _pactBuilder
                 .UponReceiving("A POST to create a developer with no authorization header")
                     .WithRequest(HttpMethod.Post, $"{apiRelativePath}")
                     .WithHeader("Accept", "application/json")
                     .WithJsonBody(body)
                 .WillRespond()
                     .WithJsonBody(expectedResponse)
                     .WithStatus(expectedStatus);

            await _pactBuilder.VerifyAsync(async ctx =>
            {
                // Act
                var client = new HttpClient() { BaseAddress = ctx.MockServerUri };
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.PostAsJsonAsync(apiRelativePath, body);

                // Assert
                Assert.Equal(expectedStatus, response.StatusCode);
            });
        }

        [Fact]
        public async Task CreateDeveloper_WithEmptyBody_ShouldReturn400()
        {
            // Arrange
            var expectedStatus = HttpStatusCode.BadRequest;
            var apiRelativePath = "/api/Developer";

            var body = new
            {
            };

            var expectedResponse = new
            {
                title = "One or more validation errors occurred.",
                status = 400,
                errors = new Dictionary<string, string[]>
                {
                    { "Name", new string[]{"The Name field is required."} },
                    { "Login", new string[]{"The Login field is required."} },
                    { "Contact", new string[]{"The Contact field is required." } },
                    { "Password", new string[]{"The Password field is required." } }
                }
            };

        _pactBuilder
                 .UponReceiving("A POST to create a developer with validation issues")
                     .WithRequest(HttpMethod.Post, $"{apiRelativePath}")
                     .WithHeader("Accept", "application/json")
                     .WithJsonBody(body)
                 .WillRespond()
                     .WithHeader("Content-Type", "application/problem+json; charset=utf-8")
                     .WithJsonBody(expectedResponse)
                     .WithStatus(expectedStatus);

            await _pactBuilder.VerifyAsync(async ctx =>
            {
                // Act
                var client = new HttpClient() { BaseAddress = ctx.MockServerUri };
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.PostAsJsonAsync(apiRelativePath, body);

                // Assert
                Assert.Equal(expectedStatus, response.StatusCode);
            });
        }

    }

}

