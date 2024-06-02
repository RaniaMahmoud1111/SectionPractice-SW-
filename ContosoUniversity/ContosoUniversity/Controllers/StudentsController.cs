using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ContosoUniversity.Controllers
{
	[Authorize]
	public class StudentsController : Controller
	{
		private readonly SchoolContext _context;

		 public StudentsController(SchoolContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{

				return View(_context.Students.ToList());
			
		}
		  
		public IActionResult InsertByURL(string fname, string lname)
		{
			Student student = new Student
			{
				FirstMidName = fname,
				LastName = lname,
				EnrollmentDate = DateTime.Now,
			};
			_context.Students.Add(student);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		 
		public	IActionResult SearchByURL(int id)
		{
			Student st = _context.Students.FirstOrDefault(s => s.ID == id);
			if(st != null)
			{ 
				return Content(st.ToString());
			}
			else
				return RedirectToAction("Index");
		}


		public IActionResult DeleteByURL(int id)
		{
			Student st = _context.Students.FirstOrDefault(s => s.ID == id);
				
				if(st!=null)//means find st who ID=id
			{
				_context.Students.Remove(st);
				_context.SaveChanges();

			}

			return RedirectToAction("Index");

		}

		public IActionResult UpdateByURL(int id, string fname, string lname)
		{
			Student st = _context.Students.FirstOrDefault(s => s.ID == id);

			if(st!=null)
			{
				st.FirstMidName = fname;
				st.LastName = lname;
				_context.Students.Update(st);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		/// Actions without URL 

		[HttpGet]
		public IActionResult Insert()
		{
			return View();
		}


		[HttpPost]
		public IActionResult Insert(Student student)
		{
			ModelState.Remove("Enrollments");
			if(ModelState.IsValid)
			{
				_context.Students.Add(student);
				_context.SaveChanges();
				return RedirectToAction("Index");

			}
			return View(student);
		}



		[HttpGet]
        //public IActionResult Details(int id)
        //{
        //	Student student = _context.Students.Where(S => S.ID == id).
        //	 Include(s => s.Enrollments).
        //	 ThenInclude(c => c.Course).
        //	 AsNoTracking().
        //	 FirstOrDefault();
        //	return View(student);
        //}

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpGet]
		public IActionResult Edit(int id)
		{
			Student student = _context.Students.Find(id);
			return View(student);
		}
		[HttpPost]
		public IActionResult Edit(Student student)
		{
			ModelState.Remove("Enrollments");
			if (ModelState.IsValid)
			{
				_context.Students.Update(student);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(student);
		}


		public IActionResult Delete(int id)
		{
			Student st = _context.Students.Find(id);
			if (st != null)
			{
				_context.Students.Remove(st);
				_context.SaveChanges();
			}
			return RedirectToAction("Index");
		}



	}

}
