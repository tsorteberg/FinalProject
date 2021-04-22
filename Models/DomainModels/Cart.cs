/***************************************************************
* Name        : Cart.cs
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
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FinalProject.Models
{
    // Cart class stores CartItem objects in session and persistent cookies. See 
    // CartItem.cs and BookDTO.cs files for more info re: issues with JSON serialization.

    public class Cart
    {
        private const string CartKey = "mycart";
        private const string CountKey = "mycount";

        private List<CartItem> items { get; set; }
        private List<CartItemDTO> storedItems { get; set; }

        private ISession session { get; set; }
        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public Cart(HttpContext ctx)
        {
            session = ctx.Session;
            requestCookies = ctx.Request.Cookies;
            responseCookies = ctx.Response.Cookies;
        }

        public void Load(Repository<Instrument> data)
        {
            items = session.GetObject<List<CartItem>>(CartKey);
            if (items == null)
            {
                items = new List<CartItem>();
                storedItems = requestCookies.GetObject<List<CartItemDTO>>(CartKey);
            }
            if (storedItems?.Count > items?.Count)
            {
                foreach (CartItemDTO storedItem in storedItems)
                {
                    var instrument = data.Get(new QueryOptions<Instrument>
                    {
                        Includes = "InstrumentBrands.Brands, Department",
                        Where = i => i.InstrumentId == storedItem.InstrumentId
                    });
                    if (instrument != null)
                    {
                        var dto = new InstrumentDTO();
                        dto.Load(instrument);

                        CartItem item = new CartItem
                        {
                            Instrument = dto,
                            Quantity = storedItem.Quantity
                        };
                        items.Add(item);
                    }
                }
                Save();
            }
        }

        public double Subtotal => items.Sum(i => i.Subtotal);
        public int? Count => session.GetInt32(CountKey) ?? requestCookies.GetInt32(CountKey);
        public IEnumerable<CartItem> List => items;

        public CartItem GetById(int id) =>
            items.FirstOrDefault(ci => ci.Instrument.InstrumentId == id);

        // if the user clicks "Add to Cart" and the item
        // is already in the cart, it's updated rather than duplicated.
        public void Add(CartItem item)
        {
            var itemInCart = GetById(item.Instrument.InstrumentId);
            // if new, add
            if (itemInCart == null)
            {
                items.Add(item);
            }
            else
            {  // otherwise, increase quantity amount by 1
                itemInCart.Quantity += 1;
            }
        }

        // when editing, replace quantity value with new one. Used when user edits a cart item
        // and changes its quantity
        public void Edit(CartItem item)
        {
            var itemInCart = GetById(item.Instrument.InstrumentId);
            if (itemInCart != null)
            {
                itemInCart.Quantity = item.Quantity;
            }
        }

        public void Remove(CartItem item) => items.Remove(item);
        public void Clear() => items.Clear();

        // stores updated cart items and new count in session and persistent cookie (stores smaller DTO in cookie). 
        // If count is zero, removes cart and count from session and cookie (this is so cart badge in navbar disappears 
        // when cart is emptied, rather than showing with a value of zero). 

        public void Save()
        {
            if (items.Count == 0)
            {
                session.Remove(CartKey);
                session.Remove(CountKey);
                responseCookies.Delete(CartKey);
                responseCookies.Delete(CountKey);
            }
            else
            {
                session.SetObject<List<CartItem>>(CartKey, items);
                session.SetInt32(CountKey, items.Count);
                responseCookies.SetObject<List<CartItemDTO>>(CartKey, items.ToDTO());
                responseCookies.SetInt32(CountKey, items.Count);
            }
        }
    }
}

