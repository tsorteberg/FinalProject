/***************************************************************
* Name        : Admin/Controllers/ValidationController.cs
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
    public class ValidationController : Controller
    {
        private Repository<Brand> brandData { get; set; }
        private Repository<Department> departmentData { get; set; }

        public ValidationController(FinalContext ctx)
        {
            brandData = new Repository<Brand>(ctx);
            departmentData = new Repository<Department>(ctx);
        }

        public JsonResult CheckGenre(string departmentId)
        {
            var validate = new Validate(TempData);
            validate.CheckDepartment(departmentId, departmentData);
            if (validate.IsValid)
            {
                validate.MarkDepartmentChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckAuthor(string brandName, string productLine, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckBrand(brandName, productLine, operation, brandData);
            if (validate.IsValid)
            {
                validate.MarkBrandChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

    }
}