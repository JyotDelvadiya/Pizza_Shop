using PizzaShop.Data.Models;

public interface ILocationService{
    Task<List<State>> GetStatesByCountryIdAsync(int countryId);
    Task<List<City>> GetCitiesByStateIdAsync(int stateId);
}