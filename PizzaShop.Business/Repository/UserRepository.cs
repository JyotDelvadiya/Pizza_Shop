using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaShop.Business.Interface;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;
using PizzaShop.Data.ViewModel;

public class UserRepository : IUserRepository
{
    private readonly PizzaShopDbContext _context;
    private readonly ILogger<UserRepository> _logger;
    private readonly IEmailService _emailService;
    private readonly IGenerateJwt _generateJWT;

    public UserRepository(PizzaShopDbContext context, ILogger<UserRepository> logger, IEmailService emailService, IGenerateJwt generateJWT)
    {
        _context = context;
        _logger = logger;
        _emailService = emailService;
        _generateJWT = generateJWT;
    }
    public DashboardVM GetDashboardData(string jwtToken)
    {
        var accountId = _generateJWT.GetAccountidFromJwtToken(jwtToken);
        var accountDetails = _context.Accounts.FirstOrDefault(x => x.Accountid == accountId) ?? new();
        var userDetails = _context.Users.FirstOrDefault(x => x.Accountid == accountId) ?? new();

        return new DashboardVM
        {
            UserName = accountDetails.Username,
            ProfileImage = userDetails.Profileimage
        };
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

            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string fileExtension = Path.GetExtension(profileImage.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return new { success = false, message = "Only image files (.jpg, .jpeg, .png, .gif) are allowed" };
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Accountid == accountId);
            if (user == null)
            {
                return new { success = false, message = "User not found" };
            }

            string uniqueFileName = $"profile_{accountId}_{Guid.NewGuid()}{fileExtension}";
            string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadsFolder = Path.Combine(webRootPath, "images", "profiles");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            string relativeFilePath = $"/images/profiles/{uniqueFileName}";

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profileImage.CopyToAsync(fileStream);
            }

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
            return null;
        }

        var accountId = _generateJWT.GetAccountidFromJwtToken(jwtToken);
        if (accountId == null)
        {
            return null;
        }

        var accountDetails = _context.Accounts.FirstOrDefault(u => u.Accountid == accountId) ?? new();
        var userDetails = _context.Users.FirstOrDefault(x => x.Accountid == accountId) ?? new();
        var roleDetails = _context.Roles.FirstOrDefault(r => r.Roleid == userDetails.Roleid) ?? new();
        var countries = _context.Countries.ToList();
        var country = _context.Countries.FirstOrDefault(s => s.Countryid == userDetails.Country) ?? new();
        var state = _context.States.FirstOrDefault(s => s.Stateid == userDetails.State) ?? new();
        var city = _context.Cities.FirstOrDefault(c => c.Cityid == userDetails.City) ?? new();

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
        var query = GetUserListQuery(jwtToken);

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
        return GetUserListQuery(jwtToken).ToList();
    }

    private IQueryable<UserListVM> GetUserListQuery(string jwtToken)
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
            );
    }

    public async Task<(bool Success, Dictionary<string, string> Errors)> AddUserAsync(AddUserVM addUserDetails)
    {
        var errors = new Dictionary<string, string>();

        if (await _context.Accounts.AnyAsync(a => a.Username == addUserDetails.Username && !a.Isdeleted))
            errors.Add("Username", "This username is already taken.");

        if (await _context.Accounts.AnyAsync(a => a.Email == addUserDetails.Email && !a.Isdeleted))
            errors.Add("Email", "This email is already registered.");

        if (await _context.Users.AnyAsync(a => a.Phonenumber == addUserDetails.Phonenumber && !a.Isdeleted))
            errors.Add("Phonenumber", "This phone number is already registered.");

        if (errors.Count > 0)
            return (false, errors);

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

            await _emailService.SendEmailPasswordAsync(addUserDetails.Email, addUserDetails.Password);

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

            return (true, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user.");
            return (false, new Dictionary<string, string> { { "Error", "An error occurred. Try again." } });
        }
    }

    public EditUserVM GetEditUserForm(int userId)
    {
        var user = _context.Users.FirstOrDefault(x => x.Userid == userId);
        if (user == null) return null;

        var account = _context.Accounts.FirstOrDefault(x => x.Accountid == user.Accountid);
        var country = _context.Countries.FirstOrDefault(s => s.Countryid == user.Country);
        var state = _context.States.FirstOrDefault(s => s.Stateid == user.State);
        var city = _context.Cities.FirstOrDefault(c => c.Cityid == user.City);

        return new EditUserVM
        {
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Username = account?.Username,
            Email = account?.Email,
            Accountid = account?.Accountid ?? 0,
            Status = user.Status,
            Roleid = user.Roleid,
            Profileimage = user.Profileimage,
            Zipcode = user.Zipcode,
            Address = user.Address,
            Phonenumber = user.Phonenumber,
            Country = country?.Countryname,
            State = state?.Statename,
            City = city?.Cityname,
            Stateid = state?.Stateid ?? 0,
            Cityid = city?.Cityid ?? 0,
            Countries = _context.Countries.ToList()
        };
    }

    public AddUserVM GetAddUserForm()
    {
        return new AddUserVM { Countries = _context.Countries.ToList() };
    }

    public (bool Success, Dictionary<string, string> Errors) UpdateUser(EditUserVM editUserDetails)
    {
        var user = _context.Users.FirstOrDefault(x => x.Userid == editUserDetails.Userid);
        var account = _context.Accounts.FirstOrDefault(x => x.Accountid == user.Accountid);

        if (user == null || account == null)
            return (false, new Dictionary<string, string> { { "Error", "User not found." } });

        account.Username = editUserDetails.Username;
        account.Email = editUserDetails.Email;
        user.Firstname = editUserDetails.Firstname;
        user.Lastname = editUserDetails.Lastname;
        user.Status = editUserDetails.Status;
        user.Roleid = editUserDetails.Roleid;
        user.Phonenumber = editUserDetails.Phonenumber;
        user.Zipcode = editUserDetails.Zipcode;
        user.Address = editUserDetails.Address;

        _context.SaveChanges();
        return (true, null);
    }

    public (bool Success, string Message) DeleteUser(string email)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Email == email && !a.Isdeleted);
        if (account == null) return (false, "User not found.");

        var user = _context.Users.FirstOrDefault(u => u.Accountid == account.Accountid);
        if (user != null) user.Isdeleted = true;

        account.Isdeleted = true;
        _context.SaveChanges();

        return (true, "User deleted successfully.");
    }
}
