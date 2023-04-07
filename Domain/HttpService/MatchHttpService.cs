using Domain.HttpService.Interfaces;
using Domain.MatchNS;
using Domain.OrganizationNS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.HttpService
{
    public class MatchHttpService : IMatchHttpService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public MatchHttpService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IEnumerable<Organization>> GetMyMatches(Guid developerUId)
        {
            var url = _config.GetSection("MatchAPI").Value.ToString() + "api/DeveloperMatch/my?developerUid=" + developerUId;
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<IEnumerable<Organization>>();
                return json;
            }

            return default;
        }

        public async Task<IEnumerable<OrganizationDTO>> GetOrganizationsToMatch(Guid developerUId)
        {
            var url = _config.GetSection("MatchAPI").Value.ToString() + "api/DeveloperMatch?UId=" + developerUId;
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<IEnumerable<OrganizationDTO>>();
                return json;
            }

            return default;
        }

        public async Task<bool> MatchOrganization(Guid developerUId, Guid organizationUId)
        {
            var url = _config.GetSection("MatchAPI").Value.ToString() + "api/DeveloperMatch";
            var match = new Match() { DeveloperUId = developerUId, OrganizationUId= organizationUId, Date = DateTime.UtcNow };
            var requestBody = JsonSerializer.Serialize(match);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }

            return default;
        }
    }
}
