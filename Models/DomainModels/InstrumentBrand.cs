/***************************************************************
* Name        : InstrumentBrand.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class InstrumentBrand
    {
        // composite primary key and foreign keys
        public int InstrumentId { get; set; }
        public int BrandId { get; set; }

        // navigation properties
        public Brand Brand { get; set; }
        public Instrument Instrument { get; set; }
    }
}
