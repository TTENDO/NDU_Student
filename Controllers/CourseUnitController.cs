using Microsoft.AspNetCore.Mvc;
using NDU_Student.Models;
using System.Diagnostics;
using NDU_Student.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace NDU_Student.Controllers
{
    public class CourseUnitController : Controller
    {
        private readonly ILogger<CourseUnitController> _logger;
        private readonly StudentDbContext _context;

        public CourseUnitController(StudentDbContext context, ILogger<CourseUnitController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(CourseUnitViewModel model)
        {
            model.LoadCourseUnits(_context);
            return View(model);
        }


        public IActionResult Create(int Id = 0)
        {
            var model = new CourseUnit
            {
                Courses = _context.Courses.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseUnit courseUnit)
        {
            try
            {
                if (courseUnit.CourseId < 1)
                    throw new Exception("Please provide the course!");
                if (string.IsNullOrEmpty(courseUnit.CourseUnitCode))
                    throw new Exception("Please provide the course unit code!");
                if (string.IsNullOrEmpty(courseUnit.Name))
                    throw new Exception("Please provide the course unit name!");
                if (string.IsNullOrEmpty(courseUnit.DoneInYear))
                    throw new Exception("Please provide the year when this course unit is done!");

                _context.Add(courseUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { SuccessMessage = "Successfully created a Course Unit!" }); 

            }
            catch (Exception ex)
            {
                courseUnit.Courses = _context.Courses.ToList();
                courseUnit.ErrorMessage = ex.GetBaseException().Message;
            }
            return View(courseUnit);
        }

        public async Task<IActionResult> Details(int id)
        {
            var courseUnit = await _context.CourseUnits.FindAsync(id);
            if (courseUnit == null)
            {
                return NotFound();
            }

            courseUnit.Courses = _context.Courses.ToList();
            
            return View(courseUnit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, CourseUnit courseUnit)
        {
            if (id != courseUnit.Id)
            {
                return NotFound();
            }

            // Skip ModelState validation

            _context.Update(courseUnit);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { SuccessMessage = "Successfully updated the course unit!" });
        }


        public JsonResult _Delete(int Id)
        {
            string errorMessage;
            bool success = false;


            // Retrieve the entity you want to delete
            var courseUnitDb = _context.CourseUnits.FirstOrDefault(e => e.Id == Id);

            if (courseUnitDb != null)
            {
                // Remove the entity from the context
                _context.CourseUnits.Remove(courseUnitDb);

                // Save changes to the database
                _context.SaveChanges();

                errorMessage = "Course Unit deleted successfully.";
                success = true;
            }
            else
            {
                errorMessage = "Course Unit not found.";
                success = false;
            }


            return Json(new
            {
                success,
                errorMessage
            });
        }

    }
}
