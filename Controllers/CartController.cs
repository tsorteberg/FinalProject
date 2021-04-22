/***************************************************************
* Name        : CartController.cs
* Author      : Tom Sorteberg
* Created     : 04/22/2021
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
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        private Repository<Instrument> data { get; set; }
        public CartController(FinalContext ctx) => data = new Repository<Instrument>(ctx);


        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);
            return cart;
        }

        public ViewResult Index()
        {
            // create a new Cart object and get items from session or restore from cookie and db
            Cart cart = GetCart();

            // create a new builder object to work with route data in session
            var builder = new InstrumentsGridBuilder(HttpContext.Session);

            // create a new view model object with cart and route information and pass it to the view
            var vm = new CartViewModel
            {
                List = cart.List,
                Subtotal = cart.Subtotal,
                InstrumentGridRoute = builder.CurrentRoute
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            // get the book the user chose from the database
            var instrument = data.Get(new QueryOptions<Instrument>
            {
                Includes = "InstrumentBrands.Brand, Department",
                Where = i => i.InstrumentId == id
            });
            if (instrument == null)
            {
                TempData["message"] = "Unable to add instrument to cart.";
            }
            else
            {
                // create and load a new Book DTO, then add it to a new CartItem object
                // with a default quantity of one.
                var dto = new InstrumentDTO();
                dto.Load(instrument);
                CartItem item = new CartItem
                {
                    Instrument = dto,
                    Quantity = 1  // default value
                };

                // add new item to cart and save to session state
                Cart cart = GetCart();
                cart.Add(item);
                cart.Save();

                TempData["message"] = $"{instrument.Name} added to cart";
            }

            // create a new builder object to work with route data in session, then 
            // redirect to Book/List action method, passing a dictionary of route segment 
            // values to build URL
            var builder = new InstrumentsGridBuilder(HttpContext.Session);
            return RedirectToAction("List", "Instrument", builder.CurrentRoute);
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Instrument.Name} removed from cart.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            Cart cart = GetCart();
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // get selected cart item from session and pass it to the view
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            if (item == null)
            {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else
            {
                return View(item);
            }
        }

        [HttpPost]
        public RedirectToActionResult Edit(CartItem item)
        {
            Cart cart = GetCart();
            cart.Edit(item);
            cart.Save();

            TempData["message"] = $"{item.Instrument.Name} updated";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }
}