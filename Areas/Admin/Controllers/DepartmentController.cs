/***************************************************************
* Name        : Admin/Controllers/DepartmentController.cs
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
    public class DepartmentController : Controller
    {
        private Repository<Department> data { get; set; }
        public DepartmentController(FinalContext ctx) => data = new Repository<Department>(ctx);

        public ViewResult Index()
        {
            // clear any previous searches
            var search = new SearchData(TempData);
            search.Clear();

            var departments = data.List(new QueryOptions<Department>
            {
                OrderBy = d => d.Name
            });
            return View(departments);
        }

        // add
        [HttpGet]
        public ViewResult Add() => View("Department", new Department());

        [HttpPost]
        public IActionResult Add(Department department)
        {
            // server-side version of remote validation 
            var validate = new Validate(TempData);
            if (!validate.IsDepartmentChecked)
            {
                validate.CheckDepartment(department.DepartmentId, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(department.DepartmentId), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(department);
                data.Save();
                validate.ClearDepartment();
                TempData["message"] = $"{department.Name} added to Departments.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Department", department);
            }
        }

        // edit
        [HttpGet]
        public ViewResult Edit(string id) => View("Department", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            // no remote validation of genre id on edit
            if (ModelState.IsValid)
            {
                data.Update(department);
                data.Save();
                TempData["message"] = $"{department.Name} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Department", department);
            }
        }

        // delete
        [HttpGet]
        public IActionResult Delete(string id)
        {
            // because cascading deletes are turned off when DbContext configured (so don't automatically
            // delete books when delete genre), will get EF foreign key error if try to delete a genre that's
            // associated with any books. Rather than catch and handle error, this code includes books when
            // retrieving the genre to be deleted. Then, if there are any Book objects in the Books property,
            // it redirects the user to the search results page so they can see said books. 

            var department = data.Get(new QueryOptions<Department>
            {
                Includes = "Departments",
                Where = d => d.DepartmentId == id
            });

            if (department.Instruments.Count > 0)
            {
                TempData["message"] = $"Can't delete department {department.Name} "
                                    + "because it's associated with these instruments.";
                return GoToInstrumentSearchResults(id);
            }
            else
            {
                return View("Department", department);
            }
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            // no ModelState.IsValid check here bc there's no user input - 
            // only GenreId and Name in hidden fields are posted from form. 
            data.Delete(department);
            data.Save();
            TempData["message"] = $"{department.Name} removed from Departments.";
            return RedirectToAction("Index");  // PRG pattern
        }

        // view books by genre
        public RedirectToActionResult ViewInstruments(string id) => GoToInstrumentSearchResults(id);

        // private helper method
        private RedirectToActionResult GoToInstrumentSearchResults(string id)
        {
            // store genre search data in TempData and redirect
            var search = new SearchData(TempData)
            {
                SearchTerm = id,
                Type = "department"
            };
            return RedirectToAction("Search", "Instrument");
        }

    }
}