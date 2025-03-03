using Microsoft.EntityFrameworkCore;
using PizzaShop.Business.Interface;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task<bool> ResetPasswordAsync(string email, string newPassword)
    {
        var account = await _accountRepository.GetAccountByEmailAsync(email);

        if (account == null)
        {
            return false;
        }

        account.Password = HashPassword(newPassword);

        return await _accountRepository.UpdateAccountPasswordAsync(account);
    }
    private string HashPassword(string password)
    {
        // Implement your password hashing logic here
        // For example, using BCrypt
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}