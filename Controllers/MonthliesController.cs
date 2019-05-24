using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DavidProject.Models;
using DavidProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DavidProject.Controllers
{
    [Produces("application/json")]
    [Route("api/Monthlies")]
    public class MonthliesController : Controller
    {
        private readonly IMonthlies task;
        private readonly UserManager<ApplicationUser> userManager;
        private ILogger<MonthlyTask> logger;
        public MonthliesController(IMonthlies task, UserManager<ApplicationUser> userManager, ILogger<MonthlyTask> logger)
        {
            this.userManager = userManager;
            this.task = task;
            this.logger = logger;
        }
        public IActionResult GetItems()
        {
            var result = task.GetItems();
            return Ok(result);

        }
        [HttpGet("{id}")]
        public IActionResult GetItem(int? id)
        {
            if (id == null)
            {
                logger.LogInformation("item id was null");
                return NotFound(new Response<IEnumerable<MonthlyTask>>()
                {
                    Message = "the item could not be found",
                    Successful = false
                });
            }
            var result = task.GetItem(id);
            logger.LogInformation($"found {result}, with id {id}");
            return Ok(result);
        } 

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]MonthlyTask obj)
        {
            if (ModelState.IsValid)
            {
                var userid = userManager.GetUserId(HttpContext.User);
                var user = userManager.GetUserAsync(HttpContext.User);
                obj.ApplicationUser = await user;
                obj.UserId = userid;
                task.Create(obj);
                return Ok();
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
        public IActionResult Delete([FromRoute]int? id)
        {
            if (id==null)
            {
                return NotFound(new Response<MonthlyTask>
                {
                    Successful = false,
                    Message = "item was not found "
                });
            }
            task.Delete(id);
            return NoContent();
        }

        [Route("GetUserItem")]
        [HttpGet]
        public async Task<IActionResult> GetItemByUser()
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            MonthlyDto[] result = task.GetItemByUser(user, user.Id).Select(item => new MonthlyDto
            {
                Day = item.Day,
                nth = item.nth,
                Id = item.Id,
                Name = item.Name
            }).ToArray();

            if (result==null)
            {
                logger.LogInformation("user items cannot be found");
                return NotFound(new Response<IEnumerable<item>>()
                {
                    Message = "Sorry we could not find the user",
                    Successful = false,

                });
            }
            return Ok(result);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int? id,MonthlyTask item)
        {
            if (ModelState.IsValid)
            {
                if (id==null)
                {
                    logger.LogInformation("id was null");
                    return BadRequest(new Response<MonthlyTask>()
                    {
                        Message = "sorry we could not find item on the database",
                        Successful = false
                    });
                }
                task.Update(id, item);
                return Ok(new Response<IEnumerable<item>>()
                {
                    Message = "Sucessful",
                    Successful = true
                });
            }
            return BadRequest(error: "modelstate is not valid");
        }

    }   
        
}

