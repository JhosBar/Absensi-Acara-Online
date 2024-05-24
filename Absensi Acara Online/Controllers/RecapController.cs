using Absensi.Models;
using Absensi.Services.Base;
using Absensi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Absensi.Services.Interface;
using Microsoft.AspNetCore.StaticFiles;

namespace Absensi_Acara_Online.Controllers
{
    [Authorize]
    public class RecapController : Controller
    {
        private IRecapService RecapService = new RecapService();
        private BaseResponse<bool> response = new BaseResponse<bool>();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Read(DatatableVM data, RecapFilterVM filterValue)
        {
            var dir = data.order.Select(terserah => terserah["dir"]).FirstOrDefault();
            var colName = data.columns[Convert.ToInt16(data.order.Select(terserah => terserah["column"]).FirstOrDefault())].data;

            var reqData = RecapService.Read(new Paging()
            {
                Dir = dir,
                Col = colName,
                Start = data.start,
                Length = data.length,
            }, new RecapFilterData()
            {
                qEvent = filterValue.qEvent,
                qAttender = filterValue.qAttender,
            });

            return Json(new { draw = data.draw, recordsFiltered = reqData.Total, recordsTotal = reqData.Total, data = reqData.Result });
        }

        [HttpGet]
        public IActionResult GetSignature(string folder, string namaFile)
        {
            try
            {
                var FilePath = $"D:/Project_Image/{folder}/{namaFile}";
                var oFile = System.IO.File.OpenRead(FilePath);

                string ContentType = "";
                var GetMime = new FileExtensionContentTypeProvider().TryGetContentType(FilePath, out ContentType);
                return File(
                    fileStream: oFile,
                    contentType: ContentType
                );
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Account");
            }

        }
    }
}
