namespace Absensi.Services.Base
{
    public class AdminCreate
    {
        public string? CreatedBy { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string? PasswordSalt { get; set; }
        public long RoleId { get; set; }
    }

    public class AdminList
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string Status { get; set; } = null!;
        public long RoleId { get; set; }
        public string? Role { get; set; }
    }

    public class AdminUpdate
    {
        public long Id { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Email { get; set; }
        public long RoleId { get; set; }
        public bool Status { get; set; }
    }
}
