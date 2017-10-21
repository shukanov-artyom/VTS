using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        private readonly IOrderProcessor orderProcessor;

        public CartController(IProductRepository repository,
            IOrderProcessor orderProcessor)
        {
            this.repository = repository;
            this.orderProcessor = orderProcessor;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel{
                Cart = cart, 
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
            // заполняются данными формы
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                throw new ObjectNotFoundException("Product not found");
            }
            cart.AddItem(product, 1);
            return RedirectToAction("Index", "Cart", new {returnUrl});
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        // заполняются данными формы
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                throw new ObjectNotFoundException("Product not found");
            }
            cart.RemoveLine(product);
            return RedirectToAction("Index", "Cart", new { returnUrl });
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView (cart);
        }
    }
}