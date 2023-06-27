﻿using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.Shopping.GetAll(u => u.ApplicationUserId == userId, 
                includeProperties: "Product")
            };
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetCartPrice(cart);
                ShoppingCartVM.OrdertoDo += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Summary()
        {
            return View();
        }
        public IActionResult plus(int cartId) 
        {
            var result = _unitOfWork.Shopping.Get(u => u.Id == cartId);
            result.Count += 1;
            _unitOfWork.Shopping.Update(result);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult minus(int cartId) 
        {
            var result = _unitOfWork.Shopping.Get(u=>u.Id == cartId);
            if(result.Count <= 1)
            {
                _unitOfWork.Shopping.Remove(result);
            }
            else
            {
                result.Count -= 1;
                _unitOfWork.Shopping.Update(result);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult remove(int cartId) 
        {
            var result = _unitOfWork.Shopping.Get(u => u.Id == cartId);
            _unitOfWork.Shopping.Remove(result);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }



        private double GetCartPrice(ShoppingCart shoppingCart) 
        {
            if(shoppingCart.Count <=50) 
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if(shoppingCart.Count<=100)
                {
                    return shoppingCart.Product.Price50;
                }
                else
                {
                    return shoppingCart.Product.Price100;
                }
            }
        }
    }
}
