using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebAppToBeSecured.Pages
{
    public class ProtectedPage : PageModel
    {
        private readonly ILogger<ProtectedPage> _logger;

        public ProtectedPage(ILogger<ProtectedPage> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            // for that -- SaveTokens should be true in jwt settings
            String strAccessToken = await HttpContext.GetTokenAsync("access_token");
            String strIdToken = await HttpContext.GetTokenAsync("id_token");
            //
            var accessToken = new JwtSecurityTokenHandler().ReadJwtToken(strAccessToken);
            var idToken = new JwtSecurityTokenHandler().ReadJwtToken(strIdToken);
            //
            return Page();
        }
    }
}
