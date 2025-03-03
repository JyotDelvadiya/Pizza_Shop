using Microsoft.AspNetCore.Http;
using PizzaShop.Data.ViewModel;

public interface IUserRepository
{
    List<UserListVM> SearchUsers(string searchTerm, string jwtToken);
    List<UserListVM> GetUserList(string jwtToken);
    Task<(bool Success, Dictionary<string, string> Errors)> AddUserAsync(AddUserVM addUserDetails);
    EditUserVM GetEditUserForm(int userId);
    AddUserVM GetAddUserForm();
    (bool Success, Dictionary<string, string> Errors) UpdateUser(EditUserVM editUserDetails);
    (bool Success, string Message) DeleteUser(string email);
    DashboardVM GetDashboardData(string jwtToken);
    Task<int> ChangePasswordAsync(string jwtToken, string currentPassword, string newPassword);
    Task<(bool Success, Dictionary<string, string> Errors)> UpdateProfileAsync(MyProfileVM profileVM, string jwtToken);
    Task<object> UploadProfileImageAsync(IFormFile profileImage, int accountId);
    MyProfileVM GetUserProfile(string jwtToken);
}