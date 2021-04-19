/***************************************************************
* Name        : InstrumentsGridDTO.cs
* Author      : Tom Sorteberg
* Created     : 04/19/2021
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
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    // Inherits the general purpose GridDTO class and adds bookstore-specific 
    // properties for the filtering route segments defined in the Startup.cs file. 

    // Instances of this class are stored in session after being converted to a 
    // JSON string. Since the readonly DefaultFilter property doesn't need
    // to be stored, it's decorated with the [JsonIgnore] attribute so it will 
    // be skipped when the JSON string is created.

    public class InstrumentsGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Brand { get; set; } = DefaultFilter;
        public string Department { get; set; } = DefaultFilter;
        public string Price { get; set; } = DefaultFilter;
    }
}
