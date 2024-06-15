using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System.Reflection;

namespace StudentRegistration.Repositories
{
    public class B2CUsersService
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly IConfiguration _configuration;

        public B2CUsersService(IConfiguration configuration)
        {
            _configuration = configuration;

            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var tenantId = "cb5e220d-a72c-4987-9470-44cbb897e61e";
            var clientId = "1532b150-ea38-4fbd-b09b-079d918584d4";
            var clientSecret = "koO8Q~J6WR.rs0vPVDnOXHvX4yaTJbmXcJsL4bDh";

            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            _graphServiceClient = new GraphServiceClient(clientSecretCredential, scopes);

        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _graphServiceClient.Users
                .GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Select = new List<string>()
                    {
                        "displayName",
                        "id",
                        "identities",
                        "otherMails"
                    }.ToArray();

                });

            return users.Value;
        }
    }
}
