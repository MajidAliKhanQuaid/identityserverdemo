using IdentityModel;
using IdentityServer4;
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
        // we've two types of resources : identity and api
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                // these are list of scopes in identity resources
                // we need them in oidc authentication
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "is.FatherName",
                    UserClaims = new []{ "claim.FatherName" }
                }
                //new IdentityResources.Email(),
                //new IdentityResources.Phone(),
                //new IdentityResources.Address()
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
            return new[]
            {
                new ApiScope(name: "Scope1",   displayName: "Scope # 1"),
                new ApiScope(name: "Scope2",   displayName: "Scope # 2"),
                new ApiScope(name: "Scope3",   displayName: "Scope # 3"),
                new ApiScope(name: "Scope4",   displayName: "Scope # 4"),
                new ApiScope(name: "ApiToBeSecured",   displayName: "ApiToBeSecured")
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
                    AllowedScopes  = {"Scope2", "Scope3", "ApiToBeSecured"},
                    //AllowedScopes  = {"ApiToBeSecured"},
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                    //Claims = new List<ClientClaim>()
                    //{
                    //    new ClientClaim(){Type = ClaimTypes.Email, Value = "user@gmail.com"},
                    //    new ClientClaim(){Type = ClaimTypes.DateOfBirth, Value = new DateTime(1994, 4,9).ToShortDateString()}
                    //}
                },
                new Client()
                {
                    ClientId= "client_razor",
                    ClientSecrets = {new Secret("secret_razor".ToSha256()) },
                    AllowedScopes  = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        //IdentityServerConstants.StandardScopes.Email,
                        //IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Profile,
                        //IdentityServerConstants.StandardScopes.Address,
                        "is.FatherName"
                    },
                    RedirectUris = new[] { "https://localhost:44383/signin-oidc"},
                    AllowedGrantTypes = GrantTypes.Code,
                    // adding claims in id token
                    AlwaysIncludeUserClaimsInIdToken = false,
                    //RequireConsent = true
                }
            };
        }

    }
}
