using E_Share.Data;
using E_Share.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.DAL.Repository
{
    public class StatusRepository
    {
        protected readonly ApplicationDbContext _context;

        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Status>> GetStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status> GetStatusByID(int id)
        {
            return await _context.Statuses
                                    .Where(f=>f.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertStatus(Status status)
        {
           await _context.Statuses.AddAsync(status);
        }

        public async Task DeleteStatus(int id)
        {
            Status status = await _context.Statuses.FindAsync(id);
            _context.Statuses.Remove(status);
        }

        public void UpdateStatus(Status status)
        {
            _context.Entry(status).State = EntityState.Modified;
        }

        public bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.Id == id);
        }
    }
}

