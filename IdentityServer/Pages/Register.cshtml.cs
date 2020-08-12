using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ReturnUrl { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(Username);
                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                }
            }
            //
            return Page();
        }

    }
}