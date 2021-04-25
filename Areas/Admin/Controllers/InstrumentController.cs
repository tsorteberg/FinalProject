/***************************************************************
* Name        : Admin/Controllers/InstrumentController.cs
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
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstrumentController : Controller
    {
        private FinalUnitOfWork data { get; set; }
        public InstrumentController(FinalContext ctx) => data = new FinalUnitOfWork(ctx);

        public ViewResult Index()
        {
            // clear any previous searches
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

        // search (posted from search text box on Index page). Search term is required field.
        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // store search data in TempData and redirect
                new SearchData(TempData)
                {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search"); // PRG pattern
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Search()
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm)
            {
                var vm = new SearchViewModel
                {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Instrument>
                {
                    Includes = "Department, InstrumentBrands.Brand"
                };
                if (search.IsInstrument)
                {
                    options.Where = i => i.Name.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for instrument name '{vm.SearchTerm}'";
                }
                if (search.IsBrand)
                {
                    // if there's no space, search both first and last name by search term. 
                    // Otherwise, assume there's a first and last name and refine search.
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1)
                    { // no space
                        options.Where = ib => ib.InstrumentBrands.Any(
                            ib => ib.Brand.BrandName.Contains(vm.SearchTerm) || // OR
                            ib.Brand.ProductLine.Contains(vm.SearchTerm));
                    }
                    else
                    {  // assume first and last name
                        string brandName = vm.SearchTerm.Substring(0, index);
                        string productLine = vm.SearchTerm.Substring(index + 1); // skip space
                        options.Where = ib => ib.InstrumentBrands.Any(
                            ib => ib.Brand.BrandName.Contains(brandName) &&  // AND
                            ib.Brand.ProductLine.Contains(productLine));
                    }
                    vm.Header = $"Search results for brand '{vm.SearchTerm}'";
                }
                if (search.IsDepartment)
                {
                    options.Where = d => d.DepartmentId.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for department ID '{vm.SearchTerm}'";
                }
                vm.Instruments = data.Instruments.List(options);
                return View("SearchResults", vm);
            }
            else
            {
                return View("Index");
            }
        }

        // add
        [HttpGet]
        public ViewResult Add(int id) => GetInstrument(id, "Add");

        [HttpPost]
        public IActionResult Add(InstrumentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.AddNewInstrumentBrands(vm.Instrument, vm.SelectedBrands);
                data.Instruments.Insert(vm.Instrument);
                data.Save();

                TempData["message"] = $"{vm.Instrument.Name} added to Instruments.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                Load(vm, "Add");
                return View("Instrument", vm);
            }
        }

        // edit
        [HttpGet]
        public ViewResult Edit(int id) => GetInstrument(id, "Edit");

        [HttpPost]
        public IActionResult Edit(InstrumentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.DeleteCurrentInstrumentBrands(vm.Instrument);
                data.AddNewInstrumentBrands(vm.Instrument, vm.SelectedBrands);
                data.Instruments.Update(vm.Instrument);
                data.Save();

                TempData["message"] = $"{vm.Instrument.Name} updated.";
                return RedirectToAction("Search");  // PRG pattern
            }
            else
            {
                Load(vm, "Edit");
                return View("Instrument", vm);
            }
        }

        // delete
        [HttpGet]
        public ViewResult Delete(int id) => GetInstrument(id, "Delete");

        [HttpPost]
        public IActionResult Delete(InstrumentViewModel vm)
        {
            // no ModelState.IsValid check here bc there's no user input - 
            // only BookId in hidden field is posted from form. 
            data.Instruments.Delete(vm.Instrument); // cascading delete will get BookAuthors
            data.Save();
            TempData["message"] = $"{vm.Instrument.Name} removed from Instruments.";
            return RedirectToAction("Search");  // PRG pattern
        }

        // private helper methods
        private ViewResult GetInstrument(int id, string operation)
        {
            var instrument = new InstrumentViewModel();
            Load(instrument, operation, id);
            return View("Instrument", instrument);
        }
        private void Load(InstrumentViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Instrument = new Instrument();
            }
            else
            {
                vm.Instrument = data.Instruments.Get(new QueryOptions<Instrument>
                {
                    Includes = "InstrumentBrands.Brand, Department",
                    Where = i => i.InstrumentId == (id ?? vm.Instrument.InstrumentId)
                });
            }

            vm.SelectedBrands = vm.Instrument.InstrumentBrands?.Select(
                ib => ib.Brand.BrandId).ToArray();
            vm.Brands = data.Brands.List(new QueryOptions<Brand>
            {
                OrderBy = b => b.BrandName
            });
            vm.Departments = data.Departments.List(new QueryOptions<Department>
            {
                OrderBy = d => d.Name
            });
        }

    }
}