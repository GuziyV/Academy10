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
    class PlaneTypeRepository : Repository<PlaneType>
    {

        public PlaneTypeRepository(AirportContext context) : base(context)
        {
        }

      
    }
}
