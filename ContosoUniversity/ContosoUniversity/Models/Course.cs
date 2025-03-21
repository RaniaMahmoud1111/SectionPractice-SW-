﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
	public class Course
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)] //to disable auto generation and auto increment for its value
		public int CourseID { get; set; }
		public string Title { get; set; }
		public int Credits { get; set; }
		public ICollection<Enrollment> Enrollments { get; set; }
	}
}
