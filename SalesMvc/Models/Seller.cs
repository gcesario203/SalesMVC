using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecords> Sales { get; set; } = new List<SalesRecords>();


        public Seller() { }

        public Seller(int pId,string pName,string pEmail,DateTime pBirthDate,double pBaseSalary,Department pDepartment)
        {
            Id = pId;
            Name = pName;
            Email = pEmail;
            BirthDate = pBirthDate;
            BaseSalary = pBaseSalary;
            Department = pDepartment;
        }

        public void AddSales(SalesRecords pSalesRecords)
        {
            Sales.Add(pSalesRecords);
        }

        public void RemoveSales(SalesRecords pSalesRecord)
        {
            Sales.Remove(pSalesRecord);
        }

        public double TotalSales(DateTime init,DateTime final)
        {
            return Sales.Where(sr => sr.Date >= init && sr.Date<=final)
                            .Sum(sr=>sr.Amount);
        }
    }
}
