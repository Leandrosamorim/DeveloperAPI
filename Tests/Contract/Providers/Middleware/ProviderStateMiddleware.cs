using Domain.DeveloperNS;
using Domain.DeveloperNS.Interface;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Tests.Contract.Providers.Middleware
{
    public class ProviderStateMiddleware
    {
        private const string ConsumerName = "DeveloperConsumer";
        private readonly RequestDelegate _next;
        private readonly IDictionary<string, Action<IDictionary<string, string>>> _providerStates;
        private readonly IDeveloperRepository _developerRepository;


        public ProviderStateMiddleware(RequestDelegate next, IDeveloperRepository developerRepository)
        {
            _next = next;
            _providerStates = new Dictionary<string, Action<IDictionary<string, string>>>
            {
                {
                    "There is a Developer with the uId '91bc91a6-95be-4d68-a781-ce2b7682db5c'",
                    EnsureTestDeveloperExists
                }
            };
            _developerRepository = developerRepository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // if the request is not asking for a provider state, send it on it's way
            if (!(context.Request.Path.Value?.StartsWith("/provider-states") ?? false))
            {
                await _next.Invoke(context);
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.OK;

            if (context.Request.Method == HttpMethod.Post.ToString())
            {
                string jsonRequestBody;
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    jsonRequestBody = await reader.ReadToEndAsync();
                }

                var providerState = JsonSerializer.Deserialize<ProviderState>(jsonRequestBody);

                //A null or empty provider state key must be handled
                if (!string.IsNullOrEmpty(providerState?.State) && _providerStates.ContainsKey(providerState.State))
                {
                    _providerStates[providerState.State].Invoke(providerState.Params!);
                }

                await context.Response.WriteAsync(string.Empty);
            }
        }

        private void EnsureTestDeveloperExists(IDictionary<string, string> parameters)
        {
            var developerName = "testDeveloper";
            var developerUid = new Guid("91bc91a6-95be-4d68-a781-ce2b7682db5c");

            var developer = new Developer()
            {
                UId = developerUid,
                Name = developerName,
                Contact = new Domain.ContactNS.Contact()
                {
                    Email = "testMail",
                    Phone = "testPhone",
                    Id = 1
                },
                StackId = 1,
                Login = "login",
                Password= BCrypt.Net.BCrypt.HashPassword("password")
            };

            var existingDeveloper = _developerRepository.Get(new Domain.DeveloperNS.Query.DeveloperQuery() { UId = new List<Guid>() { developerUid } });

            if (existingDeveloper != null)
            {
                _developerRepository.Create(developer).GetAwaiter().GetResult();
            }
        }

    }
}
