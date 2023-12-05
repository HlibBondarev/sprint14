using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsWithRouting.Models;
using System.Diagnostics;
using ProductsWithRouting.Services;

namespace ProductsWithRouting.Controllers
{
    public class UsersController : Controller
    {
		private List<User> myUsers;
		private string password = "df2323eoT";

		public UsersController(Data data)
        {
            myUsers = data.Users;
        }
		[Route("{controller}")]
		[Route("{controller}/{action}")]
		public IActionResult Index([FromBody] string id)
		{
			if (id == password)
			{
				return View(myUsers);
			}
			return Unauthorized();
		}

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
	}
}
