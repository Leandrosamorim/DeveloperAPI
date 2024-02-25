using PactNet;
using System.Net;
using Tests.Contract.Common;
using Tests.Contract.Providers;
using Xunit.Abstractions;
using Xunit;

namespace Tests.Contract.Consumers
{
    public class GetDeveloperConsumerTests
    {
        private readonly IPactBuilderV3 _pactBuilder;

        public GetDeveloperConsumerTests(ITestOutputHelper output)
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

        //[Fact]
        //public async Task GetDevelopersByStack_WithNoIssues_ShouldReturn200()
        //{
        //    // Arrange
        //    var expectedStatus = HttpStatusCode.OK;
        //    var apiRelativePath = "/api/Developer";


        //    _pactBuilder
        //         .UponReceiving("A GET to retrieve a developer with no authorization header")
        //             .WithRequest(HttpMethod.Get, $"{apiRelativePath}")
        //             .WithHeader("Accept", "*/*")
        //             //.WithQuery("StackId", "1")
        //         .WillRespond()
        //             .WithStatus(expectedStatus);

        //    await _pactBuilder.VerifyAsync(async ctx =>
        //    {
        //        // Act
        //        var client = new HttpClient() { BaseAddress = ctx.MockServerUri };
        //        client.DefaultRequestHeaders.Add("Accept", "*/*");

        //        var response = await client.GetAsync($"{apiRelativePath}");

        //        // Assert
        //        Assert.Equal(expectedStatus, response.StatusCode);
        //    });
        //}

        //[Fact]
        //public async Task CreateDeveloper_WithEmptyBody_ShouldReturn400()
        //{
        //    // Arrange
        //    var expectedStatus = HttpStatusCode.BadRequest;
        //    var apiRelativePath = "/api/Developer";

        //    var body = new
        //    {
        //    };

        //    var expectedResponse = new
        //    {
        //        title = "One or more validation errors occurred.",
        //        status = 400,
        //        errors = new Dictionary<string, string[]>
        //        {
        //            { "Name", new string[]{"The Name field is required."} },
        //            { "Login", new string[]{"The Login field is required."} },
        //            { "Contact", new string[]{"The Contact field is required." } },
        //            { "Password", new string[]{"The Password field is required." } }
        //        }
        //    };

        //    _pactBuilder
        //             .UponReceiving("A POST to create a developer with no authorization header")
        //                 .Given("There is no authorization header")
        //                 .WithRequest(HttpMethod.Post, $"{apiRelativePath}")
        //                 .WithHeader("Accept", "application/json")
        //                 .WithJsonBody(body)
        //             .WillRespond()
        //                 .WithHeader("Content-Type", "application/problem+json; charset=utf-8")
        //                 .WithJsonBody(expectedResponse)
        //                 .WithStatus(expectedStatus);

        //    await _pactBuilder.VerifyAsync(async ctx =>
        //    {
        //        // Act
        //        var client = new HttpClient() { BaseAddress = ctx.MockServerUri };
        //        client.DefaultRequestHeaders.Add("Accept", "application/json");

        //        var response = await client.PostAsJsonAsync(apiRelativePath, body);

        //        // Assert
        //        Assert.Equal(expectedStatus, response.StatusCode);
        //    });
        //}

    }

}

