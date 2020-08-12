using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages
{
    public class LogInModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogInModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [Required]
        //[EmailAddress]
        [BindProperty]
        public string Username { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Enter valid password")]
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(Username, Password, false, false);
                if (signInResult.Succeeded)
                {
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