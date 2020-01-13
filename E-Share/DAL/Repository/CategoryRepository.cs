using E_Share.Data;
using E_Share.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.DAL.Repository
{
    public class CategoryRepository
    {
        protected readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.Include(f=>f.Avalailable).Include("Avalailable.City").ToListAsync();
        }

        public async Task<Category> GetCategoryByID(int id)
        {
            return await _context.Categories.Include(f => f.Avalailable).Include("Avalailable.City").Where(f=>f.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertCategory(Category city)
        {
           await _context.Categories.AddAsync(city);
        }

        public async Task DeleteCategory(int id)
        {
            Category city = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(city);
        }

        public void UpdateCategory(Category city)
        {
            _context.Entry(city).State = EntityState.Modified;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}

