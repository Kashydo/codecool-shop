using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ILogger<CartController> _logger2;
        public ProductService ProductService { get; set; }
        public CartService CartService { get; set; }

        public ProductController(ILogger<ProductController> logger, ILogger<CartController> logger2)


        {
            _logger = logger;
            _logger2 = logger2;
            CartService = new CartService(
                CartDaoMemory.GetInstance());
            ProductService = new ProductService(ProductDaoMemory.GetInstance(), ProductCategoryDaoMemory.GetInstance());

        }

        public IActionResult Index()
        {

            var products = ProductService.GetProductsForCategory(1);
            ViewData["Test"] = "rterrthyrt";

            if (products != null)
            {
                ViewData["Products"] = products.ToList();
            }
            else
            {
                ViewData["Products"] = new List<Product>(); // Domyślna pusta lista, jeśli nie ma produktów
            }

            var cart = CartService.GetCart(1);

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddToCart(int productId, Cart cart)
        {
            var item = ProductService.GetProduct(productId);
            cart.AddItemToCart(item);
            return RedirectToAction("Index");
        }
    }
}
