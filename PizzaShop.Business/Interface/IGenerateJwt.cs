using PizzaShop.Data.Models;

namespace PizzaShop.Business.Interface;

public interface IGenerateJwt
{
    string GenerateJwtToken(Account user,string role);

    int GetAccountidFromJwtToken(string token);
}
