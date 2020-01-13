using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Share.Data;
using E_Share.Models;
using E_Share.DAL;
using Microsoft.AspNetCore.Authorization;

namespace E_Share.Controllers
{
    [Authorize]
    public class RidesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public RidesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Rides
        public async Task<IActionResult> Index()
        {
            var user = await _unitOfWork.UserRepository.GetUserByClaims(User);
            return View(await _unitOfWork.RideRepository.GetRidesOfUser(user.Id));
        }

        //// GET: Rides/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ride = await _context.Rides
        //        .Include(r => r.User)
        //        .Include(r => r.Vehicle)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ride == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ride);
        //}

        //// GET: Rides/Create
        //public IActionResult Create()
        //{
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        //    ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Code");
        //    return View();
        //}

        //// POST: Rides/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,UserId,VehicleId,DateStart,DateStop,Price")] Ride ride)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(ride);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ride.UserId);
        //    ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Code", ride.VehicleId);
        //    return View(ride);
        //}

        //// GET: Rides/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ride = await _context.Rides.FindAsync(id);
        //    if (ride == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ride.UserId);
        //    ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Code", ride.VehicleId);
        //    return View(ride);
        //}

        //// POST: Rides/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,VehicleId,DateStart,DateStop,Price")] Ride ride)
        //{
        //    if (id != ride.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(ride);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RideExists(ride.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ride.UserId);
        //    ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "Code", ride.VehicleId);
        //    return View(ride);
        //}

        //// GET: Rides/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ride = await _context.Rides
        //        .Include(r => r.User)
        //        .Include(r => r.Vehicle)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ride == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ride);
        //}

        //// POST: Rides/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var ride = await _context.Rides.FindAsync(id);
        //    _context.Rides.Remove(ride);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RideExists(int id)
        //{
        //    return _context.Rides.Any(e => e.Id == id);
        //}
    }
}
