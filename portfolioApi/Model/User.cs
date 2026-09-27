using System.ComponentModel.DataAnnotations;

namespace portfolioApi.Model
{
    public class User
    {
        public int UserId { get; set; }

        //name validation
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public  string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } =  string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}