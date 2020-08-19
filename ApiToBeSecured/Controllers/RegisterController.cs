using System;
using Microsoft.AspNetCore.Mvc;
using ApiToBeSecured.Data;
using ApiToBeSecured.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ApiToBeSecured.Controllers
{
    //[Route("api/[controller]")]
    //public class RegisterController : Controller
    //{
    //    private readonly ECommDbContext _appDbcontext;
    //    private readonly UserManager<IdentityUser> _userManager;
    //    public RegisterController(ECommDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
    //    {
    //        _appDbcontext = applicationDbContext;
    //        _userManager = userManager;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Post([FromBody] RegistrationViewModel registrationViewModel)
    //    {
    //        if(ModelState.IsValid)
    //        {
    //            var user = new ApplicationUser{
    //                UserName = registrationViewModel.UserName,
    //                Email = registrationViewModel.Email
    //            };
    //            //create user and add simpleuser role
    //            try{
    //                var result = await _userManager.CreateAsync(user,registrationViewModel.Password);
    //                if(result.Succeeded)
    //                {
    //                    await _userManager.AddToRoleAsync(user,Constants.SimpleUser);
    //                    return Ok();
    //                }
    //                else return Ok(result);
    //            }
    //            catch{
    //                return BadRequest("Can Not Create User");
    //            }
    //        }
    //        else return BadRequest(ModelState);
    //    }
    //}
}