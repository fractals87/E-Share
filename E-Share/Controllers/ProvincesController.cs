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
    public class ProvincesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public ProvincesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Provinces
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.ProvinceRepository.GetProvinces());
        }

        // GET: Provinces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _unitOfWork.ProvinceRepository.GetProvinceByID((int)id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // GET: Provinces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Abbreviation,Name")] Province province)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.ProvinceRepository.InsertProvince(province);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(province);
        }

        // GET: Provinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _unitOfWork.ProvinceRepository.GetProvinceByID((int)id);
            if (province == null)
            {
                return NotFound();
            }
            return View(province);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Abbreviation,Name")] Province province)
        {
            if (id != province.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ProvinceRepository.UpdateProvince(province);
                    await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.ProvinceRepository.ProvinceExists(province.Id))
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
            return View(province);
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _unitOfWork.ProvinceRepository.GetProvinceByID((int)id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.ProvinceRepository.DeleteProvince((int)id);
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


    }
}
