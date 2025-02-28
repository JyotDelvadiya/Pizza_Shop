using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using PizzaShop.Business.Interface;
using PizzaShop.Data.Data;

namespace PizzaShop.Business.Services;
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    private readonly PizzaShopDbContext _context;

    public EmailService(IConfiguration configuration, PizzaShopDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(
            _configuration["SmtpSettings:SenderName"],
            _configuration["SmtpSettings:SenderEmail"]
        ));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("html") { Text = message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(
                _configuration["SmtpSettings:Server"],
                int.Parse(_configuration["SmtpSettings:Port"]),
                MailKit.Security.SecureSocketOptions.StartTls
            );
            await client.AuthenticateAsync(
                _configuration["SmtpSettings:Username"],
                _configuration["SmtpSettings:Password"]
            );
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }

    // Specific method for sending password reset email
    public async Task SendPasswordResetEmailAsync(string toEmail, string resetLink)
    {
        string subject = "Password Reset Request - PizzaShop";

        // HTML email template
        string messageBody = @$"
                <html>
                <body style='font-family: Arial, sans-serif; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                        <h2 style='color: #e31837;'>Password Reset Request</h2>
                        <p>Hello,</p>
                        <p>We received a request to reset your password for your PizzaShop account.</p>
                        <p>To reset your password, please click the button below:</p>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{resetLink}' 
                               style='background-color: #e31837; 
                                      color: white; 
                                      padding: 12px 25px; 
                                      text-decoration: none; 
                                      border-radius: 5px;
                                      display: inline-block;'>
                                Reset Password
                            </a>
                        </div>
                        <p>If you didn't request this password reset, you can safely ignore this email.</p>
                        <p>This link will expire in 30 minutes for security reasons.</p>
                        <br>
                        <p>Best regards,</p>
                        <p>The PizzaShop Team</p>
                    </div>
                </body>
                </html>";

        await SendEmailAsync(toEmail, subject, messageBody);
    }

    public async Task SendEmailPasswordAsync(string Email, string Password)
    {
        string subject = "New User Credentials - PizzaShop";

        // HTML email template
        string messageBody = @$"
                <!DOCTYPE html>
                <html>
                <body>
                    <div class='container mt-4'>
                        <div class='card'>
                            <div class='card-header bg-darkblue text-white'>
                                <h5 class='m-0'>PIZZASHOP</h5>
                            </div>
                            <div class='card-body'>
                                <h5 class='card-title'>Welcome to Pizza shop</h5>
                                <p class='card-text'>Please find the details below for login into your account.</p>
                                <div class='border border-info p-3 mb-3'>
                                    <h6>Login Details:</h6>
                                    <p><strong>Username:</strong> {Email}</p>
                                    <p><strong>Temporary Password:</strong> {Password}</p>
                                </div>
                                <p>If you encounter any issues or have any question, please do not hesitate to contact our support team.</p>
                            </div>
                        </div>
                    </div>
                </body>
                </html>";
        await SendEmailAsync(Email, subject, messageBody);
    }

    public async Task<bool> SendMailServiceAsync(string email)
    {
        var userExists = _context.Accounts.Any(u => u.Email == email);

        if (userExists)
        {
            // Generate a password reset token (you'll need to implement this)
            string token = Guid.NewGuid().ToString();

            // Create the reset link
            string resetLink = $"{_configuration["AppSettings:BaseUrl"]}/Authentication/ResetPassword?email={email}";

            // Send the email
            await SendPasswordResetEmailAsync(email, resetLink);

            // Redirect to success page or show success message
            return true;
        }
        return false;
    }
}