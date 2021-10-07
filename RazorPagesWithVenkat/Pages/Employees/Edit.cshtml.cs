using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesWithVenkat.Models;
using RazorPagesWithVenkat.Services;

namespace RazorPagesWithVenkat.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.employeeRepository = employeeRepository;
            // IWebHostEnvironment service allows us to get the
            // absolute path of WWWRoot folder
            this.webHostEnvironment = webHostEnvironment;
        }
        public Employee Employee { get; set; }

        // We use this property to store and process
        // the newly uploaded photo
        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }

        public string Message { get; set; }
        public IActionResult OnGet(int id)
        {
            Employee = employeeRepository.GetEmployee(id);

            if (Employee == null)
            {
                return RedirectToPage("/NotFound"); 
            }

            return Page();
        }

        public IActionResult OnPost(Employee employee)
        {
            if (Photo != null)
            {
                // If a new photo is uploaded, the existing photo must be
                // deleted. So check if there is an existing photo and delete
                if (employee.PhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", employee.PhotoPath);
                    System.IO.File.Delete(filePath);
                }
                // Save the new photo in wwwroot/images folder and update
                // PhotoPath property of the employee object
                employee.PhotoPath = ProcessUploadedFile();
            }
            Employee = employeeRepository.Update(employee);
            return RedirectToPage("Index");
        }

        public void OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "Thank you for turning on notifications";
            }
            else
            {
                Message = "You have turned off email notifications";
            }

            Employee = employeeRepository.GetEmployee(id);
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
