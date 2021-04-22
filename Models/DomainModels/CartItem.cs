/***************************************************************
* Name        : CartItem.cs
* Author      : Tom Sorteberg
* Created     : 04/21/2021
* Course      : CIS 174
* Version     : 1.0
* OS          : Windows 10 Pro, Visual Studio Community 2019
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : Final Project
* I have not used unauthorized source code, either modified or
* unmodified. I have not given other fellow student(s) access
* to my program.
***************************************************************/
using Newtonsoft.Json;

namespace FinalProject.Models
{
    // Instances of this class are stored in session after being converted to a 
    // JSON string. Since the readonly Subtotal property doesn't need to be
    // stored, it's decorated with the [JsonIgnore] attribute so it will 
    // be skipped when the JSON string is created.

    public class CartItem
    {
        public InstrumentDTO Instrument { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double Subtotal => Instrument.Price * Quantity;
    }
}
