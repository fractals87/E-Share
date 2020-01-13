using E_Share.Data;
using E_Share.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.DAL.Repository
{
    public class RideRepository
    {
        protected readonly ApplicationDbContext _context;

        public RideRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ride>> GetRides()
        {
            return await _context.Rides.ToListAsync();
        }

        public async Task<IEnumerable<Ride>> GetRidesOfUser(string UserId)
        {
            return await _context.Rides.Include(f=>f.Vehicle).Include(f=>f.User).Where(f=>f.UserId == UserId).Where(f=>f.DateStop != DateTime.MinValue).ToListAsync();
        }

        public async Task<Ride> GetRideByID(int id)
        {
            return await _context.Rides.Include(i => i.Vehicle)
                                         .Include(i => i.Vehicle.Category).Where(f=>f.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertRide(Ride ride)
        {
           await _context.Rides.AddAsync(ride);
        }

        public async Task DeleteRide(int id)
        {
            Ride ride = await _context.Rides.FindAsync(id);
            _context.Rides.Remove(ride);
        }

        public void UpdateRide(Ride ride)
        {
            _context.Entry(ride).State = EntityState.Modified;
        }

        public bool RideExists(int id)
        {
            return _context.Rides.Any(e => e.Id == id);
        }
    }
}

