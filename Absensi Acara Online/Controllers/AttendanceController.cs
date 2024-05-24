using Absensi.Helper;
using Absensi.Models;
using Absensi.Services.Base;
using Absensi.Services;
using Microsoft.AspNetCore.Mvc;
using Absensi.Services.Interface;

namespace Absensi_Acara_Online.Controllers
{
	public class AttendanceController : Controller
	{
        private IAttendanceService AttendanceService = new AttendanceService();
        private BaseResponse<bool> response = new BaseResponse<bool>();

        public IActionResult Index(long id)
		{

			return View();	
		}

		public IActionResult Form(long id, string eventName, string eventDate)
		{
            ViewBag.Id = id;
            ViewBag.Event = eventName;
            ViewBag.Date = eventDate;
			return View();
		}

        [HttpPost]
        public JsonResult Attend(AttendVM data)
        {
            if (!ModelState.IsValid)
            {
                response.Message = "Please fill all the required fields";
                return Json(response);
            }

            var Get = AttendanceService.Attend(new AttendData()
            {
                EventId = data.EventId,
                Name = data.Name,
                Phone = data.Phone,
                Email = data.Email,
                Signature = data.Signature,
            });
            return Json(Get);
        }
    }
}