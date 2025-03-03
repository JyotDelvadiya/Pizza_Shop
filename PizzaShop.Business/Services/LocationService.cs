using PizzaShop.Business.Interface;
using PizzaShop.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaShop.Business.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<List<State>> GetStatesByCountryIdAsync(int countryId)
        {
            return await _locationRepository.GetStatesByCountryIdAsync(countryId);
        }

        public async Task<List<City>> GetCitiesByStateIdAsync(int stateId)
        {
            return await _locationRepository.GetCitiesByStateIdAsync(stateId);
        }
    }
}
