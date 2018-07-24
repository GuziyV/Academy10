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
    class PlaneRepository : Repository<Plane>
    {

        public PlaneRepository(AirportContext context) : base(context)
        {
        }

       

        public override async Task<Plane> Get(int id)
        {
            return await dbSet.Include(p => p.PlaneType).FirstOrDefaultAsync(f => f.Id == id);
        }

        public override async Task<IEnumerable<Plane>> GetAll()
        {
            return await dbSet.Include(p => p.PlaneType).ToListAsync();
        }
    }
}
