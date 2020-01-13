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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace E_Share.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UnitOfWork _unityOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersController(UnitOfWork unitOfWork, 
                               UserManager<ApplicationUser> userManager, 
                               RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unityOfWork = unitOfWork;
        }

        // GET: Users
        public async Task<IActionResult> Index(string nameSurnameFilter)
        {
            return View(await _unityOfWork.UserRepository.GetUsers());
        }

        //GET IActionResult
        public async Task<IActionResult> AssignRole(string UserId)
        {
            if (String.IsNullOrEmpty(UserId))
            {
                return NotFound();
            }

            var user = await _unityOfWork.UserRepository.GetUserById(UserId);
            var UserRole = new ApplicationUserRole { User = user, UserId = user.Id };
            //await _unityOfWork.UserRepository.InsertUserRole(UserRole);
            //await _unityOfWork.Save();
            ViewData["RoleId"] = new SelectList(await _unityOfWork.UserRepository.GetRoles(), "Id", "Name");
            return View(UserRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AssignRole([Bind("UserId,RoleId")] ApplicationUserRole UserRole)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(UserRole);
                await _unityOfWork.UserRepository.InsertUserRole(UserRole);
                await _unityOfWork.Save();
                return RedirectToAction("Index", "Users");
            }
            return View(UserRole);
        }

        //GET User/DeleteRole

        public async Task<IActionResult> DeleteRole (string RoleName, string UserId)
        {
            if(String.IsNullOrEmpty(RoleName) || String.IsNullOrEmpty(UserId))
            {
                return NotFound();
            }
            var usr = await _userManager.FindByIdAsync(UserId);
            await _userManager.RemoveFromRoleAsync(usr, RoleName);

            return RedirectToAction("Index","Users");
        }
    }
}
