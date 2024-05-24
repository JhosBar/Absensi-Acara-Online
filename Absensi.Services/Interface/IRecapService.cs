using Absensi.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.Services.Interface
{
	public interface IRecapService
	{
        public BaseResponse<List<RecapData>> Read(Paging paging, RecapFilterData filterValue);

    }
}
