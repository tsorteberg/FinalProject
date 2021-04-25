/***************************************************************
* Name        : Admin/Models/Validate.cs
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
using Microsoft.AspNetCore.Mvc.ViewFeatures;

/* 
 * Note about validation: Admin area allows Author to be inserted, updated, and deleted. 
 * Check whether first name and last name are in database should only happen on insert,
 * so additional field of 'Operation' is needed to determine when to hit database. Genre
 * can be inserted, updated, and deleted, too, but because the thing being checked (GenreId) 
 * is also the primary key, application doesn't allow it to be changed on edit like it allows
 * the Author name fields to be changed. So, on edit, GenreId is a read-only field, so it's
 * not changed by user, so no check is needed. Thus, Genre check doesn't need additional
 * 'Operation' field. 
 * 
 * Genre check is necessary bc if try to add a GenreId that already exists in database, will
 * get EF duplicate primary key error. The Author insert won't throw errors, but still a 
 * good check to have, to help reduce bad data. 
 */
namespace FinalProject.Models
{
    // used by client-side and server-side remote validation checks
    public class Validate
    {
        // private constants for working with TempData
        private const string DepartmentKey = "validDepartment";
        private const string BrandKey = "validBrand";

        // constructor and private TempData property
        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        // public properties
        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        // genre
        public void CheckDepartment(string departmentId, Repository<Department> data)
        {
            Department entity = data.Get(departmentId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Department id {departmentId} is already in the database.";
        }
        public void MarkDepartmentChecked() => tempData[DepartmentKey] = true;
        public void ClearDepartment() => tempData.Remove(DepartmentKey);
        public bool IsDepartmentChecked => tempData.Keys.Contains(DepartmentKey);

        // author
        public void CheckBrand(string brandName, string productLine, string operation, Repository<Brand> data)
        {
            Brand entity = null;
            if (Operation.IsAdd(operation)) // only check database on add
            {
                entity = data.Get(new QueryOptions<Brand>
                {
                    Where = b => b.BrandName == brandName && b.ProductLine == productLine
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Brand {entity.BrandName} is already in the database.";
        }
        public void MarkBrandChecked() => tempData[BrandKey] = true;
        public void ClearBrand() => tempData.Remove(BrandKey);
        public bool IsBrandChecked => tempData.Keys.Contains(BrandKey);
    }
}