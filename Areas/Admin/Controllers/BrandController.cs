/***************************************************************
* Name        : Admin/Controllers/BrandController.cs
* Author      : Tom Sorteberg
* Created     : 04/25/2021
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
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private Repository<Brand> data { get; set; }
        public BrandController(FinalContext ctx) => data = new Repository<Brand>(ctx);

        public ViewResult Index()
        {
            var brands = data.List(new QueryOptions<Brand>
            {
                OrderBy = b => b.BrandName
            });
            return View(brands);
        }

        // select (posted from author drop down on Index page). 
        [HttpPost]
        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "view instruments":
                    return RedirectToAction("ViewInstruments", new { id });
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        // add
        [HttpGet]
        public ViewResult Add() => View("Brand", new Brand());

        [HttpPost]
        public IActionResult Add(Brand brand, string operation)
        {
            // server-side version of remote validation 
            var validate = new Validate(TempData);
            if (!validate.IsBrandChecked)
            {
                validate.CheckBrand(brand.BrandName, brand.ProductLine, operation, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(brand.BrandName), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(brand);
                data.Save();
                validate.ClearBrand();
                TempData["message"] = $"{brand.BrandName} added to Brands.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Author", brand);
            }
        }

        // edit
        [HttpGet]
        public ViewResult Edit(int id) => View("Brand", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            // no remote validation of author on edit
            if (ModelState.IsValid)
            {
                data.Update(brand);
                data.Save();
                TempData["message"] = $"{brand.BrandName} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Brand", brand);
            }
        }

        // delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var brand = data.Get(new QueryOptions<Brand>
            {
                Includes = "InstrumentBrands",
                Where = b => b.BrandId == id
            });

            if (brand.InstrumentBrands.Count > 0)
            {
                TempData["message"] = $"Can't delete brand {brand.BrandName} because they are associated with these instruments.";
                return GoToBrandSearch(brand);
            }
            else
            {
                return View("Brand", brand);
            }
        }

        [HttpPost]
        public RedirectToActionResult Delete(Brand brand)
        {
            // no ModelState.IsValid check here bc there's no user input - 
            // only AuthorId in hidden field is posted from form. 
            data.Delete(brand);
            data.Save();
            TempData["message"] = $"{brand.BrandName} removed from Brands.";
            return RedirectToAction("Index");  // PRG pattern
        }

        // view books by author
        public RedirectToActionResult ViewInstruments(int id)
        {
            var brand = data.Get(id);
            return GoToBrandSearch(brand);
        }

        // private helper method
        private RedirectToActionResult GoToBrandSearch(Brand brand)
        {
            // store author search data in TempData and redirect
            var search = new SearchData(TempData)
            {
                SearchTerm = brand.BrandName,
                Type = "brand"
            };
            return RedirectToAction("Search", "Instrument");
        }
    }
}