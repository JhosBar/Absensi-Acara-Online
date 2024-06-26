using Absensi.Services.Base;
using Absensi.EF.Data;
using Absensi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Absensi.LIB;
using Microsoft.IdentityModel.Tokens;
using Azure;
using System.Globalization;
using System.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Xsl;

namespace Absensi.Services
{
    public class EventService : IEventService
    {
        public BaseResponse<bool> Create(EventCreate req)
        {
            var response = new BaseResponse<bool>();
            using (var ctx = new AbsensiContext())
            {
                DateOnly parsedDate = DateOnly.MinValue;
                try
                {
                    if (!string.IsNullOrEmpty(req.Date))
                    {
                        string[] dte = req.Date.Split('/'); // assuming format: dd/MM/yyyy
                        parsedDate = new DateOnly(int.Parse(dte[2]), int.Parse(dte[1]), int.Parse(dte[0]));
                    }
                }
                catch (Exception) { response.Message = "Invalid Date"; return response; }

                DateTime parsedHour = DateTime.MinValue;

                try
                {
                    if (!string.IsNullOrEmpty(req.Hour))
                    {
                        string[] startTimeParts = req.Hour.Split(':'); // HH:mm
                        if (startTimeParts.Length == 2)
                        {
                            int startHour = int.Parse(startTimeParts[0]);
                            int startMinute = int.Parse(startTimeParts[1]);

                            if (startHour >= 0 && startHour <= 23 && startMinute >= 0 && startMinute <= 59)
                            {
                                parsedHour = new DateTime(1753, 1, 1, startHour, startMinute, 0);
                            }
                            else
                            {
                                response.Message = "Invalid Start Time";
                                return response;
                            }
                        }
                        else
                        {
                            response.Message = "Invalid Start Time Format";
                            return response;
                        }
                    }
                }
                catch (Exception)
                {
                    response.Message = "Invalid Time";
                    return response;
                }

                var check = (from b in ctx.MtEvents
                             where b.Name == req.Event
                             select b).FirstOrDefault();

                if (check != null)
                {
                    response.Message = "Event Exists";
                    return response;
                }

                var newData = new MtEvent();

                newData.Created = DateTime.UtcNow;
                newData.CreatedBy = req.CreatedBy;

                newData.Name = req.Event;
                newData.ELocation = req.Location;
                newData.EHour = parsedHour;
                newData.EDate = parsedDate;
                newData.ENote = req.Note;

                ctx.MtEvents.Add(newData);
                ctx.SaveChanges();

                response.Result = true;
                response.Message = req.Event + " Successly Created";

                return response;
            }
        }
        public BaseResponse<List<EData>> Read(Paging paging, EventFilterData filterValue)
        {
            var m = new BaseResponse<List<EData>>();
            using (var ctx = new AbsensiContext())
            {
                var GMT = Convert.ToInt32(ConfigurationManager.AppSettings["GMT"]);

                DateOnly startDate = DateOnly.Parse(filterValue.qDateStart ?? "");
                DateOnly endDate = DateOnly.Parse(filterValue.qDateEnd ?? "").AddDays(1);

                var database = from v in ctx.MtEvents
                               where v.FlgDeleted != true
                               && (string.IsNullOrEmpty(filterValue.qEvent) || v.Name != null && v.Name.Contains(filterValue.qEvent))
                               //&& (string.IsNullOrEmpty(filterValue.qLocation) || v.ELocation != null && v.ELocation.Contains(filterValue.qLocation))

                               && (v.EDate >= startDate && v.EDate < endDate)
                               select new EData()
                               {
                                   Id = v.Id,
                                   Event = v.Name,
                                   Location = v.ELocation ?? "",
                                   Note = v.ENote,

                                   Hour = v.EHour.HasValue ? v.EHour.Value.ToString("HH:mm") : "",

                                   Date = v.EDate,
                                   DateText = v.EDate.ToString(),
                                   Created = v.Created,
                                   //CreatedText = v.Created.HasValue ? v.Created.Value.ToString("dd/MM/yyyy - HH:mm:ss") : "",
                                   CreatedText = v.Created.HasValue ? v.Created.Value.AddHours(GMT).ToString("dd/MM/yyyy HH:mm:ss") : "",
                                   CreatedBy = v.CreatedBy ?? "",
                               };

                if (paging.Col == ("Event").ToLower()) { database = paging.Dir == "asc" ? database.OrderBy(x => x.Event) : database.OrderByDescending(x => x.Event); }
                else if (paging.Col == ("createdText").ToLower()) { database = paging.Dir == "asc" ? database.OrderBy(x => x.CreatedText) : database.OrderByDescending(x => x.CreatedText); }

                m.Total = database.Count();
                m.Result = database.Skip(paging.Start).Take(paging.Length).ToList();
            }
            return m;
        }

        public BaseResponse<bool> Update(EventData req)
        {
            var response = new BaseResponse<bool>();
            using (var ctx = new AbsensiContext())
            {
                DateOnly parsedDate = DateOnly.MinValue;
                try
                {
                    if (!string.IsNullOrEmpty(req.Date))
                    {
                        string[] dte = req.Date.Split('/'); // assuming format: dd/MM/yyyy
                        parsedDate = new DateOnly(int.Parse(dte[2]), int.Parse(dte[1]), int.Parse(dte[0]));
                    }
                }
                catch (Exception) { response.Message = "Invalid Date"; return response; }

                DateTime parsedHour = DateTime.MinValue;

                try
                {
                    if (!string.IsNullOrEmpty(req.Hour))
                    {
                        string[] startTimeParts = req.Hour.Split(':'); // HH:mm
                        if (startTimeParts.Length == 2)
                        {
                            int startHour = int.Parse(startTimeParts[0]);
                            int startMinute = int.Parse(startTimeParts[1]);

                            if (startHour >= 0 && startHour <= 23 && startMinute >= 0 && startMinute <= 59)
                            {
                                parsedHour = new DateTime(1753, 1, 1, startHour, startMinute, 0);
                            }
                            else
                            {
                                response.Message = "Invalid Start Time";
                                return response;
                            }
                        }
                        else
                        {
                            response.Message = "Invalid Start Time Format";
                            return response;
                        }
                    }
                }
                catch (Exception)
                {
                    response.Message = "Invalid Time";
                    return response;
                }

                var entity = (from i in ctx.MtEvents
                              where i.Id == req.Id
                              select i).FirstOrDefault();

                if (entity == null)
                {
                    response.Message = "Invalid Event";
                    return response;
                }

                entity.Name = req.Event;
                entity.EDate = parsedDate;
                entity.EHour = parsedHour;
                entity.ELocation = req.Location;
                entity.ENote = req.Note;

                entity.Updated = DateTime.Now;
                entity.UpdatedBy = req.UpdatedBy;

                ctx.SaveChanges();

                response.Result = true;
                response.Message = "Successfully Updated!";
            }
            return response;
        }

        public BaseResponse<bool> Delete(long id, string Event)
        {
            var res = new BaseResponse<bool>();
            using (var ctx = new AbsensiContext())
            {
                var entity = (from i in ctx.MtEvents
                              where i.Id == id
                              select i).FirstOrDefault();

                if (entity == null)
                {
                    res.Message = "InvalidAccount";
                    return res;
                }

                ctx.MtEvents.Remove(entity);
                //entity.FlgDeleted = true;
                ctx.SaveChanges();

                res.Result = true;
                res.Message = Event;
            }
            return (res);
        }

		public List<EventList> GetEventList(string qEventSearch)
		{
			using (var ctx = new AbsensiContext())
			{
				var database = from v in ctx.MtEvents
							   where v.FlgDeleted == false
                               && (string.IsNullOrEmpty(qEventSearch) || v.Name != null && v.Name.Contains(qEventSearch))
							   select new EventList
							   {
								   Id = v.Id,
								   Event = v.Name,

								   Date = v.EDate,
								   DateText = v.EDate.ToString(),

								   Time = v.EHour,
								   TimeText = v.EHour.HasValue ? v.EHour.Value.ToString("HH:mm") : "",

                                   Location = v.ELocation,
                                   Note = v.ENote,
							   };
				return database.OrderBy(x => x.Event).ToList();
			}
		}

        public List<AttenderList> GetAttenderList(long id, string qAttendeesSearch)
        {
            using (var ctx = new AbsensiContext())
            {
                var database = from v in ctx.PRecaps
                               where v.FlgDeleted == false
                               && (string.IsNullOrEmpty(qAttendeesSearch) || v.AttenderName != null && v.AttenderName.Contains(qAttendeesSearch))
                               && v.EventId == id
                               select new AttenderList
                               {
                                   Id = v.Id,
                                   Name = v.AttenderName,
                                   SubmitTime = v.Created,
                                   //SubmitTime = v.Created.HasValue ? v.Created.Value.ToString("dd/MM/yyyy - HH:mm:ss") : "",

                               };
                return database.ToList();
            }
        }

    }
}