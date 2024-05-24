using Absensi.Services.Base;
using Absensi.Services.Interface;
using Absensi.LIB;
using Absensi.EF.Data;

namespace Absensi.Services
{
	public class AccountService : IAccountService
	{
		public BaseResponse<AccountLogin> Login(AccountLogin req)
		{
			var response = new BaseResponse<AccountLogin>();

			using (var ctx = new AbsensiContext())
			{
				var db = (from v in ctx.MtAdmins
						  where v.Username == req.Username
						  select v).FirstOrDefault();

				if (db == null)
				{
					response.Message = "Username not found.";
					return response;
				}

				if (db.Status.ToLower() != "enabled")
				{
					response.Message = "User doesn't exist.";
					return response;
				}

				var pass = Security.CheckHMAC(db.PasswordSalt, req.Password);
				if (pass != db.Password)
				{
					response.Message = "Wrong Password!";
					return response;
				}


				var getRole = (from d in ctx.MtRoles
							   where d.Id == db.RoleId
							   select d).FirstOrDefault();

				var give = new AccountLogin();

				give.Id = db.Id;
				give.Username = db.Username;
				give.RoleId = db.RoleId;
				give.Role = getRole.Role;

				response.Result = give;

				return response;

			}

		}

		public BaseResponse<bool> ChangePass(AccountPass req)
		{
			var response = new BaseResponse<bool>();
			using (var ctx = new AbsensiContext())
			{
				var database = (from l in ctx.MtAdmins
								where l.Id == req.Id
								select l).FirstOrDefault();

				if (database == null)
				{
					response.Message = "Data doesn't exist.";
					return response;
				}
				if (database.Id == 0)
				{
					response.Message = "Data doesn't exist.";
					return response;
				}

				var pass = Security.CheckHMAC(database.PasswordSalt, req.Current_Password);
				if (pass != database.Password)
				{
					response.Message = "Wrong Password!";
					return response;
				}


				database.UpdatedBy = req.UpdatedBy;
				database.Updated = DateTime.UtcNow;

				string key = Security.RandomString(60);

				database.PasswordSalt = key;
				database.Password = Security.CheckHMAC(key, req.Password);


				ctx.SaveChanges();

				response.Result = true;
				response.Message = "Password Changed.";
				return response;

			}
		}

	}
}

