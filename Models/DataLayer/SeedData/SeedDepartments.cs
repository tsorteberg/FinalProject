/***************************************************************
* Name        : SeedDepartments.cs
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    internal class SeedDepartments : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.HasData(
                new Department { DepartmentId = "strings", Name = "Strings" },
                new Department { DepartmentId = "percussion", Name = "Percussion" },
                new Department { DepartmentId = "keys", Name = "Keys" },
                new Department { DepartmentId = "brass", Name = "Brass" },
                new Department { DepartmentId = "woodwinds", Name = "Woodwinds" },
                new Department { DepartmentId = "gear", Name = "Gear" }
            );
        }
    }
}
