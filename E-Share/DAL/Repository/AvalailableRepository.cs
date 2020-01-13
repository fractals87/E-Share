using E_Share.Data;
using E_Share.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.DAL.Repository
{
    public class AvalailableRepository
    {
        protected readonly ApplicationDbContext _context;

        public AvalailableRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Avalailable>> GetAvalailable()
        {
            return await _context.Avalailable.ToListAsync();
        }

        public async Task<Avalailable> GetAvalailableByID(int id)
        {
            return await _context.Avalailable.Where(f=>f.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertAvalailable(Avalailable avalailable)
        {
           await _context.Avalailable.AddAsync(avalailable);
        }

        public async Task DeleteAvalailable(int id)
        {
            Avalailable avalailable = await _context.Avalailable.FindAsync(id);
            _context.Avalailable.Remove(avalailable);
        }

        public void UpdateAvalailable(Avalailable avalailable)
        {
            _context.Entry(avalailable).State = EntityState.Modified;
        }

        public bool AvalailableExists(int id)
        {
            return _context.Avalailable.Any(e => e.Id == id);
        }
    }
}

