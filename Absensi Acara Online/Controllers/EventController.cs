using Absensi.Helper;
using Absensi.Models;
using Absensi.Services.Base;
using Absensi.Services;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Absensi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Absensi_Acara_Online.Controllers
{
    [Authorize]
    public class EventController : Controller
	{

        private IEventService EventService = new EventService();
        private BaseResponse<bool> response = new BaseResponse<bool>();

        public IActionResult Index()
		{
            //var attenders = EventService.GetAttenderList();

            //List<SelectListItem> attenderlist = new List<SelectListItem>();
            //foreach (var v in attenders.Result)
            //{
            //    attenderlist.Add(new SelectListItem() { Value = v.Id.ToString(), Text = v.Role });
            //}
            //ViewBag.AttenderList = attenderlist;

            return View();
        }

        [HttpPost]
        public JsonResult Create(EventCreateVM data)
        {
            if (!ModelState.IsValid)
            {
                response.Message = "Please fill all the required fields";
                return Json(response);
            }

            var Get = EventService.Create(new EventCreate()
            {
                Event = data.Event,
                Hour = data.Hour,
                Date = data.Date,
                Location = data.Location,
                Note = data.Note,
                CreatedBy = User.GetTheUsername(),
            });
            return Json(Get);
        }

        [HttpPost]
        public JsonResult Read(DatatableVM data, EventFilterVM filterValue)
        {
            var dir = data.order.Select(terserah => terserah["dir"]).FirstOrDefault();
            var colName = data.columns[Convert.ToInt16(data.order.Select(terserah => terserah["column"]).FirstOrDefault())].data;

            var reqData = EventService.Read(new Paging()
            {
                Dir = dir,
                Col = colName,
                Start = data.start,
                Length = data.length,
            }, new EventFilterData()
            {

                qEvent = filterValue.qEvent,
                qLocation = filterValue.qLocation,
                qDateStart = filterValue.qDateStart,
                qDateEnd = filterValue.qDateEnd,
            });

            return Json(new { draw = data.draw, recordsFiltered = reqData.Total, recordsTotal = reqData.Total, data = reqData.Result });
        }

        public JsonResult Update(EventUpdateVM data)
        {
            var response = new BaseResponse<bool>();
            if (data.Id <= 0) 
            { 
                response.Message = "Failed";
                return Json(response.Result); 
            }

            var res = EventService.Update(new EventData
            {
                Id = data.Id,
                Event = data.Event,
                Date = data.Date,
                Hour = data.Hour,
                Location = data.Location,
                Note = data.Note,
                UpdatedBy = User.GetTheUsername(),
            });
            return Json(res);
        }

        [HttpPost]
        public JsonResult Delete(long id, string Event)
        {
            if (id >= 0)
            {
                var res = EventService.Delete(id, Event);
                return Json(res);
            }
            return Json(false);
        }

        [HttpGet]
        public JsonResult GetAttenderList(long id, string qAttendeesSearch)
        {
            var req = EventService.GetAttenderList(id, qAttendeesSearch);
            return Json(req);
        }
    }
}
