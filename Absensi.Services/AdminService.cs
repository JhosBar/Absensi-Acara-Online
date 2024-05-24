using Absensi.EF.Data;
using Absensi.LIB;
using Absensi.Services.Base;
using Absensi.Services.Interface;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.Services
{
    public class AdminService : IAdminService
    {
        public BaseResponse<bool> Register(AdminCreate req)
        {
            var response = new BaseResponse<bool>();
            using (var ctx = new AbsensiContext())
            {
                var check = (from b in ctx.MtAdmins
                             where b.Username == req.Username
                             select b).FirstOrDefault();

                if (check != null)
                {
                    response.Message = check.Username + " already exist.";
                    return response;
                }
                if (req.Password != req.PasswordSalt)
                {
                    response.Message = "Passwords doesn't match.";
                    return response;
                }

                var newData = new MtAdmin();

                newData.Created = DateTime.UtcNow;
                newData.CreatedBy = req.CreatedBy;

                newData.Username = req.Username;
                newData.Email = req.Email;

                string key = Security.RandomString(60);
                newData.PasswordSalt = key;
                newData.Password = Security.CheckHMAC(key, req.Password);

                newData.RoleId = req.RoleId;
                newData.Status = "ENABLED";

                ctx.MtAdmins.Add(newData);
                ctx.SaveChanges();

                response.Result = true;
                response.Message = "User " + req.Username;

                return response;
            }
        }

        public BaseResponse<List<AdminList>> Read(Paging paging, string qUsername, string qEmail, string qStatus)
        {
            var m = new BaseResponse<List<AdminList>>();
            using (var ctx = new AbsensiContext())
            {
                var getData = from l in ctx.MtAdmins    
                              where (string.IsNullOrEmpty(qUsername) || l.Username != null && l.Username.ToLower().Contains(qUsername.ToLower()))
                              where (string.IsNullOrEmpty(qEmail) || l.Email != null && l.Email.ToLower().Contains(qEmail.ToLower()))
                              where (string.IsNullOrEmpty(qStatus) || l.Status != null && l.Status.ToLower().Contains(qStatus.ToLower()))
                              select new AdminList()
                              {
                                  Id = l.Id,
                                  Username = l.Username,
                                  Email = l.Email,
                                  Status = l.Status,
                                  RoleId = l.RoleId,
                                  Role = l.Role.Role,
                              };


                if (paging.Col.ToLower() == "username")
                {
                    if (paging.Dir == "asc")
                    {
                        getData = getData.OrderBy(x => x.Username);
                    }
                    else
                    {
                        getData = getData.OrderByDescending(x => x.Username);
                    }
                }
                if (paging.Col.ToLower() == "email")
                {
                    if (paging.Dir == "asc")
                    {
                        getData = getData.OrderBy(x => x.Email);
                    }
                    else
                    {
                        getData = getData.OrderByDescending(x => x.Email);
                    }
                }
                if (paging.Col.ToLower() == "role")
                {
                    if (paging.Dir == "asc")
                    {
                        getData = getData.OrderBy(x => x.Role);
                    }
                    else
                    {
                        getData = getData.OrderByDescending(x => x.Role);
                    }
                }
                if (paging.Col.ToLower() == "status")
                {
                    if (paging.Dir == "asc")
                    {
                        getData = getData.OrderBy(x => x.Status);
                    }
                    else
                    {
                        getData = getData.OrderByDescending(x => x.Status);
                    }
                }

                m.Total = getData.Count();
                m.Result = getData.Skip(paging.Start).Take(paging.Length).ToList();
            }
            return m;
        }

        public BaseResponse<bool> Update(AdminUpdate req)
        {
            var w = new BaseResponse<bool>();
            using (var ctx = new AbsensiContext())
            {
                var db = (from l in ctx.MtAdmins
                             where l.Id == req.Id
                             select l).FirstOrDefault();

                if (db == null)
                {
                    w.Result = false;
                    w.Message = "Failed to update, the data doesn't exist.";
                }
                else
                {
                    string status;
                    if(req.Status == true)
                    {
                        status = "ENABLED";
                    }
                    else
                    {
                        status = "DISABLED";
                    }
                    db.UpdatedBy = req.UpdatedBy;
                    db.Updated = DateTime.UtcNow;

                    db.Email = req.Email;
                    db.RoleId = req.RoleId;
                    db.Status = status;

                    ctx.SaveChanges();

                    w.Result = true;
                    w.Message = "Update Success";
                }
            }
            return w;
        }

    }
}
