/***************************************************************
* Name        : Admin/Models/Operation.cs
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
namespace FinalProject.Models
{
    // static class used mostly by admin area views to determine which fields to 
    // display, but also used in Admin area book controller and validate class.

    public static class Operation
    {
        public static bool IsAdd(string action) => action.EqualsNoCase("add");
        public static bool IsDelete(string action) => action.EqualsNoCase("delete");
    }
}
