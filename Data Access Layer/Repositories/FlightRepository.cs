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
    class FlightRepository : IRepository<Flight>
    {
        private AirportContext _context;

        public FlightRepository(AirportContext context)
        {
            _context = context;
        }

        public async Task Create(Flight item)
        {
             await _context.Flights.AddAsync(item);
        }

        public async Task Delete(int id)
        {
             _context.Flights.Remove(await _context.Flights.FindAsync(id));
        }

        public async Task<Flight> Get(int id)
        {
            return await _context.Flights.Include(f => f.Tickets).FirstOrDefaultAsync(f => f.Number == id);
        }

        public async Task<IEnumerable<Flight>> GetAll()
        {
            return await _context.Flights.Include(f => f.Tickets).ToListAsync();
        }

        public async Task Update(int id, Flight item)
        {
            var old = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(old);
            await _context.Flights.AddAsync(item);
        }

    }
}
