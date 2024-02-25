using PactNet.Verifier;
using Tests.Contract.Common;
using Xunit.Abstractions;
using Xunit;
using Tests.Contract.Providers.ProviderFixture;

namespace Tests.Contract.Providers
{
    public class DeveloperProviderTests : IClassFixture<DeveloperApiFixture>
    {
        private readonly DeveloperApiFixture _fixture;
        private readonly ITestOutputHelper _output;

        public DeveloperProviderTests(DeveloperApiFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public void DeveloperApi_VerifyPact_PactShouldBeVerified()
        {
            // arrange
            var config = new PactVerifierConfig
            {
                Outputters = new[] { new XUnitOutput(_output) },
                LogLevel = PactNet.PactLogLevel.Debug
            };
            string pactPath = Path.Combine("..", "..", "..", "pacts", "DeveloperConsumer-DeveloperProvider.json");
            IPactVerifier pactVerifier = new PactVerifier(config);

            // act && assert
            pactVerifier
                .ServiceProvider(DeveloperProviderConfig.DEVELOPER_PROVIDER_NAME, _fixture.ServerUri)
                .WithFileSource(new FileInfo(pactPath))
                .WithProviderStateUrl(new Uri(_fixture.ServerUri, "/provider-states"))
                .Verify();
        }
    }

}
