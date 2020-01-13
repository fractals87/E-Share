using E_Share.Data;
using E_Share.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.DAL.Repository
{
    public class ProvinceRepository
    {
        protected readonly ApplicationDbContext _context;

        public ProvinceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Province>> GetProvinces()
        {
            return await _context.Provinces.ToListAsync();
        }

        public async Task<Province> GetProvinceByID(int id)
        {
            return await _context.Provinces.Where(f=>f.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertProvince(Province province)
        {
           await _context.Provinces.AddAsync(province);
        }

        public async Task DeleteProvince(int id)
        {
            Province province = await _context.Provinces.FindAsync(id);
            _context.Provinces.Remove(province);
        }

        public void UpdateProvince(Province province)
        {
            _context.Entry(province).State = EntityState.Modified;
        }

        public bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(e => e.Id == id);
        }
    }
}

