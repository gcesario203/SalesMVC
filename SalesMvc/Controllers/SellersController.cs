using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesMvc.Data;
using SalesMvc.Models;
using SalesMvc.Services;

namespace SalesMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;

        public SellersController(SellerService ss)
        {
            _sellerService = ss;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller pSeller)
        {
            _sellerService.Insert(pSeller);
            return RedirectToAction(nameof(Index));
        }
    }
}
