# Project Contains

- Identity Server
- Web Api
- Angular app
- Razor Pages app
- Web Api Client
- CQRS for Web Api (Incomplete)

## Identity Server

Configuration file

```cs
	public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string conStr = _configuration.GetConnectionString("Default");
            services.AddDbContext<AppIdentityContext>(config =>
            {
                config.UseSqlServer(_configuration.GetConnectionString("Default"));
                //config.UseInMemoryDatabase("Default");
            });
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<AppIdentityContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = "/Login";
                config.LogoutPath = "/Logout";
                config.AccessDeniedPath = "/AccessDenied";
            });

            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiScopes(Config.GetApiScopes()) // with out this -- you get scopes error
                .AddInMemoryClients(Config.GetClients())
                .AddDeveloperSigningCredential();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(nameof(Constants.AdministratorRole), policy => policy.RequireClaim(JwtRegisteredClaimNames.Nonce, Constants.AdministratorRole));
            //});

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(nameof(Constants.SimpleUser), policy => policy.RequireClaim(JwtRegisteredClaimNames.Nonce, Constants.SimpleUser));
            //});

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                // seeding the database
                AppIdentityContextSeed.SeedData(userManager, roleManager);

                app.UseDeveloperExceptionPage();
                // default identityserver using http + chrome, doesn't work. 
                // Chrome enforces that cookies with SameSite=none have also Secure attribute, so you may have to either use HTTPS, or modify the cookie policy
                app.UseCookiePolicy(new CookiePolicyOptions()
                {
                    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax
                });
            }

            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
```

OpenId connect resources, and client for in memory identity server setup

```cs
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
                new IdentityResources.Email(),
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
                        "is.FatherName",
                    },
                    RedirectUris = new[] { "https://localhost:44383/signin-oidc"},
                    AllowedGrantTypes = GrantTypes.Code,
                    // adding claims in id token
                    AlwaysIncludeUserClaimsInIdToken = false,
                    //RequireConsent = true
                },
                new Client {
                    ClientId = "client_angular",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "ApiToBeSecured"
                    },

                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                }
            };
        }

    }
```


### Path for OpenId Connect Discovery Document

https://**server**/.well-known/openid-configuration