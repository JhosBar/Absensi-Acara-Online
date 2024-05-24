using Absensi.EF.Data;
using Absensi.Services.Base;
using Absensi.Services.Interface;
using Absensi.LIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.Services
{
    public class AttendanceService : IAttendanceService
    {
        public BaseResponse<bool> Attend(AttendData req)
        {
            var response = new BaseResponse<bool>();
            using (var ctx = new AbsensiContext())
            {
                var check = (from b in ctx.PRecaps
                             where b.AttenderEmail == req.Email
                             select b).FirstOrDefault();

                if (check != null) 
                {
                    response.Message = check.AttenderEmail + " already exist.";
                }

                var newData = new PRecap();

                newData.Created = DateTime.UtcNow;

                newData.AttenderName = req.Name;
                newData.AttenderPhone = req.Phone;
                newData.AttenderEmail = req.Email;
                newData.ASignature = Tool.SaveSignatureImage(req.Signature);
                newData.EventId = req.EventId;

                ctx.PRecaps.Add(newData);
                ctx.SaveChanges();

                response.Result = true;
                response.Message = "Successfully record the data";

                return response;
            }
        }
    }
}
