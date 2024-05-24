using Absensi.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absensi.Services.Interface
{
	public interface IAccountService
	{
		public BaseResponse<AccountLogin> Login(AccountLogin req);
		public BaseResponse<bool> ChangePass(AccountPass req);
	}
}
