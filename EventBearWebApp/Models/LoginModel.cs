using System.ComponentModel.DataAnnotations;

namespace EventBearWebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "กรุณากรอก Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "กรุณากรอก Email ให้ถูกต้อง ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "กรุณากรอก รหัสผ่าน")]
        public string Password { get; set; }
    }
}