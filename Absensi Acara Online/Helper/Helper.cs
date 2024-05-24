using System.Security.Claims;
using Absensi.Services.Base;

namespace Absensi.Helper
{
	public static class Helper
	{
		public static string? GetUserId(this ClaimsPrincipal principal)
		{
			if (principal == null)
				throw new ArgumentNullException(nameof(principal));

			return principal.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		//User Id
		public static long GetTheId(this ClaimsPrincipal principal)
		{
			long id = 0;
			if (principal != null)
			{
				id = Convert.ToInt64(principal.FindFirstValue("Id"));
			}
			return id;
		}
		//Username
		public static string? GetTheUsername(this ClaimsPrincipal principal)
		{
			string? username = "";
			if (principal != null)
			{
				username = principal.FindFirstValue("Username");
			}
			return username;
		}

		public static string? GetThePassword(this ClaimsPrincipal principal)
		{
			string? Password = "";
			if (principal != null)
			{
				Password = principal.FindFirstValue("Password");
			}
			return Password;
		}

		public static string? GetCustomEmail(this ClaimsPrincipal principal)
		{
			string? email = "";
			if (principal != null)
			{
				email = principal.FindFirstValue(ClaimTypes.Email);
			}
			return email;
		}

        public static long? GetTheRoleId(this ClaimsPrincipal principal)
        {
            long? roleId = 0;
            if (principal != null)
            {
				roleId = Convert.ToInt64(principal.FindFirstValue("RoleId"));
            }
            return roleId;
        }
        public static string? GetTheRole(this ClaimsPrincipal principal)
		{

            string? role = "";
            if (principal != null)
            {
                role = principal.FindFirstValue("Role");
            }
            return role;
        }
	}
}
