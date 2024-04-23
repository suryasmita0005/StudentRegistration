using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using StudentRegistration.Models;
using System;
using System.Net.Http.Headers;

namespace StudentRegistration.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }


        public async Task<IActionResult> Index()
        {
            var task = GetAccessTokenAsync(); // Get the Task<AuthenticationResult>
            var result = await task; 

            if (result == null)
            {
                return View("Error");
            }

            //var accessToken = result.AccessToken; // Extract the access token
            var users = await GetUsersAsync();
            return View(users);
        }

        private async Task<List<User>> GetUsersAsync()
        {
            var accessToken = await GetAccessTokenAsync();
            var users = new List<User>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var nextLink = $"https://graph.microsoft.com/v1.0/users?$top=100";
                do
                {
                    var response = await client.GetAsync(nextLink);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var userPage = JsonConvert.DeserializeObject<UserPage>(content);

                    users.AddRange(userPage.Value);
                    nextLink = userPage.NextLink;
                } while (nextLink != null);
            }


            return users;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var clientId = "1532b150-ea38-4fbd-b09b-079d918584d4";
            var policy = "B2C_1_studentregistrationlogin";
            var Tenant = "asterissolutionsorg.onmicrosoft.com";
            var aadInstance = "https://asterissolutionsorg.b2clogin.com/v2.0/tfp/";
            var graphScope = "https://graph.microsoft.com/.default";

            var fullAuthority = $"{aadInstance}{Tenant}/{policy}";

            var pca =  PublicClientApplicationBuilder
                      .Create(clientId)
                      .WithB2CAuthority(fullAuthority)
                      .Build();

            var scopes = new string[] { graphScope };

            try
            {
                // Attempt silent token acquisition using cached tokens (if available)
                var accounts = await pca.GetAccountsAsync().ConfigureAwait(false);

                
                if (accounts.Any())
                {
                    var silentResult = await pca.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync().ConfigureAwait(false);
                    return silentResult.AccessToken;
                }
                else
                {
                    // Redirect to login if no accounts found
                    var result = await pca.AcquireTokenInteractive(scopes)
                    .WithPrompt(Microsoft.Identity.Client.Prompt.SelectAccount)
                    .ExecuteAsync();

                    return result.AccessToken;
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                throw new Exception($"An unexpected error occurred: {ex.Message}");
            }
        }

    }
}

