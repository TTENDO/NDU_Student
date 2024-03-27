using Microsoft.AspNetCore.Mvc;
using NDU_Student.Models;
using System.Diagnostics;
using NDU_Student.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace NDU_Student.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private readonly StudentDbContext _context;

        public CourseController(StudentDbContext context, ILogger<CourseController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index(CourseViewModel model)
        {
            model.LoadCourses(_context);
            return View(model);
        }


        public IActionResult Create(int Id = 0)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            try
            {
                if (string.IsNullOrEmpty(course.CourseCode))
                    throw new Exception("Please provide the course code!");
                if (string.IsNullOrEmpty(course.Name))
                    throw new Exception("Please provide the course name!");

                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { SuccessMessage = "Successfully created a Course!" }); // Redirect to another action (e.g., Index)

            }
            catch (Exception ex)
            {
                course.ErrorMessage = ex.GetBaseException().Message;
            }
            return View(course);

        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            // Skip ModelState validation

            _context.Update(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { SuccessMessage = "Successfully updated the course!" });
        }


        public JsonResult _Delete(int Id)
        {
            string errorMessage;
            bool success = false;


            // Retrieve the entity you want to delete
            var courseDb = _context.Courses.FirstOrDefault(e => e.Id == Id);

            if (courseDb != null)
            {
                // Remove the entity from the context
                _context.Courses.Remove(courseDb);

                // Save changes to the database
                _context.SaveChanges();

                errorMessage = "Course deleted successfully.";
                success = true;
            }
            else
            {
                errorMessage = "Course not found.";
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
