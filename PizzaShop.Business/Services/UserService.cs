using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaShop.Business.Interface;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;
using PizzaShop.Data.ViewModel;

namespace PizzaShop.Business.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;
    private readonly IGenerateJwt _generateJWT;

    public UserService(IUserRepository userRepository, IEmailService emailService, IGenerateJwt generateJWT, ILogger<UserService> logger)
    {
        _logger = logger;
        _emailService = emailService;
        _userRepository = userRepository;
        _generateJWT = generateJWT;
    }
    
    public DashboardVM GetDashboardData(string jwtToken)
    {
        return _userRepository.GetDashboardData(jwtToken);
    }
    
    public Task<int> ChangePasswordAsync(string jwtToken, string currentPassword, string newPassword)
    {
        return _userRepository.ChangePasswordAsync(jwtToken, currentPassword, newPassword);
    }
    
    public Task<(bool Success, Dictionary<string, string> Errors)> UpdateProfileAsync(MyProfileVM profileVM, string jwtToken)
    {
        return _userRepository.UpdateProfileAsync(profileVM, jwtToken);
    }
    
    public Task<object> UploadProfileImageAsync(IFormFile profileImage, int accountId)
    {
        return _userRepository.UploadProfileImageAsync(profileImage, accountId);
    }
    
    public MyProfileVM GetUserProfile(string jwtToken)
    {
        return _userRepository.GetUserProfile(jwtToken);
    }

    public List<UserListVM> SearchUsers(string searchTerm, string jwtToken)
    {
        return _userRepository.SearchUsers(searchTerm, jwtToken);
    }
    
    public List<UserListVM> GetUserList(string jwtToken)
    {
        return _userRepository.GetUserList(jwtToken);
    }
    
    public Task<(bool Success, Dictionary<string, string> Errors)> AddUserAsync(AddUserVM addUserDetails)
    {
        return _userRepository.AddUserAsync(addUserDetails);
    }
    
    public EditUserVM GetEditUserForm(int userId)
    {
        return _userRepository.GetEditUserForm(userId);
    }
    
    public AddUserVM GetAddUserForm()
    {
        return _userRepository.GetAddUserForm();
    }
    
    public (bool Success, Dictionary<string, string> Errors) UpdateUser(EditUserVM editUserDetails)
    {
        return _userRepository.UpdateUser(editUserDetails);
    }
    
    public (bool Success, string Message) DeleteUser(string email)
    {
        return _userRepository.DeleteUser(email);
    }
}