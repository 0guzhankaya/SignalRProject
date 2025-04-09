using Microsoft.AspNetCore.Mvc;

namespace SignalR.WebUI.Controllers
{
	public class AdminLayout : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
