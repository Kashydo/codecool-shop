using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuGet.DependencyResolver;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Codecool.CodecoolShop.Controllers
{
    public class HomeController : Controller
    {
        const int id = 1;
        private readonly ILogger<ProductController> _logger;
        private readonly ILogger<CartController> _logger2;
        public ProductService ProductService { get; set; }
        public CartService CartService { get; set; }

        public HomeController(ILogger<CartController> logger2, ILogger<ProductController> logger)


        {
            _logger = logger;
            _logger2 = logger2;
            CartService = new CartService(
                CartDaoMemory.GetInstance());
            ProductService = new ProductService(ProductDaoMemory.GetInstance(), ProductCategoryDaoMemory.GetInstance());
        }
        public ActionResult IndexViewBag(int categoryId = id)
        {
            var products = ProductService.GetProductsForCategory(categoryId);
            ViewData["Test"] = "rterrthyrt";

            if (products != null)
            {
                ViewData["Products"] = products.ToList();
            }
            else
            {
                ViewData["Products"] = new List<Product>(); // Domyślna pusta lista, jeśli nie ma produktów
            }

            var cart = CartService.GetCart(categoryId);

            if (cart != null)
            {
                ViewData["Cart"] = cart.ToString();
            }
            else
            {
                ViewData["Cart"] = "Cart is empty"; // Domyślna informacja, jeśli koszyk jest pusty
            }

            return View();
        }
    }
}