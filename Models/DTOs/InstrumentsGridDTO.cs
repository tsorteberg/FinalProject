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
