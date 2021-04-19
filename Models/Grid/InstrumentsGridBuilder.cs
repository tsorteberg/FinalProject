/***************************************************************
* Name        : InstrumentsGridBuilder.cs
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
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    // inherits the general purpose GridBuilder class and adds application-specific 
    // methods for loading and clearing filter route segments in route dictionary.
    // Also adds application-specific Boolean flags for sorting and filtering. 

    public class InstrumentsGridBuilder : GridBuilder
    {
        // this constructor gets current route data from session
        public InstrumentsGridBuilder(ISession sess) : base(sess) { }

        // this constructor stores filtering route segments, as well as
        // the paging and sorting route segments stored by the base constructor
        public InstrumentsGridBuilder(ISession sess, InstrumentsGridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            // store filter route segments - add filter prefixes if this is initial load
            // of page with default values rather than route values (route values have prefix)
            bool isInitial = values.Department.IndexOf(FilterPrefix.Department) == -1;
            routes.BrandFilter = (isInitial) ? FilterPrefix.Brand + values.Brand : values.Brand;
            routes.DepartmentFilter = (isInitial) ? FilterPrefix.Department + values.Department : values.Department;
            routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
        }

        // load new filter route segments contained in a string array - add filter prefix 
        // to each one. if filtering by author (rather than just 'all'), add author slug 
        public void LoadFilterSegments(string[] filter, Brand brand)
        {
            if (brand == null)
            {
                routes.BrandFilter = FilterPrefix.Brand + filter[0];
            }
            else
            {
                routes.BrandFilter = FilterPrefix.Brand + filter[0]
                    + "-" + brand.BrandName.Slug();
            }
            routes.DepartmentFilter = FilterPrefix.Department + filter[1];
            routes.PriceFilter = FilterPrefix.Price + filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

        //~~ filter flags ~~//
        string def = InstrumentsGridDTO.DefaultFilter;   // get default filter value from static DTO property
        public bool IsFilterByBrand => routes.BrandFilter != def;
        public bool IsFilterByDepartment => routes.DepartmentFilter != def;
        public bool IsFilterByPrice => routes.PriceFilter != def;

        //~~ sort flags ~~//
        public bool IsSortByDepartment =>
            routes.SortField.EqualsNoCase(nameof(Department));
        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Instrument.Price));
    }
}
