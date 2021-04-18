/***************************************************************
* Name        : SeedInstrumentBrands.cs
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
    internal class SeedInstrumentBrands : IEntityTypeConfiguration<InstrumentBrand>
    {
        public void Configure(EntityTypeBuilder<InstrumentBrand> entity)
        {
            entity.HasData(
                new InstrumentBrand { InstrumentId = 1, BrandId = 1 },
                new InstrumentBrand { InstrumentId = 2, BrandId = 2 },
                new InstrumentBrand { InstrumentId = 3, BrandId = 3 },
                new InstrumentBrand { InstrumentId = 4, BrandId = 4 },
                new InstrumentBrand { InstrumentId = 5, BrandId = 2 },
                new InstrumentBrand { InstrumentId = 6, BrandId = 5 },
                new InstrumentBrand { InstrumentId = 7, BrandId = 6 },
                new InstrumentBrand { InstrumentId = 8, BrandId = 7 },
                new InstrumentBrand { InstrumentId = 9, BrandId = 8 },
                new InstrumentBrand { InstrumentId = 10, BrandId = 6 }
            );
        }
    }
}
