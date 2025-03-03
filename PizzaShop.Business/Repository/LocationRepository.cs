using Microsoft.EntityFrameworkCore;
using PizzaShop.Business.Interface;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Business.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly PizzaShopDbContext _context;

        public LocationRepository(PizzaShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<State>> GetStatesByCountryIdAsync(int countryId)
        {
            return await _context.States
                .Where(s => s.Countryid == countryId)
                .ToListAsync();
        }

        public async Task<List<City>> GetCitiesByStateIdAsync(int stateId)
        {
            return await _context.Cities
                .Where(c => c.Stateid == stateId)
                .ToListAsync();
        }
    }
}
