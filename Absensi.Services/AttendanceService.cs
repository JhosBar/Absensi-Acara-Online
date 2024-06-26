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

            if (req.Name.Any(char.IsDigit))
            {
                response.Result = false;
                response.Message = "Invalid Name";
                return response;
            }

            if (!req.Email.EndsWith("@gmail.com") && !req.Email.EndsWith("@yahoo.com"))
            {
                response.Result = false;
                response.Message = "Invalid Email";
                return response;
            }

            if (!req.Phone.All(char.IsDigit))
            {
                response.Result = false;
                response.Message = "Invalid Phone number";
                return response;
            }

            using (var ctx = new AbsensiContext())
            {
                var check = (from b in ctx.PRecaps
                             where b.AttenderEmail == req.Email
                             select b).FirstOrDefault();

                if (check != null)
                {
                    response.Message = check.AttenderEmail + " already exists.";
                    response.Result = false;
                    return response;
                }

                var newData = new PRecap
                {
                    Created = DateTime.UtcNow,
                    AttenderName = req.Name,
                    AttenderPhone = req.Phone,
                    AttenderEmail = req.Email,
                    ASignature = Tool.SaveSignatureImage(req.Signature),
                    EventId = req.EventId
                };

                ctx.PRecaps.Add(newData);
                ctx.SaveChanges();

                response.Result = true;
                response.Message = "Successfully recorded the data";
            }

            return response;
        }

    }
}
