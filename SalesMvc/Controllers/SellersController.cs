
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using SalesMvc.Models;
using SalesMvc.Services;
using SalesMvc.Models.ViewModels;
using SalesMvc.Services.Exceptions;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
         
            var sellerInfo = await _sellerService.FindByIdAsync(id.Value);
            if (id == null || sellerInfo == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Invalid Id" });
            }

            return View(sellerInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SellerFormViewModel pSeller)
        {
            if (!ModelState.IsValid)
            {
                return View(pSeller);
            }
            var FormSeller = pSeller.Seller;
            Seller Seller = new Seller(FormSeller.Id, FormSeller.Name, FormSeller.Email, FormSeller.BirthDate, FormSeller.BaseSalary, FormSeller.Department);

            Seller.DepartmentId = FormSeller.DepartmentId;
            await _sellerService.InsertAsync(Seller);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            var seller = await _sellerService.FindByIdAsync(id.Value);

            if(id == null || seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Invalid Id" });
            }

            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var seller = await _sellerService.FindByIdAsync(id.Value);

            if (id == null || seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Invalid Id" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,SellerFormViewModel pSeller)
        {
            if (!ModelState.IsValid)
            {
                return View(pSeller);
            }
            if (id != pSeller.Seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            var FormSeller = pSeller.Seller;
            try{
                Seller Seller = new Seller(FormSeller.Id, FormSeller.Name, FormSeller.Email, FormSeller.BirthDate, FormSeller.BaseSalary, FormSeller.Department);
                Seller.DepartmentId = FormSeller.DepartmentId;
                await _sellerService.UpdateAsync(Seller);
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
