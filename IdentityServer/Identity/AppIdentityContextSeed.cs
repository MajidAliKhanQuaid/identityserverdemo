using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Identity
{
    public class AppIdentityContextSeed
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin";
                user.Email = "admin@ds.com";

                IdentityResult userResult = userManager.CreateAsync(user, "123456")
                    .ContinueWith(_ => userManager.AddClaimAsync(user, new Claim(ClaimTypes.DateOfBirth, new DateTime(1994, 4, 9).ToString("yyyyMMdd"))).Result, TaskContinuationOptions.ExecuteSynchronously)
                    .ContinueWith(_ => userManager.AddClaimAsync(user, new Claim(ClaimTypes.Country, "PK")).Result, TaskContinuationOptions.ExecuteSynchronously)
                    .ContinueWith(_ => userManager.AddClaimAsync(user, new Claim(ClaimTypes.PostalCode, "9444")).Result, TaskContinuationOptions.ExecuteSynchronously).Result;

                if (userResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

            if (userManager.FindByNameAsync("user1").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "user";
                user.Email = "user@ds.com";


                IdentityResult userResult = userManager.CreateAsync(user, "123456")
                    .ContinueWith(_ => userManager.AddClaimAsync(user, new Claim(ClaimTypes.DateOfBirth, new DateTime(2012,12,12).ToString("yyyyMMdd"))).Result, TaskContinuationOptions.ExecuteSynchronously)
                    .ContinueWith(_ => userManager.AddClaimAsync(user, new Claim(ClaimTypes.Country, "PK")).Result, TaskContinuationOptions.ExecuteSynchronously)
                    .ContinueWith(_ => userManager.AddClaimAsync(user, new Claim(ClaimTypes.PostalCode, "12000")).Result, TaskContinuationOptions.ExecuteSynchronously).Result;

                if (userResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "NormalUser").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Customer").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Customer";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

        }
    }
}
