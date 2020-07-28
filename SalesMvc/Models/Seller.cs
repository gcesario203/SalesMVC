using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SalesMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
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
