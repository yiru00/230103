using _230103.Models;
using Microsoft.AspNetCore.Mvc;

namespace _230103.Controllers
{
	public class AjaxController : Controller
	{
		NorthwindContext _context=null;
		public AjaxController(NorthwindContext context)
		{
			_context= context;
		}

		[HttpGet]
		public string Greet(string Name)
		{
			Thread.Sleep(3000);
			return $"Hello, {Name}!";
		}

		[HttpPost,ActionName("Greet")]
		public string PostGreet(string Name)
		{
			Thread.Sleep(3000);
			return $"Hello, {Name}!";
		}

		[HttpPost]
		public string CheckName(string CompanyName)
		{
			bool Exists = _context.Customers.Any(c => c.CompanyName == CompanyName);
			return Exists ? "true" : "false";
		}
	}
}
