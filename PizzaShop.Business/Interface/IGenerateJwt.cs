using PizzaShop.Data.Models;

namespace PizzaShop.Business.Interface;

public interface IGenerateJwt
{
    string GenerateJwtToken(Account user,string role, TimeSpan? expirationTime = null);

    int GetAccountidFromJwtToken(string token);
}
