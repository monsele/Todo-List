using DavidProject.Models;
using DavidProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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
        private ILogger<item> logger;
        
        public itemController(IItem repo, UserManager<ApplicationUser> userManager,ILogger<item>logger)
		{
			this.repo = repo;
			this.userManager = userManager;
            this.logger = logger;
		}
		
        
        [AllowAnonymous]
		[HttpGet]
		public IActionResult GetItems()
		{
            logger.LogInformation("Bringing in items");
			var result = repo.GetItems();
            
			return Ok(result);
		}
		
		[HttpGet("{id}")]
		public IActionResult GetItem(int? id)
		{
			if (id == null)
			{
                logger.LogInformation("item id was null");
				return NotFound(new Response<IEnumerable<item>>()
				{
					Message = "the item could nof be found",
					Successful = false
				});
			}
			var result = repo.GetItem(id);
            logger.LogInformation($"found {result}, with id{id}");
            return Ok(result);
			//return Ok(new Response<item>()
			//{
			//	Message = "Sucessfully created item",
			//	Successful = true,
			//	Data = result
			//});
		}
		[HttpPost]
		public async Task<IActionResult> CreateItem([FromBody]item obj)
		{
            
            if (ModelState.IsValid)
            {
                ItemModel dto = new ItemModel();
                var userid = userManager.GetUserId(HttpContext.User);
                var user = userManager.GetUserAsync(HttpContext.User);
                obj.ApplicationUser = await user;
                obj.UserId = userid;
               
                obj.Priority.ToString();
                repo.Create(obj);
                logger.LogCritical(obj.Name);
                logger.LogInformation("created an item");
                return Ok(new Response<item>()
                {
                    Message = "Sucessfully created item",
                    Successful = true

                });
            }
            else
            {
                var err = ModelState.Select(x => x.Value.Errors).ToArray();
                logger.LogCritical($"these are model errors{ err.ToString()}");
                return BadRequest(new Response<IEnumerable<object>>()
                {
                    Message = "Sorry something got wrong",
                    Successful = false,
                    Data = err
                });
            }
        }
		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute]int id)
		{
			repo.Delete(id);
            logger.LogDebug("Item has been deleted");
			return Ok(new Response<IEnumerable<item>>()
			{
				Message = "Sucessfully deleted the item",
				Successful = true
			});
		}
		[Route("GetUserItem")]
		[HttpGet]
		public async Task<IActionResult> GetItemOfUser()
		{
             //id = userManager.GetUserId(HttpContext.User);
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            logger.LogInformation(user.NormalizedUserName);
            ItemModel[] result =repo.GetItemByUser(user, user.Id).Select(item => new ItemModel {
                isCompleted = item.isCompleted,
                Description = item.Description,
                DueDate = item.DueDate,
                Id = item.Id,
                Name = item.Name,
                Priority=item.Priority
            }).ToArray();

           

			if (result==null)
			{
                logger.LogInformation("user cannot be found");
				return NotFound(new Response<IEnumerable<item>>()
				{
					Message = "Sorry we could not find the user",
					Successful = false,

				});
			}


			return Ok(result);
		}

        [HttpPut("{id}")]
        public  IActionResult Update(int? id, [FromBody]item obj)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    logger.LogInformation("id was null");
                    return BadRequest(new Response<item>()
                    {
                        Message = "sorry we could not find item on the database",
                        Successful = false
                    });
                }
                repo.Update(id, obj);
                return Ok(new Response<IEnumerable<item>>()
                {
                    Message = "Sucessful",
                    Successful = true
                });
            }
            return BadRequest();
        }

       
	}
}
