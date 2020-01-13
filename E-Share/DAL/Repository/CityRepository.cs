using E_Share.Data;
using E_Share.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.DAL.Repository
{
    public class CityRepository
    {
        protected readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.Include(v => v.Province).ToListAsync();
        }

        public async Task<City> GetCityByID(int id)
        {
            return await _context.Cities.Include(v => v.Province)
                                    .Where(f=>f.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertCity(City city)
        {
           await _context.Cities.AddAsync(city);
        }

        public async Task DeleteCity(int id)
        {
            City city = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(city);
        }

        public void UpdateCity(City city)
        {
            _context.Entry(city).State = EntityState.Modified;
        }

        public bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}

