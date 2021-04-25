/***************************************************************
* Name        : Admin/Models/InstrumentViewModel.cs
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    // Rather than try to build a custom validator for the SelectedAuthors property, 
    // this class is validatable. This is an example of class-level validation. It 
    // leads to a two-step validation process, but it's adequate for this app.

    public class InstrumentViewModel : IValidatableObject
    {
        public Instrument Instrument { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public int[] SelectedBrands { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (SelectedBrands == null)
            {
                yield return new ValidationResult(
                  "Please select at least one brand.",
                  new[] { nameof(SelectedBrands) });
            }
        }

    }
}
