using System.ComponentModel.DataAnnotations;

namespace portfolioApi.Model
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "DepartmentName is required.")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "DepartmentName must be between 2 and 100 characters.")]
        public string DepartmentName { get; set; } =string.Empty;
        public string DepartmentCode { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}