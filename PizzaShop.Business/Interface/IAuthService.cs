using PizzaShop.Data.ViewModel;

namespace PizzaShop.Business.Interface;

public interface IAuthService
{
    Task<(int status,string token)> Login (LoginVM user);
}