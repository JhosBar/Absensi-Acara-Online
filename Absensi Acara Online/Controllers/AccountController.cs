using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Absensi.Services.Base;
using Absensi.Services.Interface;
using Absensi.Services;
using Absensi.Models;
using Absensi.Helper;

namespace Absensi_Acara_Online.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private IAccountService AccountService = new AccountService();
		private BaseResponse<bool> response = new BaseResponse<bool>();

		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginVM user)
		{
			if (!ModelState.IsValid)
			{
				response.Message = "Please fill in all fields.";
				return Json(response);
			}

			var check = AccountService.Login(new AccountLogin()
			{
				Username = user.Username,
				Password = user.Password,
			});

			if (check.Result == null)
			{
				response.Message = check.Message;
				return Json(response);
			}

			var claims = new List<Claim>
			{
				new Claim("Username", check.Result.Username),
				new Claim("RoleId", check.Result.RoleId.ToString()),
				new Claim("Role", check.Result.Role),
				new Claim("Id", check.Result.Id.ToString()),
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authProperties = new AuthenticationProperties { };
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

			response.Result = true;
			response.Message = "Success";
			return Json(response);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult ChangePassword(AccountPassVM data)
		{
			if (ModelState.IsValid)
			{
				var get = AccountService.ChangePass(new AccountPass()
				{
					Id = User.GetTheId(),
					Current_Password = data.Current_Password,
					Password = data.Password,
					UpdatedBy = User.GetTheUsername(),
				});
				return Json(get);

			}
			response.Message = "Nothing Change";
			return Json(response);
		}

		public IActionResult Notfound()
		{
			return View();
		}
	}
}
