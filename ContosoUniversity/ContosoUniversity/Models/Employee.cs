using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
	[PrimaryKey("Email")]
	public class Employee
	{
		[Required(ErrorMessage = "You Should Enter a vaild Email"),
		EmailAddress(ErrorMessage = "You Should Enter a vaild Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "You Should Enter Your First Name"),
		Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "You Should Enter Your Last Name"),
		Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "You Should Enter Your Password"),
		DataType(DataType.Password),
		MinLength(3, ErrorMessage = "Password should be longer than 3 chars")]
	    public string Password { get; set; }
		[Required, Compare("Password", ErrorMessage = "your password doesn’t match!")]
	    public string ConfirmPassword { get; set; }
		[Display(Name ="Image")]
	    public string ImageUrl { get; set; }
	}

}
