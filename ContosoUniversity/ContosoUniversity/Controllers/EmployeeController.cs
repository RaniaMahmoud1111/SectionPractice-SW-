using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;

public class EmployeeController : Controller
{
	private readonly SchoolContext _context;
	private readonly IWebHostEnvironment _environment;
	public EmployeeController(SchoolContext context, IWebHostEnvironment environment)
	{
		_context = context;
		_environment = environment;
	}
    //HttpContext.Session.SetString("User", user.Email);
   // HttpContext.Session.SetString("img", user.ImageUrl);

		//ViewBag.User = HttpContext.Session.GetString("User");
       //ViewBag.img = HttpContext.Session.GetString("img");

	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}
	[HttpPost]
	public IActionResult Register(Employee EMP, IFormFile ImageUrl)
	{
		if (ImageUrl != null)
		{
			string imgFolderPath = Path.Combine(_environment.WebRootPath, "img");
			if (!Directory.Exists(imgFolderPath))
			{
				Directory.CreateDirectory(imgFolderPath);
			}
			string imgPath = Path.Combine(imgFolderPath,
			ImageUrl.FileName);
			EMP.ImageUrl = ImageUrl.FileName;
			using (var stream = new FileStream(imgPath,
		   FileMode.Create))
			{
				ImageUrl.CopyTo(stream);
			}
		}
		else
		{
			EMP.ImageUrl = "Default.png";
		}
		ModelState.Remove("ImageUrl");
		if (ModelState.IsValid)
		{
			_context.Employees.Add(EMP);
			_context.SaveChanges();
			return RedirectToAction("Index", "Home");
		}
		return View(EMP);
	}
	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}
	[HttpPost]
	public IActionResult Login(Employee EMP)
	{
		ModelState.Remove("FirstName");
		ModelState.Remove("LastName");
		ModelState.Remove("ConfirmPassword");
		ModelState.Remove("ImageUrl");
		if (ModelState.IsValid)
		{
			var user = _context.Employees.FirstOrDefault(u => u.Email == EMP.Email);
			if (user != null && user.Password == EMP.Password)
			{
				HttpContext.Session.SetString("User", user.Email);
				HttpContext.Session.SetString("img", user.ImageUrl);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.error = "your Email or Password is Invalid";
			 }
		}
		else
		{
			ViewBag.error = "your Email or Password is Invalid";
		}
		return View(EMP);
	}












}
