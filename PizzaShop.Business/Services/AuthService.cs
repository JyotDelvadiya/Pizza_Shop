using Microsoft.EntityFrameworkCore;
using PizzaShop.Business.Interface;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;
using PizzaShop.Data.ViewModel;

namespace PizzaShop.Business.Services;

public class AuthService : IAuthService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IGenerateJwt _generateJWT;
    public AuthService(IAccountRepository accountRepository, IGenerateJwt generateJWT)
    {
        _accountRepository = accountRepository;
        _generateJWT = generateJWT;
    }

    /// <summary>
    /// Login User: Check if user exists with this email, then check password
    /// Returns:
    /// (0, empty) - Email not exist
    /// (1, empty) - Wrong Password
    /// (2, token) - Success with JWT token
    /// </summary>
    /// <param name="user">Login view model</param>
    /// <returns>Tuple with status code and token</returns>
    public async Task<(int status, string token)> Login(LoginVM user, TimeSpan? tokenExpiration = null)
    {
        Account? accountDetails = await _accountRepository.GetAccountByEmailAsync(user.Email);

        if (accountDetails == null)
        {
            return (0, string.Empty);
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Password, accountDetails.Password);

        if (!isPasswordValid)
        {
            return (1, string.Empty);
        }

        User? userDetails = await _accountRepository.GetUserByAccountIdAsync(accountDetails.Accountid);
        string roleName = string.Empty;

        if (userDetails != null)
        {
            Role? role = await _accountRepository.GetRoleByIdAsync(userDetails.Roleid);
            roleName = role?.Rolename ?? string.Empty;
        }

        string token = _generateJWT.GenerateJwtToken(accountDetails, roleName, tokenExpiration) ?? string.Empty;
        accountDetails.Isrememberme = user.Isrememberme;

        await _accountRepository.SaveChangesAsync();
        return (2, token);
    }
}