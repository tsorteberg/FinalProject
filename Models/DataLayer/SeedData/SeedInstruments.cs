/***************************************************************
* Name        : SeedInstruments.cs
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
    internal class SeedInstruments : IEntityTypeConfiguration<Instrument>
    {
        public void Configure(EntityTypeBuilder<Instrument> entity)
        {
            entity.HasData(
                new Instrument { InstrumentId = 1, Name = "Squier by StratoCaster", DepartmentId = "strings", Price = 219.99, LogoImage="Squire.jpg" },
                new Instrument { InstrumentId = 2, Name = "V3 Student Violin", DepartmentId = "strings", Price = 799.99, LogoImage="V3.jpg" },
                new Instrument { InstrumentId = 3, Name = "Questlove Pocket Kit", DepartmentId = "percussion", Price = 269.99, LogoImage="QuestLove.jpg" },
                new Instrument { InstrumentId = 4, Name = "City Series Conga", DepartmentId = "percussion", Price = 349.99, LogoImage="CitySeries.jpg" },
                new Instrument { InstrumentId = 5, Name = "P-45 88-Key Digital Piano", DepartmentId = "keys", Price = 499.99, LogoImage="P-45.jpg" },
                new Instrument { InstrumentId = 6, Name = "MicroKorg Synthesizer", DepartmentId = "keys", Price = 399.99, LogoImage="MicroKorg.jpg" },
                new Instrument { InstrumentId = 7, Name = "AS-400 Alto Saxophone", DepartmentId = "brass", Price = 499.99, LogoImage="AS-400.jpg" },
                new Instrument { InstrumentId = 8, Name = "2SP Student Flute", DepartmentId = "woodwinds", Price = 479.99, LogoImage="2SP.jpg" },
                new Instrument { InstrumentId = 9, Name = "C-Series (3000) Trumpet", DepartmentId = "brass", Price = 299.99, LogoImage = "C-Series-3000.jpg" },
                new Instrument { InstrumentId = 10, Name = "CL-300 Student Clarinet", DepartmentId = "woodwinds", Price = 199.99, LogoImage = "CL-300.jpg" }
            );
        }
    }
}
