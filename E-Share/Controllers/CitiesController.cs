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
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public CitiesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.CityRepository.GetCities());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _unitOfWork.CityRepository.GetCityByID((int)id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProvinceId"] = new SelectList(await _unitOfWork.ProvinceRepository.GetProvinces(), "Id", "Abbreviation");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cap,ProvinceId")] City city)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(city);
                //await _context.SaveChangesAsync();
                await _unitOfWork.CityRepository.InsertCity(city);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProvinceId"] = new SelectList(await _unitOfWork.ProvinceRepository.GetProvinces(), "Id", "Abbreviation", city.ProvinceId);
            return View(city);
        }

        //// GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _unitOfWork.CityRepository.GetCityByID((int)id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["ProvinceId"] = new SelectList(await _unitOfWork.ProvinceRepository.GetProvinces(), "Id", "Abbreviation", city.ProvinceId);
            return View(city);
        }

        //// POST: Cities/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cap,ProvinceId")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(city);
                    //await _context.SaveChangesAsync();
                    _unitOfWork.CityRepository.UpdateCity(city);
                    await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.CityRepository.CityExists(city.Id))
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
            ViewData["ProvinceId"] = new SelectList(await _unitOfWork.ProvinceRepository.GetProvinces(), "Id", "Abbreviation", city.ProvinceId);
            return View(city);
        }

        //// GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _unitOfWork.CityRepository.GetCityByID((int)id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        //// POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.CityRepository.DeleteCity(id);
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
