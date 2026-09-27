using System.ComponentModel.DataAnnotations;

namespace portfolioApi.Model
{
    public class Subject
    {
        public int SubjectId { get; set; }

        public string Faculty { get; set; } = string.Empty;

        public string CourseTitle{ get; set; }=string.Empty;

        public string Code { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Credits must be between 1 and 10.")] 
        public int credit{ get; set; }

        public int ModifyBy{ get; set; }

        public DateTime? ModifyOn{ get; set; }
    }
}