using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesMvc.Data;
using SalesMvc.Models;
using SalesMvc.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using SalesMvc.Services.Exceptions;

namespace SalesMvc.Services
{
    public class SellerService
    {
        private readonly SalesMvcContext _context;

        public SellerService(SalesMvcContext DbContext)
        {
            _context = DbContext;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller pSeller)
        {
            _context.Add(pSeller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(item => item.Department). FirstOrDefaultAsync(seller => seller.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var seller = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(seller);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            if(!await _context.Seller.AnyAsync(x=>x.Id == seller.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
