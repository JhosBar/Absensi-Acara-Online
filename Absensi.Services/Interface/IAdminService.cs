using Absensi.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.Services.Interface
{
    public interface IAdminService
    {
        public BaseResponse<bool> Register(AdminCreate req);
        public BaseResponse<List<AdminList>> Read(Paging paging, string qUsername, string qEmail, string qStatus);
        public BaseResponse<bool> Update(AdminUpdate req);

    }
}
