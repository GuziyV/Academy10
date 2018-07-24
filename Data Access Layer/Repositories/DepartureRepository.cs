using Data_Access_Layer.Contexts;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    class DepartureRepository : Repository<Departure>
    {
        public DepartureRepository(AirportContext context) : base(context)
        {
            
        }

        public override async Task<Departure> Get(int id)
        {
            return await dbSet.Include(d => d.Crew).ThenInclude(c => c.Pilot).Include(d =>d.Crew).ThenInclude(c => c.Stewardesses)
                .Include(d => d.Flight).ThenInclude(f => f.Tickets)
                .Include(d => d.Plane).ThenInclude(p => p.PlaneType).FirstOrDefaultAsync(s => s.Id == id);
        }

        public override async Task<IEnumerable<Departure>> GetAll()
        {
            return  await dbSet.Include(d => d.Crew).ThenInclude(c => c.Pilot).Include(d => d.Crew).ThenInclude(c => c.Stewardesses)
                .Include(d => d.Flight).ThenInclude(f => f.Tickets)
                .Include(d => d.Plane).ThenInclude(p => p.PlaneType).ToListAsync();
        }
    }
}
