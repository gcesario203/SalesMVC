using SalesMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMvc.Models.Enums;

namespace SalesMvc.Data
{
    public class SeedingService
    {
        private SalesMvcContext _context;

        public SeedingService(SalesMvcContext DbContext)
        {
            _context = DbContext;
        }

        public void Seed()
        {
            if (_context.Department.Any() ||
                _context.SalesRecords.Any() ||
                _context.Seller.Any())
            {
                return; // DB ja populado
            }

            Department d1 = new Department(1, "Eletronics");
            Department d2 = new Department(2, "Fashion");
            Department d3 = new Department(3, "Books");
            Department d4 = new Department(4, "Machines");

            Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1997, 6, 25), 900.00, d1);
            Seller s2 = new Seller(2, "Maisa Brown", "maisa@gmail.com", new DateTime(1997, 6, 25), 300.00, d2);
            Seller s3 = new Seller(3, "Lucas Purpule", "lucas@gmail.com", new DateTime(1997, 6, 25), 200.00, d3);
            Seller s4 = new Seller(4, "Maicon Green", "mg@gmail.com", new DateTime(1997, 6, 25), 11100.00, d4);

            SalesRecords sr1 = new SalesRecords(1, new DateTime(2020, 2, 16), 25000, SaleStatus.Billed,s1);
            SalesRecords sr2 = new SalesRecords(2, new DateTime(2020, 4, 6), 2225000, SaleStatus.Canceled, s2);
            SalesRecords sr3 = new SalesRecords(3, new DateTime(2020, 6, 21), 15000, SaleStatus.Pending, s1);

            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4);
            _context.SalesRecords.AddRange(sr1, sr2, sr3);

            _context.SaveChanges();
        }
    }
}
