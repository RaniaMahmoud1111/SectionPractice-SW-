using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
	public class Student
	{
		[Key]
		public int ID { get; set; }
		public string LastName { get; set; }

	//	[Column("FirstName", Order = 2,TypeName = "nvarchar")]
	    [Display(Name= "FirstName")]
		public string FirstMidName { get; set; }
		public DateTime EnrollmentDate { get; set; }
		public ICollection<Enrollment> Enrollments { get; set; }

		public override string ToString()
		{
			//string interpolation
			string stat =$"ID={ID}\n"+
				$"LastName={LastName}\n" +
				$"FirstName={FirstMidName}\n" +
				$"EnrollmentDate={EnrollmentDate}";

			return stat;
		}
		public  string ToString2()
		{
			string er = "Not Found";

			return er;
		}
	}

}
