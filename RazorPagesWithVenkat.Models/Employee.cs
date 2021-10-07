using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RazorPagesWithVenkat.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "需要名稱")]
        [MinLength(3, ErrorMessage = "Name should contain at least 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "se necesita email")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public Dept? Department { get; set; }
    }
}
