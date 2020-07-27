using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMvc.Data;
using SalesMvc.Models;

namespace SalesMvc.Services
{
    public class SellerService
    {
        private readonly SalesMvcContext _context;

        public SellerService(SalesMvcContext DbContext)
        {
            _context = DbContext;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller pSeller)
        {
            _context.Add(pSeller);
            _context.SaveChanges();
        }
    }
}
