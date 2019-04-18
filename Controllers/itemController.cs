using DavidProject.Models;
using DavidProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidProject.Controllers
{

	[Authorize]
	[Produces("application/json")]
	[Route("api/item")]
    public class itemController : Controller
    {
		private readonly IItem repo;
		private readonly UserManager<ApplicationUser> userManager;
		public itemController(IItem repo, UserManager<ApplicationUser> userManager)
		{
			this.repo = repo;
			this.userManager = userManager;
		}
		
		[HttpGet]
		public IActionResult GetItems()
		{
			var result = repo.GetItems();
			return Ok(new Response<IEnumerable<item>>()
			{
				Message = "Sucessfully created item",
				Successful = true,
				Data=result
			});
		}
		
		[HttpGet("{id}")]
		public IActionResult GetItem(int? id)
		{
			if (id == null)
			{
				return NotFound(new Response<IEnumerable<item>>()
				{
					Message = "the item could nof be found",
					Successful = false
				});
			}
			var result = repo.GetItem(id);
			return Ok(new Response<item>()
			{
				Message = "Sucessfully created item",
				Successful = true,
				Data = result
			});
		}
		[HttpPost]
		public async Task<IActionResult> CreateItem([FromBody]item obj)
		{
			
			if (ModelState.IsValid)
			{
				var userid = userManager.GetUserId(HttpContext.User);
				var user = userManager.GetUserAsync(HttpContext.User);
				obj.ApplicationUser = await user;
				obj.UserId =userid;
				repo.Create(obj);
				return Ok(new Response<item>()
				{
					Message = "Sucessfully created item",
					Successful = true
					
				});
			}
			else
			{
				return BadRequest(new Response<IEnumerable<item>>()
				{
					Message = "Sorry something got wrong",
					Successful = false
				});
			}
		}
		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute]int id)
		{

			repo.Delete(id);
			return Ok(new Response<IEnumerable<item>>()
			{
				Message = "Sucessfully deleted the item",
				Successful = true
			});
		}
		[Route("GetUserItem")]
		[HttpGet]
		public async Task<IActionResult> GetItemOfUser(ApplicationUser user,string id)
		{
             id = userManager.GetUserId(HttpContext.User);
            user = await userManager.GetUserAsync(HttpContext.User);
            var result =repo.GetItemByUser(user,id);
			if (result==null)
			{
				return NotFound(new Response<IEnumerable<item>>()
				{
					Message = "Sorry we could not find the user",
					Successful = false,

				});
			}
			return Ok(new Response<IEnumerable<item>>()
			{
				Message = "Sucessful",
				Successful = true,
				Data=result
			});
		}

        [HttpPut("Update")]
        public  IActionResult Update(int? id, item obj)
        {
            if (ModelState.IsValid)
            {
                repo.Update(id, obj);
                return Ok(new Response<IEnumerable<item>>()
                {
                    Message = "Sucessful",
                    Successful = true
                });
            }
            return NoContent();
        }

	}
}
