using DavidProject.Models;
using DavidProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace DavidProject.Controllers
{
	[Produces("application/json")]
    [Route("api/item")]
    public class itemController : Controller
    {
		private readonly IItem repo;
		public itemController(IItem repo)
		{
			this.repo = repo;
		}
		[HttpGet]
		public IActionResult GetItems()
		{
			var result = repo.GetItems();
			return Ok(result);
		}
		[HttpGet("{id}")]
		public IActionResult GetItem(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var result = repo.GetItem(id);
			return Ok(result);
		}
		[HttpPost]
		public IActionResult CreateItem([FromBody]item obj)
		{
			if (ModelState.IsValid)
			{
				repo.Create(obj);
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute]int id)
		{
			repo.Delete(id);
			return Ok();
		}

	}
}
