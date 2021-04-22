/***************************************************************
* Name        : InstrumentController.cs
* Author      : Tom Sorteberg
* Created     : 04/21/2021
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
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class InstrumentController : Controller
    {
        private FinalUnitOfWork data { get; set; }
        public InstrumentController(FinalContext ctx) => data = new FinalUnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("List");

        // dto has properties for the paging, sorting, and filtering route segments defined in the Startup.cs file
        public ViewResult List(InstrumentsGridDTO values)
        {
            // get grid builder, which loads route segment values and stores them in session
            var builder = new InstrumentsGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Instrument.Name));

            // create a InstrumentQueryOptions object to build a query expression for a page of data
            var options = new InstrumentQueryOptions
            {
                Includes = "InstrumentBrands.Brand, Department",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            // call the SortFilter() method of the BookQueryOptions object and pass it the builder
            // object. It uses the route information and the properties of the builder object to 
            // add sort and filter options to the query expression. 
            options.SortFilter(builder);

            // create view model and add page of book data, data for drop-downs, 
            // the current route, and the total number of pages. 
            var vm = new InstrumentListViewModel
            {
                Instruments = data.Instruments.List(options),
                Brands = data.Brands.List(new QueryOptions<Brand>
                {
                    OrderBy = b => b.BrandName
                }),
                Departments = data.Departments.List(new QueryOptions<Department>
                {
                    OrderBy = d => d.Name
                }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Instruments.Count)
            };

            // pass view model to view
            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var instrument = data.Instruments.Get(new QueryOptions<Instrument>
            {
                Includes = "InstrumentBrands.Brand, Department",
                Where = i => i.InstrumentId == id
            });
            return View(instrument);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            // get current route segments from session
            var builder = new InstrumentsGridBuilder(HttpContext.Session);

            // clear or update filter route segment values. If update, get author data
            // from database so can add author name slug to author filter value.
            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                var brand = data.Brands.Get(filter[0].ToInt());
                builder.CurrentRoute.PageNumber = 1;
                builder.LoadFilterSegments(filter, brand);
            }

            // save route data back to session and redirect to Book/List action method,
            // passing dictionary of route segment values to build URL
            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }
}
