/***************************************************************
* Name        : InstrumentDTO.cs
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
using System.Collections.Generic;

namespace FinalProject.Models
{
    // Trying to store a Book object in session can cause problems because the JSON 
    // serialization done in SessionExtensionMethods.cs can create circular references
    // as the serializer tries to follow all the navigation properties. You can decorate
    // those properties with the [JsonIgnore] attribute, but you can end up with that
    // scattered all around. Another way, shown here, is to create a DTO class with the 
    // data needed for the cart. The DTO includes a Load() method to transfer the needed 
    // data from a Book object.

    public class InstrumentDTO
    {
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Dictionary<int, string> Brands { get; set; }

        public void Load(Instrument instrument)
        {
            InstrumentId = instrument.InstrumentId;
            Name = instrument.Name;
            Price = instrument.Price;
            Brands = new Dictionary<int, string>();
            foreach (InstrumentBrand ib in instrument.InstrumentBrands)
            {
                Brands.Add(ib.Brand.BrandId, ib.Brand.BrandName);
            }
        }
    }

}

