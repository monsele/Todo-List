using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using DavidProject.Dto;
using DavidProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DavidProject.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
		//Post:api/Account/Register
		
		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterDto model)
		{
			
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = model.Username };
				if (user!=null)
				{
					var result = await userManager.CreateAsync(user, model.Password);
				
					if (result.Succeeded)
					{
						await signInManager.SignInAsync(user, false);//note that in production is persistent value should be true
						return Ok(new Response<IEnumerable<LoginDto>>()
						{
							Message = "Sucessful registration! Welcome to your new Account",
							Successful = true
						});
					}
				   	
				}
			
			}
			return BadRequest(ModelState);

		}
		[Route("Login")]
		[HttpPost()]
		public async Task<IActionResult>Login(LoginDto dto)
		{
			if (ModelState.IsValid)
			{
				var signResult = await signInManager.PasswordSignInAsync(dto.Username, dto.Password,false,false);//note that in production ispersistent will be true
				if (signResult.Succeeded)
				{
					return Ok(new Response<IEnumerable<LoginDto>>()
					{
						Message="Sucessfully signed in",
						Successful = true
					});
				}
				return BadRequest(ModelState);
			}
			
			return BadRequest(ModelState);
		}
    }
}