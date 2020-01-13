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

namespace E_Share.Controllers
{
    public class AvalailablesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public AvalailablesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Avalailables
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Avalailable.Include(a => a.Category).Include(a => a.City);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: Avalailables/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var avalailable = await _context.Avalailable
        //        .Include(a => a.Category)
        //        .Include(a => a.City)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (avalailable == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(avalailable);
        //}

        // GET: Avalailables/Create
        public async Task<IActionResult> Create(int CategoryId)
        {
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.CategoryRepository.GetCategories(), "Id", "Description", CategoryId);
            ViewData["CityId"] = new SelectList(await _unitOfWork.CityRepository.GetCities(), "Id", "Name");
            return View(new Avalailable { CategoryId = CategoryId });
        }

        // POST: Avalailables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DayOfWeek,EndService,StartService,CityId,CategoryId")] Avalailable avalailable)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.AvalailableRepository.InsertAvalailable(avalailable);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Edit), "Categories", new { id = avalailable.CategoryId });
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.CategoryRepository.GetCategories(), "Id", "Description", avalailable.CategoryId);
            ViewData["CityId"] = new SelectList(await _unitOfWork.CityRepository.GetCities(), "Id", "Name", avalailable.CityId);
            return View(avalailable);
        }

        // GET: Avalailables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avalailable = await _unitOfWork.AvalailableRepository.GetAvalailableByID((int)id);
            if (avalailable == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.CategoryRepository.GetCategories(), "Id", "Description", avalailable.CategoryId);
            ViewData["CityId"] = new SelectList(await _unitOfWork.CityRepository.GetCities(), "Id", "Name", avalailable.CityId);
            return View(avalailable);
        }

        // POST: Avalailables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DayOfWeek,EndService,StartService,CityId,CategoryId")] Avalailable avalailable)
        {
            if (id != avalailable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.AvalailableRepository.UpdateAvalailable(avalailable);
                    await _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.AvalailableRepository.AvalailableExists(avalailable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), "Categories", new { id = avalailable.CategoryId });
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.CategoryRepository.GetCategories(), "Id", "Description", avalailable.CategoryId);
            ViewData["CityId"] = new SelectList(await _unitOfWork.CityRepository.GetCities(), "Id", "Name", avalailable.CityId);
            return View(avalailable);
        }

        // GET: Avalailables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avalailable = await _unitOfWork.AvalailableRepository.GetAvalailableByID((int)id);
            if (avalailable == null)
            {
                return NotFound();
            }

            return View(avalailable);
        }

        // POST: Avalailables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var av = await _unitOfWork.AvalailableRepository.GetAvalailableByID(id);
            var categoryId = av.CategoryId;
            await _unitOfWork.AvalailableRepository.DeleteAvalailable(id);
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Edit), "Categories", new { id =categoryId  });
        }
    }
}
