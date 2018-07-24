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
    class CrewRepository : Repository<Crew>
    {
        public CrewRepository(AirportContext context) : base(context)
        {
            
        }



         public override async Task<Crew> Get(int id)
         {
            return await dbSet.Include(c => c.Pilot).Include(c => c.Stewardesses)
                .FirstOrDefaultAsync(c => c.Id == id);
         }

        public override async Task<IEnumerable<Crew>> GetAll()
        {
            return await dbSet.Include(c => c.Pilot).Include(c => c.Stewardesses).ToListAsync();
        }
    }
}