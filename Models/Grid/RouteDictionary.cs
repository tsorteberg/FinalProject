/***************************************************************
* Name        : RouteDictionary.cs
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
    // static class of constants used to add and remove user-friendly
    // prefix from filter route segment values. Public class rather
    // than private constants bc also used by BookstoreGridBuilder class.

    public static class FilterPrefix
    {
        public const string Department = "department-";
        public const string Price = "price-";
        public const string Brand = "brand-";
    }

    // inherits dictionary of strings, adds a Clone() method. Adds properties
    // to get and set general paging, sorting, and filtering values from dictionary. 
    // Adds methods to set sort field value and sort direction value based on sort field, re-set filter values.

    public class RouteDictionary : Dictionary<string, string>
    {
        public int PageNumber
        {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize
        {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string SortField
        {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }

        public void SetSortAndDirection(string fieldName, RouteDictionary current)
        {
            this[nameof(GridDTO.SortField)] = fieldName;

            // set sort direction based on comparison of new and current sort field. if 
            // sort field is same as current, toggle between ascending and descending. 
            // if it's different, should always be ascending.

            if (current.SortField.EqualsNoCase(fieldName) &&
                current.SortDirection == "asc")
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
        }

        public string DepartmentFilter
        {
            get => Get(nameof(InstrumentsGridDTO.Department))?.Replace(FilterPrefix.Department, "");
            set => this[nameof(InstrumentsGridDTO.Department)] = value;
        }

        public string PriceFilter
        {
            get => Get(nameof(InstrumentsGridDTO.Price))?.Replace(FilterPrefix.Price, "");
            set => this[nameof(InstrumentsGridDTO.Price)] = value;
        }

        public string BrandFilter
        {
            get
            {
                // author filter contains prefix, author id, and slug (eg, author-8-ta-nehisi-coates).
                // only need author id for filtering, so first remove 'author-' prefix from string. At
                // that point, the authorid will be at beginning of string. So find index of dash after 
                // id number and then return substring from beginning of string to that index.
                string s = Get(nameof(InstrumentsGridDTO.Brand))?.Replace(FilterPrefix.Brand, "");
                int index = s?.IndexOf('-') ?? -1;
                return (index == -1) ? s : s.Substring(0, index);
            }
            set => this[nameof(InstrumentsGridDTO.Brand)] = value;
        }

        //public void ClearFilters() =>
        //    GenreFilter = PriceFilter = AuthorFilter = BooksGridDTO.DefaultFilter;
        public void ClearFilters()
        {
            DepartmentFilter = FilterPrefix.Department + InstrumentsGridDTO.DefaultFilter;
            PriceFilter = FilterPrefix.Price + InstrumentsGridDTO.DefaultFilter;
            BrandFilter = FilterPrefix.Brand + InstrumentsGridDTO.DefaultFilter;
        }

        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        // return a new dictionary that contains the same values as this dictionary.
        // needed so that pages can change the route values when calculating paging, sorting,
        // and filtering links, without changing the values of the current route

        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys)
            {
                clone.Add(key, this[key]);
            }
            return clone;
        }
    }
}
