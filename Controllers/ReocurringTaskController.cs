using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DavidProject.Models;
using DavidProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DavidProject.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/ReocurringTask")]
    public class ReocurringTaskController : Controller
    {
		private readonly IReocurring task;
        private readonly UserManager<ApplicationUser> userManager;
        public ReocurringTaskController(IReocurring task, UserManager<ApplicationUser> userManager)
		{
			this.task = task;
            this.userManager = userManager;
        }
        // GET: api/ReocurringTask
        [HttpGet]
        public IActionResult  GetAllTasks()
        {
			var result = task.GetReoccurrings();
			return Ok(result);

        }

        // GET: api/ReocurringTask/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetTask(int id)
        {
			var soln = task.GetReoccurring(id);
			return Ok(soln);
        }
        
        // POST: api/ReocurringTask
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Reoccurring obj)
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
				ModelState.AddModelError("","wrong input");
				return BadRequest(ModelState);
			}
        }
        
        // PUT: api/ReocurringTask/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, [FromBody]Reoccurring obj)
        {
            if (id==null)
            {
                return BadRequest(new Response<IEnumerable<Reoccurring>>()
                {
                    Message = "sorry we cannot find the task",
                    Successful = false,
                });
            }
            if (ModelState.IsValid)
            {
                task.Update(id, obj);
                return Ok(new Response<Reoccurring>()
                {
                    Message = "Sucessfully created item",
                    Successful = true,
                    Data = obj
                });
            }
            return NoContent();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id==null)
            {
                return BadRequest(new Response<Reoccurring>()
                {
                    Message = "Sorry there is something wrong",
                    Successful = false
                });
            }
			task.Delete(id);
			return Ok();
		}
    }
}
