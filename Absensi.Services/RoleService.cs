using Absensi.Services.Base;
using Absensi.EF.Data;
using Absensi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.Services
{
    public class RoleService : IRoleService
    {
        public BaseResponse<List<RoleList>> GetRoleList()
        {
            var m = new BaseResponse<List<RoleList>>();
            using (var ctx = new AbsensiContext())
            {
                var getRole = from n in ctx.MtRoles
                              select new RoleList()
                              {
                                  Id = n.Id,
                                  Role = n.Role,
                              };

                m.Result = getRole.ToList();
            }
            return m;
        }
    }
}
