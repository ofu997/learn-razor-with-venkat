using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesWithVenkat.Models;
using RazorPagesWithVenkat.Services;

namespace RazorPagesWithVenkat.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;

        public DetailsModel(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public Employee Employee { get; private set; }

        public IActionResult OnGet(int id)
        {
            Employee = employeeRepository.GetEmployee(id); 

            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

    }
}