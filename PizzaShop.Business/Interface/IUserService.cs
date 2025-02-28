using PizzaShop.Data.ViewModel;

namespace PizzaShop.Business.Interface;

public interface IUserService
{
    Task<int> ChangePasswordAsync(string jwtToken, string currentPassword, string newPassword);
    
}