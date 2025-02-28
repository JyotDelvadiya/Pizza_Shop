using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Data;
using PizzaShop.Models;
using BCrypt.Net;
using PizzaShop.Business.Services;
using PizzaShop.Business.Interface;
using PizzaShop.Data.ViewModel;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace PizzaShop.Controllers;

public class AuthenticationController : Controller
{
    private readonly PizzaShopDbContext _context;

    private readonly IEmailService _emailService;

    private readonly IAuthService _authService;

    private readonly IAccountService _accountService;

    private readonly IConfiguration _configuration;

    private readonly IGenerateJwt _generateJWT;


    public AuthenticationController(PizzaShopDbContext context, IEmailService emailService, IConfiguration configuration, IAccountService accountService, IGenerateJwt generateJWT, IAuthService authService)
    {
        _context = context;
        _emailService = emailService;
        _configuration = configuration;
        _accountService = accountService;
        _authService = authService;
        _generateJWT = generateJWT;
    }

    public IActionResult ResetPassword(string email, string token)
    {
        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction("Login", "Home");
        }
        var model = new ResetPasswordViewModel
        {
            Email = email,
            Token = token
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        // Verify token and update password
        var success = await _accountService.ResetPasswordAsync(
            model.Email,
            model.NewPassword
        );

        if (success)
        {
            TempData["SuccessMessage"] = "Your password has been reset successfully. Please login with your new password.";
            return RedirectToAction("Login", "Home");
        }

        ModelState.AddModelError("", "Invalid token or the token has expired.");
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendEmail(string email)
    {
        try
        {
            var success = _emailService.SendMailServiceAsync(email);

            if (success != null)
            {
                // Redirect to success page or show success message
                TempData["SuccessMessage"] = "Password reset instructions have been sent to your email.";
                return RedirectToAction("Login", "Home");
            }
            return Json(new { success = false, message = "No account found with this email address" });
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine(ex);
            TempData["ErrorMessage"] = "An error occurred while sending the reset email.";
            return RedirectToAction("ForgotPassword", "Authentication");
        }
    }

    public IActionResult ForgotPassword(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            ViewBag.Email = email;
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PassEmail(string email)
    {
        // Redirect to forgot password with the email
        return Json(new
        {
            success = true,
            redirectUrl = Url.Action("ForgotPassword", "Authentication", new { email = email })
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM user)
    {
        if (!ModelState.IsValid)
        {
            return View("~/Views/Authentication/Index.cshtml", user);
        }
        (int status, string token) = await _authService.Login(user);
        if (status == 0)
        {
            ModelState.AddModelError("Email", "Account not found");
            return View("~/Views/Authentication/Index.cshtml", user);
        }
        else if (status == 1)
        {
            ModelState.AddModelError("Password", "Invalid password");
            return View("~/Views/Authentication/Index.cshtml", user);
        }
        else
        {
            Response.Cookies.Append("JWT", token);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            if (user.Isrememberme != false)
            {
                Response.Cookies.Append("UserEmail", user.Email, cookieOptions);
                cookieOptions.Expires = DateTime.UtcNow.AddDays(30);
            }
            return RedirectToAction("Index", "Home");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new Data.ViewModel.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}