﻿using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Book_Store.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productlist = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productlist);
        }

        public IActionResult Details(int productid)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(x => x.Id == productid, includeProperties: "Category"),
                Count = 1,
                ProductId = productid
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartfromDb = _unitOfWork.Shopping.Get(u => u.ApplicationUserId == userId && 
                                                                    u.ProductId == shoppingCart.ProductId);
            if(cartfromDb != null) 
            {
                //Shopping cart exist
                cartfromDb.Count += shoppingCart.Count;
                _unitOfWork.Shopping.Update(cartfromDb);

            }
            else
            {
                // add cart record
                _unitOfWork.Shopping.Add(shoppingCart);
            }
            TempData["success"] = "Cart Updated Successfully";
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
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
    }
}