using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

namespace GraphClient
{
    public class Program{
        private const string _clientId = "818d0aae-a33f-44fb-957a-54340164d22d";
        private const string _tenantId = "206f248c-2a21-47cf-9a5e-e7c8a3c537d5";
        public static async Task Main(string[] args){
            IPublicClientApplication app;
            app = PublicClientApplicationBuilder
                .Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost")
                .Build();

            List<string> scopes = new List<string> { 
                "user.read" 
            };

            // AuthenticationResult result;
            // result = await app
            // .AcquireTokenInteractive(scopes)
            // .ExecuteAsync();
            // Console.WriteLine($"Token:\t{result.AccessToken}");
            DeviceCodeProvider provider = new DeviceCodeProvider(app, scopes);
            GraphServiceClient client = new GraphServiceClient(provider);

            User myProfile = await client.Me
            .Request()
            .GetAsync();

            Console.WriteLine($"Name:\t{myProfile.DisplayName}");
            Console.WriteLine($"AAD Id:\t{myProfile.Id}");
        }
        
    }
}
