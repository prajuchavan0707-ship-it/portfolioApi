using System.ComponentModel.DataAnnotations;

namespace portfolioApi.Model
{
    public class Student
    {
        public int Prn { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public String FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public String LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required.")]
        public String Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public String Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string ContactNo { get; set; } = string.Empty;

        public int  CreatedBy{ get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }
    }
}