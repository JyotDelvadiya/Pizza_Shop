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
    private readonly PizzaShopDbContext _context;
    private readonly IEmailService _emailService;
    private readonly IGenerateJwt _generateJWT;

    public UserService(PizzaShopDbContext context, IEmailService emailService, IGenerateJwt generateJWT, ILogger<UserService> logger)
    {
        _logger = logger;
        _context = context;
        _emailService = emailService;
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
    public async Task<(bool Success, Dictionary<string, string> Errors)> UpdateProfileAsync(MyProfileVM profileVM, string jwtToken)
    {
        var errors = new Dictionary<string, string>();
        var accountId = _generateJWT.GetAccountidFromJwtToken(jwtToken);

        var existingPhone = await _context.Users.FirstOrDefaultAsync(u => u.Phonenumber == profileVM.Phonenumber && u.Accountid != accountId && !u.Isdeleted);
        if (existingPhone != null)
        {
            errors.Add("Phonenumber", "This Phone Number is already registered. Please use another Phone Number.");
        }

        var existingUsername = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == profileVM.Username && a.Accountid != accountId && !a.Isdeleted);
        if (existingUsername != null)
        {
            errors.Add("Username", "This username is already taken. Please choose another.");
        }

        if (errors.Count > 0)
        {
            return (false, errors);
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Accountid == accountId);
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Accountid == accountId);

        if (user == null || account == null)
        {
            return (false, new Dictionary<string, string> { { "Error", "User or Account not found." } });
        }

        user.Firstname = profileVM.Firstname;
        user.Lastname = profileVM.Lastname;
        account.Username = profileVM.Username;
        user.Phonenumber = profileVM.Phonenumber;
        user.City = int.Parse(profileVM.City ?? "0");
        user.State = int.Parse(profileVM.State ?? "0");
        user.Country = int.Parse(profileVM.Country);
        user.Address = profileVM.Address;
        user.Zipcode = profileVM.Zipcode;

        await _context.SaveChangesAsync();

        return (true, null);
    }

    public async Task<object> UploadProfileImageAsync(IFormFile profileImage, int accountId)
    {
        try
        {
            if (profileImage == null || profileImage.Length == 0)
            {
                return new { success = false, message = "No file selected" };
            }

            // Validate file type
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string fileExtension = Path.GetExtension(profileImage.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return new { success = false, message = "Only image files (.jpg, .jpeg, .png, .gif) are allowed" };
            }

            // Find the user
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Accountid == accountId);
            if (user == null)
            {
                return new { success = false, message = "User not found" };
            }

            // Generate unique filename
            string uniqueFileName = $"profile_{accountId}_{Guid.NewGuid()}{fileExtension}";

            // Define paths
            string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadsFolder = Path.Combine(webRootPath, "images", "profiles");

            // Ensure directory exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            string relativeFilePath = $"/images/profiles/{uniqueFileName}";

            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profileImage.CopyToAsync(fileStream);
            }

            // Delete old profile image if exists
            if (!string.IsNullOrEmpty(user.Profileimage) && user.Profileimage != "/images/Default_pfp.svg.png")
            {
                string oldImagePath = Path.Combine(webRootPath, user.Profileimage.TrimStart('/'));
                if (File.Exists(oldImagePath))
                {
                    try
                    {
                        File.Delete(oldImagePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Failed to delete old profile image: {ex.Message}");
                    }
                }
            }

            // Update database
            user.Profileimage = relativeFilePath;
            await _context.SaveChangesAsync();

            return new { success = true, filePath = relativeFilePath, message = "Profile Image Changed Successfully" };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error uploading profile image: {ex.Message}");
            return new { success = false, message = "Error uploading file: " + ex.Message };
        }
    }

    public MyProfileVM GetUserProfile(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken))
        {
            return null; // Handle missing JWT case
        }

        var accountId = _generateJWT.GetAccountidFromJwtToken(jwtToken);
        if (accountId == null)
        {
            return null; // Handle invalid JWT case
        }

        // Fetch database records separately
        var accountDetails = _context.Accounts.FirstOrDefault(u => u.Accountid == accountId) ?? new();
        var userDetails = _context.Users.FirstOrDefault(x => x.Accountid == accountId) ?? new();
        var roleDetails = _context.Roles.FirstOrDefault(r => r.Roleid == userDetails.Roleid) ?? new();
        var countries = _context.Countries.ToList();
        var country = _context.Countries.FirstOrDefault(s => s.Countryid == userDetails.Country) ?? new();
        var state = _context.States.FirstOrDefault(s => s.Stateid == userDetails.State) ?? new();
        var city = _context.Cities.FirstOrDefault(c => c.Cityid == userDetails.City) ?? new();

        // Assign values safely
        return new MyProfileVM
        {
            Firstname = userDetails.Firstname ?? string.Empty,
            Lastname = userDetails.Lastname ?? string.Empty,
            Username = accountDetails.Username ?? string.Empty,
            Phonenumber = userDetails.Phonenumber ?? string.Empty,
            Address = userDetails.Address ?? string.Empty,
            Zipcode = userDetails.Zipcode ?? string.Empty,
            Countries = countries,
            Country = country?.Countryname ?? "N/A",
            State = state?.Statename ?? "N/A",
            Stateid = state?.Stateid ?? 0,
            City = city?.Cityname ?? "N/A",
            Cityid = city?.Cityid ?? 0,
            Email = accountDetails.Email ?? string.Empty,
            RoleName = roleDetails?.Rolename ?? "N/A",
            Profileimage = userDetails.Profileimage ?? "/images/Default_pfp.svg.png",
            Accountid = accountId
        };
    }

    public List<UserListVM> SearchUsers(string searchTerm, string jwtToken)
    {
        var accountId = _generateJWT.GetAccountidFromJwtToken(jwtToken);

        var query = _context.Users
            .Where(user => user.Isdeleted == false)
            .Join(
                _context.Roles,
                user => user.Roleid,
                role => role.Roleid,
                (user, role) => new { user, role }
            )
            .Join(
                _context.Accounts.Where(account => account.Isdeleted == false),
                combined => combined.user.Accountid,
                account => account.Accountid,
                (combined, account) => new UserListVM
                {
                    Userid = combined.user.Userid,
                    Isdeleted = combined.user.Isdeleted,
                    Firstname = combined.user.Firstname,
                    Lastname = combined.user.Lastname,
                    Email = account.Email,
                    Phonenumber = combined.user.Phonenumber,
                    RoleName = combined.role.Rolename,
                    Status = combined.user.Status,
                    Profileimage = combined.user.Profileimage
                }
            );

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(u =>
                u.Firstname.ToLower().Contains(searchTerm) ||
                u.Lastname.ToLower().Contains(searchTerm) ||
                u.Email.ToLower().Contains(searchTerm) ||
                (u.Phonenumber ?? string.Empty).Contains(searchTerm) ||
                (u.RoleName ?? string.Empty).ToLower().Contains(searchTerm)
            );
        }

        return query.ToList();
    }

    public List<UserListVM> GetUserList(string jwtToken)
    {
        var accountId = _generateJWT.GetAccountidFromJwtToken(jwtToken);
        return _context.Users
            .Where(user => !user.Isdeleted)
            .Join(
                _context.Roles,
                user => user.Roleid,
                role => role.Roleid,
                (user, role) => new { user, role }
            )
            .Join(
                _context.Accounts.Where(account => !account.Isdeleted),
                combined => combined.user.Accountid,
                account => account.Accountid,
                (combined, account) => new UserListVM
                {
                    Userid = combined.user.Userid,
                    Firstname = combined.user.Firstname,
                    Lastname = combined.user.Lastname,
                    Email = account.Email,
                    Phonenumber = combined.user.Phonenumber,
                    RoleName = combined.role.Rolename,
                    Status = combined.user.Status,
                    Profileimage = combined.user.Profileimage
                }
            )
            .ToList();
    }

    public async Task<(bool Success, Dictionary<string, string> Errors)> AddUserAsync(AddUserVM addUserDetails)
    {
        var errors = new Dictionary<string, string>();
        
        var existingUsername = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == addUserDetails.Username && a.Isdeleted != true);
        if (existingUsername != null)
        {
            errors.Add("Username", "This username is already taken. Please choose another.");
        }

        var existingEmail = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == addUserDetails.Email && a.Isdeleted != true);
        if (existingEmail != null)
        {
            errors.Add("Email", "This email is already registered. Please use another email.");
        }

        var existingPhoneNumber = await _context.Users.FirstOrDefaultAsync(a => a.Phonenumber == addUserDetails.Phonenumber && a.Isdeleted != true);
        if (existingPhoneNumber != null)
        {
            errors.Add("Phonenumber", "This Phone Number is already registered. Please use another Phone Number.");
        }

        if (errors.Count > 0)
        {
            return (false, errors);
        }

        try
        {
            var account = new Account
            {
                Username = addUserDetails.Username,
                Email = addUserDetails.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(addUserDetails.Password)
            };

            _context.Add(account);
            await _context.SaveChangesAsync();

            var user = new User
            {
                Firstname = addUserDetails.Firstname,
                Lastname = addUserDetails.Lastname,
                Roleid = addUserDetails.Roleid,
                Country = int.Parse(addUserDetails.Country),
                State = int.Parse(addUserDetails.State ?? string.Empty),
                City = int.Parse(addUserDetails.City ?? string.Empty),
                Zipcode = addUserDetails.Zipcode,
                Address = addUserDetails.Address,
                Phonenumber = addUserDetails.Phonenumber,
                Accountid = account.Accountid
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            if (addUserDetails.ProfileImageFile != null && addUserDetails.ProfileImageFile.Length > 0)
            {
                try
                {
                    await UploadProfileImageAsync(addUserDetails.ProfileImageFile, account.Accountid);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading profile image for user {Username}", addUserDetails.Username);
                }
            }

            await _emailService.SendEmailPasswordAsync(addUserDetails.Email, addUserDetails.Password);
            return (true, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user: {ErrorMessage}", ex.Message);
            return (false, new Dictionary<string, string> { { "Error", "An error occurred while creating the user. Please try again." } });
        }
    }

    public EditUserVM GetEditUserForm(int userId)
    {
        var countries = _context.Countries.ToList();
        var userDetails = _context.Users.FirstOrDefault(x => x.Userid == userId) ?? new();
        var accountDetails = _context.Accounts.FirstOrDefault(x => x.Accountid == userDetails.Accountid) ?? new();
        var country = _context.Countries.FirstOrDefault(s => s.Countryid == userDetails.Country) ?? new();
        var state = _context.States.FirstOrDefault(s => s.Stateid == userDetails.State) ?? new();
        var city = _context.Cities.FirstOrDefault(c => c.Cityid == userDetails.City) ?? new();

        return new EditUserVM
        {
            Firstname = userDetails.Firstname,
            Lastname = userDetails.Lastname,
            Username = accountDetails.Username,
            Email = accountDetails.Email,
            Accountid = accountDetails.Accountid,
            Status = userDetails.Status,
            Roleid = userDetails.Roleid,
            Profileimage = userDetails.Profileimage,
            Zipcode = userDetails.Zipcode,
            Address = userDetails.Address,
            Phonenumber = userDetails.Phonenumber,
            Country = country.Countryname,
            State = state.Statename,
            Stateid = state.Stateid,
            City = city.Cityname,
            Cityid = city.Cityid,
            Countries = countries
        };
    }

    public AddUserVM GetAddUserForm()
    {
        return new AddUserVM
        {
            Countries = _context.Countries.ToList()
        };
    }

    public (bool Success, Dictionary<string, string> Errors) UpdateUser(EditUserVM editUserDetails)
    {
        var errors = new Dictionary<string, string>();
        
        var existingUsername = _context.Accounts.FirstOrDefault(a => a.Username == editUserDetails.Username && a.Isdeleted != true && a.Accountid != editUserDetails.Accountid);
        if (existingUsername != null)
        {
            errors.Add("Username", "This username is already taken. Please choose another.");
        }

        var existingEmail = _context.Accounts.FirstOrDefault(a => a.Email == editUserDetails.Email && a.Isdeleted != true && a.Accountid != editUserDetails.Accountid);
        if (existingEmail != null)
        {
            errors.Add("Email", "This email is already registered. Please use another email.");
        }

        var existingPhoneNumber = _context.Users.FirstOrDefault(a => a.Phonenumber == editUserDetails.Phonenumber && a.Isdeleted != true && a.Userid != editUserDetails.Userid);
        if (existingPhoneNumber != null)
        {
            errors.Add("Phonenumber", "This Phone Number is already registered. Please use another Phone Number.");
        }

        if (errors.Count > 0)
        {
            return (false, errors);
        }

        var userDetails = _context.Users.FirstOrDefault(x => x.Userid == editUserDetails.Userid);
        var accountDetails = _context.Accounts.FirstOrDefault(x => x.Accountid == userDetails.Accountid);

        if (userDetails == null || accountDetails == null)
        {
            return (false, new Dictionary<string, string> { { "Error", "User not found." } });
        }

        accountDetails.Username = editUserDetails.Username;
        accountDetails.Email = editUserDetails.Email;
        userDetails.Firstname = editUserDetails.Firstname;
        userDetails.Lastname = editUserDetails.Lastname;
        userDetails.Status = editUserDetails.Status;
        userDetails.Roleid = editUserDetails.Roleid;
        userDetails.Phonenumber = editUserDetails.Phonenumber;
        userDetails.Zipcode = editUserDetails.Zipcode;
        userDetails.Address = editUserDetails.Address;

        _context.SaveChanges();
        return (true, null);
    }

    public (bool Success, string Message) DeleteUser(string email)
    {
        try
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Email == email && !a.Isdeleted);
            if (account == null)
            {
                return (false, "User not found.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Accountid == account.Accountid);
            if (user != null)
            {
                user.Isdeleted = true;
            }
            
            account.Isdeleted = true;
            _context.SaveChanges();

            return (true, "User deleted successfully.");
        }
        catch (Exception ex)
        {
            return (false, "An error occurred while deleting the user: " + ex.Message);
        }
    }
}