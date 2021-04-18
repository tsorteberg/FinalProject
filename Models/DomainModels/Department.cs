/***************************************************************
* Name        : Department.cs
* Author      : Tom Sorteberg
* Created     : 04/18/2021
* Course      : CIS 174
* Version     : 1.0
* OS          : Windows 10 Pro, Visual Studio Community 2019
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : Final Project.
* I have not used unauthorized source code, either modified or 
* unmodified. I have not given other fellow student(s) access 
* to my program.         
***************************************************************/
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Department
    {
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter a department id.")]
        [Remote("CheckDepartment", "Validation", "Admin")] // include 'Admin' area name
        public string DepartmentId { get; set; }

        [StringLength(25)]
        [Required(ErrorMessage = "Please enter a department name.")]
        public string Name { get; set; }

        // navigation property
        public ICollection<Instrument> Instruments { get; set; }
    }
}
