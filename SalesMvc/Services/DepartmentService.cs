using SalesMvc.Data;
using SalesMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesMvcContext _context;

        public DepartmentService(SalesMvcContext DbContext)
        {
            _context = DbContext;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x=>x.Name).ToList();
        }
    }
}
