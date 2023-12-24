using System.ComponentModel.DataAnnotations;

namespace LoginUser.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName cannot be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile_No is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid Mobile_No format")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [RegularExpression(@"^(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain 1 special character, 1 upper case letter, and length should be at least 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "User Type cannot be empty")]
        public string UserType { get; set; }
    }

}

