﻿using System;
using System.Collections.Generic;
using RazorPagesWithVenkat.Models;

namespace RazorPagesWithVenkat.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        Employee Update(Employee updatedEmployee);
        Employee Add(Employee newEmployee);
    }
}
