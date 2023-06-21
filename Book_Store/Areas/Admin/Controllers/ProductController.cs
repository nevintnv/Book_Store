using Book.DataAccess.Repository.IRepository;
using Book.Models.Models;
using Book.Models.ViewModels;
using Book.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Book_Store.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {

            productVM productVM = new()
            {
                objCategory = _unitOfWork.Category
                                         .GetAll()
                                         .Select(x => new SelectListItem
                                         {
                                             Text = x.Name,
                                             Value = x.CategoryId.ToString()
                                         }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //Create

                return View(productVM);
            }
            else
            {
                //Update

                productVM.Product = _unitOfWork.Product.Get(x => x.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(productVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; // to access route folder wwwwroot
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // random Guid to save filename
                    string productpath = Path.Combine(wwwRootPath, @"images\Product"); //This will help to upload file to the provided path

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldimagepath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimagepath))
                        {
                            System.IO.File.Delete(oldimagepath);
                        }
                    }
                    //Upload a new image

                    using (var filestream = new FileStream(Path.Combine(productpath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    obj.Product.ImageUrl = @"\images\Product\" + filename;
                }
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product created successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Updated successfully";


                }

                return RedirectToAction("Index", "Product"); //Redirecttoaction will help to go to Index view
            }

            return View();

        }






        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category")
                                                              .ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var result = _unitOfWork.Product.Get(x => x.Id == id);
            if (result == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }

            var oldimage = Path.Combine(_webHostEnvironment.WebRootPath, result.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(oldimage))
            {
                System.IO.File.Delete(oldimage);
            }
            _unitOfWork.Product.Remove(result);
            _unitOfWork.Save();
            return Json(new { success = true, Message = "Book details deleted" });


        }

        #endregion
    }

}
