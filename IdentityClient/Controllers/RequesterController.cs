using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequesterController : ControllerBase
    {

        private readonly ILogger<RequesterController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public RequesterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string responseBody = null;
            //
            var serviceMetaClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await serviceMetaClient.GetDiscoveryDocumentAsync(Config.IDENTITY_SERVER_URL);
            var tokenResponse = await serviceMetaClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = Config.CLIENT_ID,
                ClientSecret = Config.CLIENT_SECRET,
                Scope = "ApiToBeSecured"
            });
            //serviceMetaClient.Dispose();
            //
            var dataClient = _httpClientFactory.CreateClient();
            dataClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await dataClient.GetAsync($"{Config.RESOURCE_API_URL}/list");
            if (response.IsSuccessStatusCode)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }
            //dataClient.Dispose();
            //
            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                responseBody
            });
        }
    }
}
