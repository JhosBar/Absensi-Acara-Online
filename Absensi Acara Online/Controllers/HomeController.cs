using Absensi.Services.Base;
using Absensi.Services.Interface;
using Absensi.Services;
using Absensi_Acara_Online.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Absensi_Acara_Online.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
		private IEventService EventService = new EventService();
		private BaseResponse<bool> response = new BaseResponse<bool>();

        //public HomeController(ILogger<HomeController> logger)
        //{
        //          _logger = logger;
        //      }

        public IActionResult Index()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		[HttpGet]
		public JsonResult GetCard(string qEventSearch)
		{
			var req = EventService.GetEventList(qEventSearch);
			return Json(req);
		}
	}
}
