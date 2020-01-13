using E_Share.Data;
using E_Share.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Share.DAL.Repository
{
    public class UserRepository
    {
        protected readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        //
        public UserRepository(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _context.Users.Include(i => i.UserRoles).ThenInclude(ii => ii.Role).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationRole>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _context.Users.Include(f => f.UserRoles).Include("UserRoles.Role").Include(f=>f.RechargeStories).Where(f=>f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetUserByClaims(ClaimsPrincipal User)
        {
            var currentUser = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return currentUser;
        }
        
        public async Task InsertUserRole(ApplicationUserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }

        public async Task InsertRecharge(RechargeStory rechargeStory)
        {
            await _context.RechargeStories.AddAsync(rechargeStory);
        }

        public void UpdateUser(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}

