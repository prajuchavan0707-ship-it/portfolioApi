using System.ComponentModel.DataAnnotations;

namespace portfolioApi.Model
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        [Required(ErrorMessage = "StudentID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "StudentID must be greater than 0.")]
        public string StudentPrn { get; set; } = string.Empty;

        [Required(ErrorMessage = "SubjectID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "SubjectID must be greater than 0.")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Attendance date is required.")]
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int ModifyBy { get; set; }
        public DateTime? ModifyOn { get; set; }
    }
}