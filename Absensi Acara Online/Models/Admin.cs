using System.ComponentModel.DataAnnotations;

namespace Absensi.Models
{
    public class AdminCreateVM
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string PasswordSalt { get; set; } = null!;
        public string? Email { get; set; }
        [Required]
        public int RoleId { get; set; }
    }

    public class AdminUpdateVM
    {
        [Required]
        public long Id { get; set; }
        public string? Email { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Status { get; set; } = null!;
    }

    public class AdminSetPassVM
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string PasswordSalt { get; set; } = null!;
        public string? Email { get; set; }
        [Required]
        public int RoleId { get; set; }
    }

}
