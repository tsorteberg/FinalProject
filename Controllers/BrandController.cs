/***************************************************************
* Name        : BrandController.cs
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
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class BrandController: Controller
    {
        private Repository<Brand> data { get; set; }
        public BrandController(FinalContext ctx) => data = new Repository<Brand>(ctx);

        public IActionResult Index() => RedirectToAction("List");

        // dto has properties for the paging and sorting route segments defined in the Startup.cs file
        public ViewResult List(GridDTO vals)
        {
            // get grid builder, which loads route segment values and stores them in session
            string defaultSort = nameof(Brand.BrandName);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            // create options for querying authors. OrderBy depends on value in SortField route 
            var options = new QueryOptions<Brand>
            {
                Includes = "InstrumentBrands.Instrument",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = a => a.BrandName;
            else
                options.OrderBy = a => a.ProductLine;

            // create view model and add page of author data, the current route,
            // and the total number of pages
            var vm = new BrandListViewModel
            {
                Brands = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            // pass view model to view
            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var author = data.Get(new QueryOptions<Brand>
            {
                Includes = "InstrumentBrands.Instrument",
                Where = a => a.BrandId == id
            });
            return View(author);
        }
    }
}
