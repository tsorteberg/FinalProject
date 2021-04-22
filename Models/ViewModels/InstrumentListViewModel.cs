/***************************************************************
* Name        : InstrumentListViewModel.cs
* Author      : Tom Sorteberg
* Created     : 04/19/2021
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
    public class InstrumentListViewModel
    {
        public IEnumerable<Instrument> Instruments { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        // data for filter drop-downs - one hardcoded
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public Dictionary<string, string> Prices =>
            new Dictionary<string, string> {
                { "under100", "Under $100" },
                { "100to500", "$100 to $500" },
                { "over500", "Over $500" }
            };
    }
}
