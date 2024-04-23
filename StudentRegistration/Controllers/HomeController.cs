using Microsoft.AspNetCore.Authorization;
using StudentRegistration.Models;
using StudentRegistration.Repositories;
using StudentRegistration.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace StudentRegistration.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IStudentRepository _studentRepository;

        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var student = await _studentRepository.GetAllAsync();
            return View(student);
        }

        //GET: Student/Add
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        //POST: Student/Add
        [HttpPost]
        public async Task<IActionResult> Add(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return to the form with validation errors
            }

            // Save the students to the database
            await _studentRepository.AddAsync(model);

            return RedirectToAction("Index", "Home");

        }

        //GET: Student/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //Fetch the student details
            var student = await _studentRepository.GetByIdAsync(id);
            return View(student);
        }

        //POST: Student/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel student)
        {

            if (!ModelState.IsValid)
            {
                return View(student); // Return to the form with validation errors
            }
            //Update the database with modified details
            await _studentRepository.UpdateAsync(student);

            // Redirect to List all department page
            return RedirectToAction("Index", "Home");
        }

        //GET: /Student/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //Delete the data from database
            await _studentRepository.DeleteAsync(id);
            // Redirect to List all department page
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    
    }
}
