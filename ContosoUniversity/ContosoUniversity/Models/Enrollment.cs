using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
	public enum Grade
	{
		A, B, C, D, F
	}
	public class Enrollment
	{
		public int EnrollmentID { get; set; }
		public Grade Grade { get; set; }
		[ForeignKey("Student")] //name of associated navigation property
		public int StudentID { get; set; }
		[ForeignKey("Course")] //name of associated navigation property
		public int CourseID { get; set; }
	//	[ForeignKey("StudentID")] //name of associated property that represents foreign key
		public virtual Student Student { get; set; }
	//	[ForeignKey("CourseID")] //name of associated property that represents foreign key
		public virtual Course Course { get; set; }
	}
}
