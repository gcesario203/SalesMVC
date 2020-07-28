
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using SalesMvc.Models;
using SalesMvc.Services;
using SalesMvc.Models.ViewModels;
using SalesMvc.Services.Exceptions;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace SalesMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService ss,DepartmentService ds)
        {
            _sellerService = ss;
            _departmentService = ds;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        public IActionResult Delete(int? id)
        {
         
            var sellerInfo = _sellerService.FindById(id.Value);
            if (id == null || sellerInfo == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Invalid Id" });
            }

            return View(sellerInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SellerFormViewModel pSeller)
        {
            var FormSeller = pSeller.Seller;
            Seller Seller = new Seller(FormSeller.Id, FormSeller.Name, FormSeller.Email, FormSeller.BirthDate, FormSeller.BaseSalary, FormSeller.Department);

            Seller.DepartmentId = FormSeller.DepartmentId;
            _sellerService.Insert(Seller);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            var seller = _sellerService.FindById(id.Value);

            if(id == null || seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Invalid Id" });
            }

            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            var seller = _sellerService.FindById(id.Value);

            if (id == null || seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Invalid Id" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,SellerFormViewModel pSeller)
        {
            if(id != pSeller.Seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            var FormSeller = pSeller.Seller;
            try{
                Seller Seller = new Seller(FormSeller.Id, FormSeller.Name, FormSeller.Email, FormSeller.BirthDate, FormSeller.BaseSalary, FormSeller.Department);
                Seller.DepartmentId = FormSeller.DepartmentId;
                _sellerService.Update(Seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
           
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
