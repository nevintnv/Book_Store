using Book.DataAccess.Repository.IRepository;
using Book.Models;
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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {


            if (id == null || id == 0)
            {
                //Create

                return View(new Company());
            }
            else
            {
                //Update

                Company obj = _unitOfWork.Company.Get(x => x.Id == id);
                return View(obj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Company Updated successfully";


                }

                return RedirectToAction("Index", "Company"); //Redirecttoaction will help to go to Index view
            }

            return View();

        }






        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var result = _unitOfWork.Company.Get(x => x.Id == id);
            if (result == null)
            {
                return Json(new { success = false, Message = "Error while deleting the detail" });
            }

            _unitOfWork.Company.Remove(result);
            _unitOfWork.Save();
            return Json(new { success = true, Message = "Company details deleted" });


        }

        #endregion
    }

}
