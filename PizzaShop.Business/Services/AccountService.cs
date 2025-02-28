using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;

public class AccountService : IAccountService
{
    private readonly PizzaShopDbContext _context;

    public AccountService(PizzaShopDbContext context)
    {
        _context = context;
    }
    public async Task<bool> ResetPasswordAsync(string email, string newPassword)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => 
            a.Email == email 
        );

        if (account == null)
        {
            return false;
        }

        // Hash the new password
        account.Password = HashPassword(newPassword);

        await _context.SaveChangesAsync();
        return true;
    }

    private string HashPassword(string password)
    {
        // Implement your password hashing logic here
        // For example, using BCrypt
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}