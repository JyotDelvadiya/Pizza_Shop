using PizzaShop.Data.ViewModel;

namespace PizzaShop.Business.Interface;

public interface IEmailService{
    public Task SendEmailAsync(string toEmail, string subject, string message);
    public Task SendPasswordResetEmailAsync(string toEmail, string resetLink);
    public Task SendEmailPasswordAsync(string Email, string Password);
    public Task<bool> SendMailServiceAsync(string email);
}