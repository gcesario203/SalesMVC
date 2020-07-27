using System;
using System.ComponentModel;
using SalesMvc.Models.Enums;

namespace SalesMvc.Models
{
    public class SalesRecords
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }

        public SalesRecords() { }

        public SalesRecords(int pId , DateTime pDate,double pAmount,SaleStatus pStatus,Seller pSeller)
        {
            Id = pId;
            Date = pDate;
            Amount = pAmount;
            Status = pStatus;
            Seller = pSeller;
        }
    }
}
