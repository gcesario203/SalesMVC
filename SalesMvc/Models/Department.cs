using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }
        public Department(int pId, string pName)
        {
            Id = pId;
            Name = pName;
        }

        public void AddSeller(Seller pSeller)
        {
            Sellers.Add(pSeller);
        }

        public double TotalSales(DateTime init, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(init, final));
        }
    }
}
