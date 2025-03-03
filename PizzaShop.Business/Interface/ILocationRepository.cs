using PizzaShop.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaShop.Business.Interface
{
    public interface ILocationRepository
    {
        Task<List<State>> GetStatesByCountryIdAsync(int countryId);
        Task<List<City>> GetCitiesByStateIdAsync(int stateId);
    }
}
