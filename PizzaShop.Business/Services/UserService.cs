using Microsoft.EntityFrameworkCore;
using PizzaShop.Business.Interface;
using PizzaShop.Data.Data;
using PizzaShop.Data.ViewModel;

namespace PizzaShop.Business.Services;

public class UserService : IUserService
{
    private readonly PizzaShopDbContext _context;
    private readonly IGenerateJwt _generateJWT;
    public UserService(PizzaShopDbContext context, IGenerateJwt generateJWT){
        _context = context;
        _generateJWT = generateJWT;
    }
    public async Task<int> ChangePasswordAsync(string jwtToken, string currentPassword, string newPassword)
    {
        var accountId = _generateJWT.GetAccountidFromJwtToken(jwtToken);
        var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Accountid == accountId);

        if (user == null)
        {
            return 0;
        }

        if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
        {
            return 1; 
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _context.SaveChangesAsync();

        return 2;
    }
}