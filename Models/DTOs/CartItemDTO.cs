/***************************************************************
* Name        : CartItemDTO.cs
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

namespace FinalProject.Models
{
    // used when storing CartItem data to persistent cookie. Use a DTO so only store the 
    // minimal amount of data needed to restore data from database.

    public class CartItemDTO
    {
        public int InstrumentId { get; set; }
        public int Quantity { get; set; }
    }
}

