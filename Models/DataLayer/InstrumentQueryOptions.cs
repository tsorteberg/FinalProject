/***************************************************************
* Name        : InstrumentsQueryOptions.cs
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
    // extends generic QueryOptions<Book> class to add a 
    // SortFilter() method that adds the Sort and Filter
    // expression specific to the Bookstore application

    public class InstrumentQueryOptions : QueryOptions<Instrument>
    {
        public void SortFilter(InstrumentsGridBuilder builder)
        {
            // filter
            if (builder.IsFilterByDepartment)
            {
                Where = d => d.DepartmentId == builder.CurrentRoute.DepartmentFilter;
            }
            if (builder.IsFilterByPrice)
            {
                if (builder.CurrentRoute.PriceFilter == "under100")
                    Where = p => p.Price < 100;
                else if (builder.CurrentRoute.PriceFilter == "100to500")
                    Where = p => p.Price >= 100 && p.Price <= 500;
                else
                    Where = p => p.Price > 500;
            }
            if (builder.IsFilterByBrand)
            {
                int id = builder.CurrentRoute.BrandFilter.ToInt();
                if (id > 0)
                    // to filter the books by author, use the LINQ Any() method. 
                    Where = p => p.InstrumentBrands.Any(ib => ib.Brand.BrandId == id);
            }

            // sort 
            if (builder.IsSortByDepartment)
            {
                OrderBy = d => d.Department.Name;
            }
            else if (builder.IsSortByPrice)
            {
                OrderBy = d => d.Price;
            }
            else
            {
                OrderBy = d => d.Name;
            }
        }
    }
}
