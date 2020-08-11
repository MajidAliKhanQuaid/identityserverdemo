using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                //new IdentityResources.Profile(),
                //new IdentityResource("ApiToBeSecured", new List<string>{
                //      JwtClaimTypes.BirthDate,
                //      JwtClaimTypes.Email,
                //}),
            };

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>()
            {
                new ApiResource("ApiToBeSecured", "ApiToBeSecured")
                {
                    // defining scopes here is imp. 
                    // audience will be null if not defined
                    Scopes = new [] { "Scope1", "Scope2", "ApiToBeSecured" }
                }
        };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            List<string> claims = new List<string>();
            claims.Add("user@gmail.com");
            claims.Add(new DateTime(1994, 4, 9).ToShortDateString());
            //
            return new[]
            {
                new ApiScope(name: "Scope1",   displayName: "Scope # 1", claims),
                new ApiScope(name: "Scope2",   displayName: "Scope # 2", claims),
                new ApiScope(name: "Scope3",   displayName: "Scope # 3", claims),
                new ApiScope(name: "Scope4",   displayName: "Scope # 4", claims),
                new ApiScope(name: "ApiToBeSecured",   displayName: "ApiToBeSecured", claims)

                // new ApiScope(name: "Scope1",   displayName: "Scope # 1"),
                //new ApiScope(name: "Scope3",   displayName: "Scope # 3" ),
                //new ApiScope(name: "Scope4",   displayName: "Scope # 4" ),
                //new ApiScope(name: "Scope2",   displayName: "Scope # 2" ),
                //new ApiScope(name: "ApiToBeSecured",   displayName: "ApiToBeSecured")

            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId= "client-1",
                    ClientSecrets = {new Secret("secret-1".ToSha256()) },
                    AllowedScopes  = {"Scope1", "Scope3", "ApiToBeSecured"},
                    //AllowedScopes  = {"ApiToBeSecured"},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //Claims = new List<ClientClaim>()
                    //{
                    //    new ClientClaim(){Type = ClaimTypes.Email, Value = "user@gmail.com"},
                    //    new ClientClaim(){Type = ClaimTypes.DateOfBirth, Value = new DateTime(1994, 4,9).ToShortDateString()}
                    //}
                }
            };
        }

    }
}
