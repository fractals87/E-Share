using E_Share.Data;
using E_Share.Helper;
using E_Share.Models;
using E_Share.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.DAL.Repository
{
    public class VehicleRepository
    {
        protected readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles(int? categoryIdFilter, int? cityIdFilter)
        {
            var vehicles = _context.Vehicles.Include(v => v.Category).Include(v => v.City).Include(v => v.Status).Include(v => v.Rides);
            if (categoryIdFilter != null)
                vehicles.Where(f => f.CategoryId == categoryIdFilter);
            if (cityIdFilter != null)
                vehicles.Where(f => f.CityId == cityIdFilter);
            return await vehicles.ToListAsync();
        }


        public async Task<IEnumerable<Ride>> GetRidesInUse(string UserId)
        {
            var vehicles = _context.Rides.Include(i => i.Vehicle)
                                         .Include(i => i.Vehicle.Category)
                                         .Include(i => i.User)
                                         .Where(f => f.UserId == UserId && f.DateStop == DateTime.MinValue);
            return await vehicles.ToListAsync();
        }

        public async Task<IEnumerable<VehicleDTO>> GetVehiclesDTO()
        {
            var vehicles = await _context.Vehicles.Include(v => v.Category)
                                                  .Where(f => f.StatusId == 1)
                                                  .Where(f => f.Category.Avalailable.Any(a => a.DayOfWeek == DateTime.Now.DayOfWeek && a.CityId == f.CityId && a.StartService < DateTime.Now.TimeOfDay && a.EndService > DateTime.Now.TimeOfDay))
                                                  .Select(f => new VehicleDTO { code = f.Code, batteryresidue = f.BatteryResidue, latitude = f.Latitude, longitude = f.Longitude, category = f.Category.Description }).ToListAsync();
            return vehicles;
        }


        public async Task<IEnumerable<Vehicle>> GetVehiclesPaginated(int? categoryIdFilter, int? cityIdFilter, int pageSize, int? pageNumber)
        {
            IQueryable<Vehicle> vehicles = _context.Vehicles.Include(v => v.Category).Include(v => v.City).Include(v => v.Status).Include(v => v.Rides);
            if (categoryIdFilter != null)
                vehicles = vehicles.Where(f => f.CategoryId == categoryIdFilter);
            if (cityIdFilter != null)
                vehicles = vehicles.Where(f => f.CityId == cityIdFilter);
            return await PaginatedList<Vehicle>.CreateAsync(vehicles, pageNumber ?? 1, pageSize);
        }

        public async Task<Vehicle> GetVehicleByID(int id)
        {
            return await _context.Vehicles.Include(v => v.Category)
                                    .Include(v => v.City)
                                    .Include(v => v.Status)
                                    .Include(v => v.Rides)
                                    .Include("Rides.User")
                                    .Where(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Vehicle> GetVehicleByCode(string code)
        {
            return await _context.Vehicles.Include(v => v.Category)
                                    .Include(v => v.City)
                                    .Include(v => v.Status)
                                    .Include(v => v.Rides)
                                    .Where(f => f.Code == code).FirstOrDefaultAsync();
        }

        public async Task InsertVehicle(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
        }

        public async Task InsertRide(string UserId, int VehicleId)
        {
            Ride ride = new Ride { UserId = UserId, VehicleId = VehicleId, DateStart = DateTime.Now };
            await _context.Rides.AddAsync(ride);
        }

        public async Task DeleteVehicle(int id)
        {
            Vehicle vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Entry(vehicle).State = EntityState.Modified;
        }

        public void UpdateRide(Ride ride)
        {
            _context.Entry(ride).State = EntityState.Modified;
        }

        public bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}

