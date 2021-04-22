/***************************************************************
* Name        : SeedBrands.cs
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
    internal class SeedBrands : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> entity)
        {
            entity.HasData(
                new Brand { BrandId = 1, BrandName = "Fender Stratocaster", ProductLine = "Guitars", LogoImage = "Fender.jpg" },
                new Brand { BrandId = 2, BrandName = "Yamaha", ProductLine = "Orchestra", LogoImage = "Yamaha.jpg" },
                new Brand { BrandId = 3, BrandName = "Ludwig", ProductLine = "Drum Sets", LogoImage = "Ludwig.jpg" },
                new Brand { BrandId = 4, BrandName = "Latin Percussion", ProductLine = "Hand Percussion", LogoImage = "LatinPercussion.jpg" },
                new Brand { BrandId = 5, BrandName = "Korg", ProductLine = "Synthesizers", LogoImage = "Korg.jpg" },
                new Brand { BrandId = 6, BrandName = "Jean Paul USA", ProductLine = "Band", LogoImage = "JeanPaul.jpg" },
                new Brand { BrandId = 7, BrandName = "Gemeinhardt", ProductLine = "Orchestra", LogoImage = "Gemeinhardt.jpg" },
                new Brand { BrandId = 8, BrandName = "Kaizer", ProductLine = "Band", LogoImage = "Kaizer.jpg" }
            );
        }
    }

}