using Absensi.EF.Data;
using Absensi.Services.Base;
using Absensi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.Services
{
    public class RecapService : IRecapService
    {
        public BaseResponse<List<RecapData>> Read(Paging paging, RecapFilterData filterValue)
        {
            var m = new BaseResponse<List<RecapData>>();
            using (var ctx = new AbsensiContext())
            {
                var GMT = Convert.ToInt32(ConfigurationManager.AppSettings["GMT"]);

                var database = from v in ctx.PRecaps
                               where v.FlgDeleted != true
                               && (string.IsNullOrEmpty(filterValue.qEvent) || v.Event.Name != null && v.Event.Name.Contains(filterValue.qEvent))
                               && (string.IsNullOrEmpty(filterValue.qAttender) || v.AttenderName != null && v.AttenderName.Contains(filterValue.qAttender))
                               select new RecapData()
                               {
                                   Id = v.Id,
                                   EventId = v.EventId,
                                   Event = v.Event.Name,

                                   attenderName = v.AttenderName,
                                   attenderPhone = v.AttenderPhone,
                                   attenderEmail = v.AttenderEmail,
                                   aSignature = v.ASignature,

                                   Created = v.Created,
                                   CreatedText = v.Created.HasValue ? v.Created.Value.ToString("dd/MM/yyyy - HH:mm:ss") : "",
                               };

                if (paging.Col == ("Event").ToLower()) { database = paging.Dir == "asc" ? database.OrderBy(x => x.Event) : database.OrderByDescending(x => x.Event); }
                else if (paging.Col == ("attenderName")) { database = paging.Dir == "asc" ? database.OrderBy(x => x.attenderName) : database.OrderByDescending(x => x.attenderName); }
                else if (paging.Col == ("attenderPhone")) { database = paging.Dir == "asc" ? database.OrderBy(x => x.attenderPhone) : database.OrderByDescending(x => x.attenderPhone); }
                else if (paging.Col == ("createdText")) { database = paging.Dir == "asc" ? database.OrderByDescending(x => x.Created) : database.OrderBy(x => x.Created); }

                m.Total = database.Count();
                m.Result = database.Skip(paging.Start).Take(paging.Length).ToList();
            }
            return m;
        }
    }
}
