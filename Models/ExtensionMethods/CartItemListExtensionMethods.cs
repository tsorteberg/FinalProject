/***************************************************************
* Name        : CartItemListExtensionMethods.cs
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
using System.Linq;
using System.Collections.Generic;
using FinalProject.Models;

namespace FinalProject.Models
{
    // add a ToDtoList() method to a List of CartItem objects, to make the code
    // that converts it to a list of CartItemDTO objects cleaner.

    public static class CartItemListExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO
            {
                InstrumentId = ci.Instrument.InstrumentId,
                Quantity = ci.Quantity
            }).ToList();
    }
}
