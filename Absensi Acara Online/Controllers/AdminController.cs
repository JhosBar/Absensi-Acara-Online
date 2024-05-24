using Absensi.Helper;
using Absensi.Models;
using Absensi.Services.Base;
using Absensi.Services;
using Absensi.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Absensi_Acara_Online.Controllers
{
    [Authorize]
    public class AdminController : Controller
	{
        private IAdminService AdminService = new AdminService();
        private IRoleService RoleService = new RoleService();

        private BaseResponse<bool> response = new BaseResponse<bool>();

        public IActionResult Index()
		{
            var roles = RoleService.GetRoleList();

            List<SelectListItem> rolelist = new List<SelectListItem>();
            foreach (var v in roles.Result)
            {
                rolelist.Add(new SelectListItem() { Value = v.Id.ToString(), Text = v.Role });
            }
            ViewBag.RoleList = rolelist;

            return View();
		}

        [HttpPost]
        public JsonResult Create(AdminCreateVM data)
        {
            if (!ModelState.IsValid)
            {
                response.Message = "Please fill all the required fields";
                return Json(response);
            }

            var Get = AdminService.Register(new AdminCreate()
            {
                Username = data.Username,
                Email = data.Email,
                Password = data.Password,
                PasswordSalt = data.PasswordSalt,
                CreatedBy = User.GetTheUsername(),
                RoleId = data.RoleId,
            });
            return Json(Get);
        }

        [HttpPost]
        public JsonResult Read(DatatableVM data, string qUsername, string qEmail, string qStatus)
        {
            var dir = data.order.Select(row => row["dir"]).FirstOrDefault();
            var colname = data.columns[Convert.ToInt16(data.order.Select(row => row["column"]).FirstOrDefault())].data;

            var getEverything = AdminService.Read(new Paging()
            {
                Dir = dir,
                Col = colname,
                Start = data.start,
                Length = data.length,
            }, qUsername, qEmail, qStatus);

            return Json(new { draw = data.draw, recordsFiltered = getEverything.Total, recordsTotal = getEverything.Total, data = getEverything.Result });
        }

        [HttpPost]
        public JsonResult Update(AdminUpdateVM data)
        {
            bool status = false;

            if (data.Status == "on")
            {
                status = true;
            }

            var result = AdminService.Update(new AdminUpdate()
            {
                Id = data.Id,
                Email = data.Email,
                RoleId = data.RoleId,
                Status = status,
                UpdatedBy = User.GetTheUsername(),
            });
            return Json(result);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult SetPassword(AdminSetPassVM Model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var Send = AdminService.SetPassword(
        //            Model.RoleId,
        //            Model.Password, 
        //            User.GetTheUsername()
        //        );
        //        if (Send.Sts == false) { return Json(Helper.GetLang(ViewBag.LangKey, Send.Msg)); }
        //        return Json(true);
        //    }
        //    return Json(false);
        //}
    }
}
