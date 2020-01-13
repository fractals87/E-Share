using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Share.Data;
using E_Share.Models;
using E_Share.DAL.Repository;
using E_Share.DAL;
using E_Share.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace E_Share.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        //private readonly VehicleRepository _vehicleRepository;
        private readonly UnitOfWork _unitOfWork;

        public VehiclesController(UnitOfWork unitOfWork)//VehicleRepository vehicleRepository)//ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Release(int Id)
        {
            var user = await _unitOfWork.UserRepository.GetUserByClaims(User);
            var ride = await _unitOfWork.RideRepository.GetRideByID(Id);
            if (ride != null)
            {
                ride.DateStop = DateTime.Now;
                ride.Price = ride.CalcPrice;
                ride.Vehicle.StatusId = 1;
                _unitOfWork.RideRepository.UpdateRide(ride);

                user.Credit = user.Credit - ride.Price;
                _unitOfWork.UserRepository.UpdateUser(user);
                await _unitOfWork.Save();
            }

            return RedirectToAction(nameof(MyVehicles));
        }

        public async Task<IActionResult> Find(double latitude, double longitude )
        {
            return View();
        }

        public async Task<JsonResult> GetVehicles()
        {
            //return Json(_context.Cities.Where(f => f.ProvinceId == provinceIdFilter).OrderBy(f => f.Name));
            return Json(await _unitOfWork.VehicleRepository.GetVehiclesDTO());
        }

        public async Task<IActionResult> CatchVehicles(string VehicleCode, int typeRequest)
        {
            var user = await _unitOfWork.UserRepository.GetUserByClaims(User );
            if(user.Credit > 0)
            {
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByCode(VehicleCode);
                if (vehicle == null)
                    if (typeRequest == 1)
                        return RedirectToAction(nameof(Find));
                    else
                        return Json(new ResJSON { Status = "KO", Message = "Vehicle not found" });
                if (vehicle.StatusId== 1)
                {
                    vehicle.StatusId = 2;
                    _unitOfWork.VehicleRepository.UpdateVehicle(vehicle);
                    await _unitOfWork.VehicleRepository.InsertRide(user.Id, vehicle.Id);
                    await _unitOfWork.Save();
                    if (typeRequest == 1)
                        return RedirectToAction(nameof(MyVehicles));
                    else
                        return Json(new ResJSON { Status = "OK", Message = "Vehicle cached" });
                }
                else
                {
                    if (typeRequest == 1)
                        return RedirectToAction(nameof(Find));
                    else
                        return Json(new ResJSON { Status = "KO", Message = "Vehicle unavailable" });
                }
            }
            if (typeRequest == 1)
                return RedirectToAction(nameof(Find));
            else
                return Json(new ResJSON { Status = "KO",  Message = "No Money" });
        }

        public async Task<IActionResult> MyVehicles(double? latitude, double? longitude)
        {
            var user = await _unitOfWork.UserRepository.GetUserByClaims(User);
            var rides = await _unitOfWork.VehicleRepository.GetRidesInUse(user.Id);
            bool end = false;
            foreach (var ride in rides)
            {
                if (rides.Sum(f=>f.CalcPrice) >= user.Credit)
                {
                    ride.DateStop = DateTime.Now;
                    ride.Price = ride.CalcPrice;
                    ride.Vehicle.StatusId = 1;
                    _unitOfWork.RideRepository.UpdateRide(ride);
                    end = true;
                }
                if (latitude != null && longitude != null)
                {
                    ride.Vehicle.Latitude = (double)latitude;
                    ride.Vehicle.Longitude = (double)longitude;
                    _unitOfWork.VehicleRepository.UpdateVehicle(ride.Vehicle);
                }
            }
            if(end == true)
            {
                user.Credit = user.Credit - rides.Sum(f => f.Price);
                _unitOfWork.UserRepository.UpdateUser(user);
            }
                
            await _unitOfWork.Save();
            return View(rides);
        }


        // GET: Vehicles
        public async Task<IActionResult> Index(int? categoryIdFilter, int? cityIdFilter, int? pageNumber)
        {
            int pageSize = 5;
            ViewBag.categoryIdFilter = new SelectList(await _unitOfWork.CategoryRepository.GetCategories(), "Id", "Description", categoryIdFilter);
            ViewBag.cityIdFilter = new SelectList(await _unitOfWork.CityRepository.GetCities(), "Id", "Name", cityIdFilter);
            return View(await _unitOfWork.VehicleRepository.GetVehiclesPaginated(categoryIdFilter, cityIdFilter, pageSize, pageNumber));
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByID((int)id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.CategoryRepository.GetCategories(), "Id", "Description");
            ViewData["CityId"] = new SelectList(await _unitOfWork.CityRepository.GetCities(), "Id", "Name");
            ViewData["StatusId"] = new SelectList(await _unitOfWork.StatusRepository.GetStatuses(), "Id", "Description");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Latitude,Longitude,BatteryResidue,StatusId,CategoryId,CityId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.VehicleRepository.InsertVehicle(vehicle);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.CategoryRepository.GetCategories(), "Id", "Description", vehicle.CategoryId);
            ViewData["CityId"] = new SelectList(await _unitOfWork.CityRepository.GetCities(), "Id", "Name", vehicle.CityId);
            ViewData["StatusId"] = new SelectList(await _unitOfWork.StatusRepository.GetStatuses(), "Id", "Description", vehicle.StatusId);
            return View(vehicle);
        }

        //// GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByID((int)id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.CategoryRepository.GetCategories(), "Id", "Description", vehicle.CategoryId);
            ViewData["CityId"] = new SelectList(await _unitOfWork.CityRepository.GetCities(), "Id", "Name", vehicle.CityId);
            ViewData["StatusId"] = new SelectList(await _unitOfWork.StatusRepository.GetStatuses(), "Id", "Description", vehicle.StatusId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Latitude,Longitude,BatteryResidue,StatusId,CategoryId,CityId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.VehicleRepository.UpdateVehicle(vehicle);
                    await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.VehicleRepository.VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.CategoryRepository.GetCategories(), "Id", "Description", vehicle.CategoryId);
            ViewData["CityId"] = new SelectList(await _unitOfWork.CityRepository.GetCities(), "Id", "Name", vehicle.CityId);
            ViewData["StatusId"] = new SelectList(await _unitOfWork.StatusRepository.GetStatuses(), "Id", "Description", vehicle.StatusId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByID((int)id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.VehicleRepository.DeleteVehicle((int)id);
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
