public interface IAccountService
{
    Task<bool> ResetPasswordAsync(string email, string newPassword);
    // Add other methods you need
}