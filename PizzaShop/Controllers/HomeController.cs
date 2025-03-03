using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Business.Interface;
using PizzaShop.Data.ViewModel;

namespace PizzaShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGenerateJwt _generateJWT;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly ILocationService _locationService;
    public HomeController(ILogger<HomeController> logger, IGenerateJwt generateJWT, IUserService userService, IEmailService emailService, ILocationService locationService)
    {
        _logger = logger;
        _emailService = emailService;
        _generateJWT = generateJWT;
        _userService = userService;
        _locationService = locationService;
    }

     [HttpGet]
    public IActionResult Index()
    {
        var jwtToken = Request.Cookies["JWT"];
        var dashboardData = _userService.GetDashboardData(jwtToken);
        
        ViewBag.UserName = dashboardData.UserName;
        ViewBag.Profileimage = dashboardData.ProfileImage;
        
        return View();
    }

    // [Authorize(Roles = "Account Manager")]
    // public IActionResult Roles()
    // {
    //     var Roles = _context.Roles.Select(x => new RolesVM
    //     {
    //         Roleid = x.Roleid,
    //         Rolename = x.Rolename
    //     }).ToList();
    //     return View(Roles);
    // }

    // [Authorize(Roles = "Account Manager")]
    // [HttpPost]
    // public IActionResult EditPermissions(int RoleID)
    // {
    //     var Role = _context.Roles.FirstOrDefault(x => x.Roleid == RoleID);
    //     return View(Role);
    // }

    // [Authorize(Roles = "Account Manager")]
    // [HttpPost]
    // public IActionResult DeleteUser(string email)
    // {
    //     try
    //     {
    //         // Find the account by email (only get active accounts)
    //         var account = _context.Accounts.FirstOrDefault(a => a.Email == email && !a.Isdeleted);

    //         if (account == null)
    //         {
    //             return Json(new { success = false, message = "User not found." });
    //         }

    //         // Find the associated user
    //         var user = _context.Users.FirstOrDefault(u => u.Accountid == account.Accountid);

    //         if (user != null)
    //         {
    //             // Soft delete the user by setting IsDeleted flag
    //             user.Isdeleted = true;
    //         }

    //         // Soft delete the account
    //         account.Isdeleted = true;

    //         // Save changes to both entities
    //         _context.SaveChanges();

    //         return Json(new { success = true, message = "User deleted successfully." });
    //     }
    //     catch (Exception ex)
    //     {
    //         // Log the error
    //         Console.WriteLine(ex);
    //         return Json(new { success = false, message = "An error occurred while deleting the user: " + ex.Message });
    //     }
    // }

    [HttpPost]
    public IActionResult DeleteUser(string email)
    {
        var result = _userService.DeleteUser(email);
        return Json(result);
    }

    // [Authorize(Roles = "Account Manager")]
    // [HttpPost]
    // public IActionResult EditUserForm(EditUserVM editUserDetails)
    // {
    //     // Check if username already exists
    //     var existingUsername = _context.Accounts.FirstOrDefault(a => a.Username == editUserDetails.Username && a.Isdeleted != true && a.Accountid != editUserDetails.Accountid);
    //     if (existingUsername != null)
    //     {
    //         ModelState.AddModelError("Username", "This username is already taken. Please choose another.");
    //         // return new JsonResult ( new {success= false, message= "Email Already Exists! Try with Other Email!"}); 
    //     }

    //     // Check if email already exists
    //     var existingEmail = _context.Accounts.FirstOrDefault(a => a.Email == editUserDetails.Email && a.Isdeleted != true && a.Accountid != editUserDetails.Accountid);
    //     if (existingEmail != null)
    //     {
    //         ModelState.AddModelError("Email", "This email is already registered. Please use another email.");
    //     }

    //     // Check if Phone Number already exists
    //     var existingPhoneNumber = _context.Users.FirstOrDefault(a => a.Phonenumber == editUserDetails.Phonenumber && a.Isdeleted != true && a.Userid != editUserDetails.Userid);
    //     if (existingPhoneNumber != null)
    //     {
    //         ModelState.AddModelError("Phonenumber", "This Phone Number is already registered. Please use another Phone Number.");
    //     }
    //     // Get the existing user and account records
    //     var userDetails = _context.Users.FirstOrDefault(x => x.Userid == editUserDetails.Userid) ?? new();
    //     var accountDetails = _context.Accounts.FirstOrDefault(x => x.Accountid == userDetails.Accountid);
    //     if (ModelState.IsValid)
    //     {
    //         try
    //         {
    //             if (userDetails == null || accountDetails == null)
    //             {
    //                 TempData["ErrorMessage"] = "User not found.";
    //                 return RedirectToAction("UserList", "Home");
    //             }

    //             // Update account information
    //             accountDetails.Username = editUserDetails.Username;
    //             accountDetails.Email = editUserDetails.Email;

    //             // Update user information
    //             userDetails.Firstname = editUserDetails.Firstname;
    //             userDetails.Lastname = editUserDetails.Lastname;
    //             userDetails.Status = editUserDetails.Status;
    //             userDetails.Roleid = editUserDetails.Roleid;
    //             userDetails.Phonenumber = editUserDetails.Phonenumber;
    //             userDetails.Zipcode = editUserDetails.Zipcode;
    //             userDetails.Address = editUserDetails.Address;

    //             // Update profile image if provided
    //             if (!string.IsNullOrEmpty(editUserDetails.Profileimage))
    //             {
    //                 userDetails.Profileimage = editUserDetails.Profileimage;
    //             }

    //             // Update location information
    //             // Note: You might need to handle these differently depending on how your form submits these values
    //             if (!string.IsNullOrEmpty(editUserDetails.Country))
    //             {
    //                 var country = _context.Countries.FirstOrDefault(c => c.Countryid == Int32.Parse(editUserDetails.Country));
    //                 if (country != null)
    //                 {
    //                     userDetails.Country = country.Countryid;
    //                 }
    //             }

    //             userDetails.City = Int32.Parse(editUserDetails.City ?? string.Empty);
    //             userDetails.State = Int32.Parse(editUserDetails.State ?? string.Empty);

    //             // Save changes to database
    //             _context.Update(accountDetails);
    //             _context.Update(userDetails);
    //             _context.SaveChanges();

    //             TempData["SuccessMessage"] = "User information updated successfully.";
    //             return RedirectToAction("UserList", "Home");
    //         }
    //         catch (Exception ex)
    //         {
    //             // Log the error
    //             Console.WriteLine(ex);
    //             TempData["ErrorMessage"] = "An error occurred while updating user information.";

    //             // Reload countries for the form
    //             editUserDetails.Countries = _context.Countries.ToList();
    //             return View(editUserDetails);
    //         }
    //     }

    //     // If ModelState is invalid, reload the form with validation errors
    //     editUserDetails.Countries = _context.Countries.ToList();
    //     var PopulateCountry = _context.Countries.FirstOrDefault(s => s.Countryid == Int32.Parse(editUserDetails.Country)) ?? new();
    //     var PopulateState = _context.States.FirstOrDefault(s => s.Stateid == Int32.Parse(editUserDetails.State ?? string.Empty)) ?? new();
    //     var PopulateCity = _context.Cities.FirstOrDefault(c => c.Cityid == Int32.Parse(editUserDetails.City ?? string.Empty)) ?? new();
    //     editUserDetails.Country = PopulateCountry.Countryname;
    //     editUserDetails.State = PopulateState.Statename;
    //     editUserDetails.Stateid = PopulateState.Stateid;
    //     editUserDetails.City = PopulateCity.Cityname;
    //     editUserDetails.Cityid = PopulateCity.Cityid;
    //     editUserDetails.Profileimage = userDetails.Profileimage;
    //     return View(editUserDetails);
    // }

    [HttpPost]
    public IActionResult EditUserForm(EditUserVM editUserDetails)
    {
        var result = _userService.UpdateUser(editUserDetails);
        
        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
            return View(editUserDetails);
        }
        
        TempData["SuccessMessage"] = "User information updated successfully.";
        return RedirectToAction("UserList", "Home");
    }

    // [Authorize(Roles = "Account Manager")]
    // public IActionResult EditUserForm(int userId)
    // {
    //     EditUserVM EditUserDetails = new EditUserVM();
    //     var countries = _context.Countries.ToList();

    //     var UserDetails = _context.Users.FirstOrDefault(x => x.Userid == userId) ?? new();
    //     var AccountDetails = _context.Accounts.FirstOrDefault(x => x.Accountid == UserDetails.Accountid) ?? new();
    //     var country = _context.Countries.FirstOrDefault(s => s.Countryid == UserDetails.Country) ?? new();
    //     var states = _context.States.FirstOrDefault(s => s.Stateid == UserDetails.State) ?? new();
    //     var cities = _context.Cities.FirstOrDefault(c => c.Cityid == UserDetails.City) ?? new();

    //     EditUserDetails.Firstname = UserDetails.Firstname;
    //     EditUserDetails.Lastname = UserDetails.Lastname;
    //     EditUserDetails.Username = AccountDetails.Username;
    //     EditUserDetails.Email = AccountDetails.Email;
    //     EditUserDetails.Accountid = AccountDetails.Accountid;
    //     EditUserDetails.Status = UserDetails.Status;
    //     EditUserDetails.Roleid = UserDetails.Roleid;
    //     EditUserDetails.Profileimage = UserDetails.Profileimage;
    //     EditUserDetails.Zipcode = UserDetails.Zipcode;
    //     EditUserDetails.Address = UserDetails.Address;
    //     EditUserDetails.Phonenumber = UserDetails.Phonenumber;
    //     EditUserDetails.Country = country.Countryname;
    //     EditUserDetails.State = states.Statename;
    //     EditUserDetails.Stateid = states.Stateid;
    //     EditUserDetails.City = cities.Cityname;
    //     EditUserDetails.Cityid = cities.Cityid;
    //     EditUserDetails.Countries = countries;

    //     return View(EditUserDetails);
    // }

    [HttpGet]
    public IActionResult EditUserForm(int userId)
    {
        var editUserDetails = _userService.GetEditUserForm(userId);
        return View(editUserDetails);
    }

    // [Authorize(Roles = "Account Manager")]
    // public IActionResult AddUserForm()
    // {
    //     AddUserVM AddUserDetails = new AddUserVM();
    //     var countries = _context.Countries.ToList();

    //     AddUserDetails.Countries = countries;
    //     return View(AddUserDetails);
    // }

    [HttpGet]
    public IActionResult AddUserForm()
    {
        var addUserDetails = _userService.GetAddUserForm();
        return View(addUserDetails);
    }

    // [Authorize(Roles = "Account Manager")]
    // [HttpPost]
    // public async Task<IActionResult> AddUserForm(AddUserVM addUserDetails)
    // {
    //     // Check if username already exists
    //     var existingUsername = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == addUserDetails.Username && a.Isdeleted != true);
    //     if (existingUsername != null)
    //     {
    //         ModelState.AddModelError("Username", "This username is already taken. Please choose another.");
    //         // return new JsonResult ( new {success= false, message= "Email Already Exists! Try with Other Email!"}); 
    //     }

    //     // Check if email already exists
    //     var existingEmail = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == addUserDetails.Email && a.Isdeleted != true);
    //     if (existingEmail != null)
    //     {
    //         ModelState.AddModelError("Email", "This email is already registered. Please use another email.");
    //     }

    //     // Check if Phone Number already exists
    //     var existingPhoneNumber = await _context.Users.FirstOrDefaultAsync(a => a.Phonenumber == addUserDetails.Phonenumber && a.Isdeleted != true);
    //     if (existingPhoneNumber != null)
    //     {
    //         ModelState.AddModelError("Phonenumber", "This Phone Number is already registered. Please use another Phone Number.");
    //     }

    //     // Check model state validity
    //     if (!ModelState.IsValid)
    //     {
    //         // Re-populate dropdown data
    //         addUserDetails.Countries = await _context.Countries.ToListAsync();
    //         return View(addUserDetails);
    //     }

    //     try
    //     {
    //         // Create account
    //         var account = new Account
    //         {
    //             Username = addUserDetails.Username,
    //             Email = addUserDetails.Email,
    //             Password = BCrypt.Net.BCrypt.HashPassword(addUserDetails.Password)
    //         };

    //         _context.Add(account);
    //         await _context.SaveChangesAsync();

    //         // Create user
    //         var user = new User
    //         {
    //             Firstname = addUserDetails.Firstname,
    //             Lastname = addUserDetails.Lastname,
    //             Roleid = addUserDetails.Roleid,
    //             Country = Int32.Parse(addUserDetails.Country),
    //             State = Int32.Parse(addUserDetails.State ?? string.Empty),
    //             City = Int32.Parse(addUserDetails.City ?? string.Empty),
    //             Zipcode = addUserDetails.Zipcode,
    //             Address = addUserDetails.Address,
    //             Phonenumber = addUserDetails.Phonenumber,
    //             Accountid = account.Accountid
    //         };

    //         _context.Add(user);
    //         await _context.SaveChangesAsync();

    //         // Handle profile image upload if a file is provided
    //         if (addUserDetails.ProfileImageFile != null && addUserDetails.ProfileImageFile.Length > 0)
    //         {
    //             try
    //             {
    //                 await UploadProfileImage(addUserDetails.ProfileImageFile, account.Accountid);
    //             }
    //             catch (Exception ex)
    //             {
    //                 // Log error but continue with user creation
    //                 _logger.LogError(ex, "Error uploading profile image for user {Username}", addUserDetails.Username);
    //                 // We continue with user creation even if image upload fails
    //             }
    //         }

    //         // Send the email
    //         await _emailService.SendEmailPasswordAsync(addUserDetails.Email, addUserDetails.Password);

    //         // Redirect to success page with message
    //         TempData["SuccessMessage"] = "User created successfully. Credentials have been sent to User's email.";
    //         return RedirectToAction("UserList", "Home");
    //     }
    //     catch (Exception ex)
    //     {
    //         // Log the error
    //         _logger.LogError(ex, "Error creating user: {ErrorMessage}", ex.Message);

    //         // Add model error
    //         ModelState.AddModelError("", "An error occurred while creating the user. Please try again.");

    //         // Re-populate dropdown data
    //         addUserDetails.Countries = await _context.Countries.ToListAsync();
    //         return View(addUserDetails);
    //     }
    // }

    [HttpPost]
    public async Task<IActionResult> AddUserForm(AddUserVM addUserDetails)
    {
        var result = await _userService.AddUserAsync(addUserDetails);
        
        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
            return View(addUserDetails);
        }
        
        TempData["SuccessMessage"] = "User created successfully. Credentials have been sent to User's email.";
        return RedirectToAction("UserList", "Home");
    }

    // [Authorize(Roles = "Account Manager")]
    // public IActionResult UserList()
    // {
    //     var accountId = _generateJWT.GetAccountidFromJwtToken(Request.Cookies["JWT"]);
    //     var AccountDetails = _context.Accounts.FirstOrDefault(x => x.Accountid == accountId) ?? new();
    //     var UserDetails = _context.Users.FirstOrDefault(x => x.Accountid == accountId) ?? new();

    //     ViewBag.UserName = AccountDetails.Username;
    //     ViewBag.Profileimage = UserDetails.Profileimage;

    //     var userListDetails = _context.Users
    //     .Where(user => !user.Isdeleted) // Filter deleted users
    //     .Join(
    //         _context.Roles,
    //         user => user.Roleid,
    //         role => role.Roleid,
    //         (user, role) => new { user, role }
    //     )
    //     .Join(
    //         _context.Accounts.Where(account => !account.Isdeleted), // Filter deleted accounts
    //         combined => combined.user.Accountid,
    //         account => account.Accountid,
    //         (combined, account) => new UserListVM
    //         {
    //             Userid = combined.user.Userid,
    //             Firstname = combined.user.Firstname,
    //             Lastname = combined.user.Lastname,
    //             Email = account.Email,
    //             Phonenumber = combined.user.Phonenumber,
    //             RoleName = combined.role.Rolename,
    //             Status = combined.user.Status,
    //             Profileimage = combined.user.Profileimage
    //         }
    //     )
    //     .ToList();
    //     return View(userListDetails);
    // }

    [HttpGet]
    public IActionResult UserList()
    {
        var jwtToken = Request.Cookies["JWT"];
        var userListDetails = _userService.GetUserList(jwtToken);
        return View(userListDetails);
    }

    // [Authorize(Roles = "Account Manager")]
    // [HttpGet]
    // public JsonResult SearchUsers(string searchTerm)
    // {
    //     var accountId = _generateJWT.GetAccountidFromJwtToken(Request.Cookies["JWT"]);

    //     var query = _context.Users
    //         .Where(user => user.Isdeleted == false)
    //         .Join(
    //             _context.Roles,
    //             user => user.Roleid,
    //             role => role.Roleid,
    //             (user, role) => new { user, role }
    //         )
    //         .Join(
    //             _context.Accounts.Where(account => account.Isdeleted == false),
    //             combined => combined.user.Accountid,
    //             account => account.Accountid,
    //             (combined, account) => new UserListVM
    //             {
    //                 Userid = combined.user.Userid,
    //                 Isdeleted = combined.user.Isdeleted,
    //                 Firstname = combined.user.Firstname,
    //                 Lastname = combined.user.Lastname,
    //                 Email = account.Email,
    //                 Phonenumber = combined.user.Phonenumber,
    //                 RoleName = combined.role.Rolename,
    //                 Status = combined.user.Status,
    //                 Profileimage = combined.user.Profileimage
    //             }
    //         );
    //     if (!string.IsNullOrEmpty(searchTerm))
    //     {
    //         searchTerm = searchTerm.ToLower();
    //         query = query.Where(u =>
    //             u.Firstname.ToLower().Contains(searchTerm) ||
    //             u.Lastname.ToLower().Contains(searchTerm) ||
    //             u.Email.ToLower().Contains(searchTerm) ||
    //             (u.Phonenumber ?? string.Empty).Contains(searchTerm) ||
    //             (u.RoleName ?? string.Empty).ToLower().Contains(searchTerm)
    //         );
    //     }
    //     var userListDetails = query.ToList();
    //     return Json(userListDetails);
    // }

    [HttpGet]
    public JsonResult SearchUsers(string searchTerm)
    {
        var jwtToken = Request.Cookies["JWT"];
        var userListDetails = _userService.SearchUsers(searchTerm, jwtToken);
        return Json(userListDetails);
    }

   [HttpGet]
    public async Task<IActionResult> GetStatesByCountryId(int countryId)
    {
        var states = await _locationService.GetStatesByCountryIdAsync(countryId);
        return Ok(states);
    }

    [HttpGet]
    public async Task<IActionResult> GetCitiesByStateId(int stateId)
    {
        var cities = await _locationService.GetCitiesByStateIdAsync(stateId);
        return Ok(cities);
    }

    // [Authorize(Roles = "Account Manager")]
    // public IActionResult MyProfile()
    // {
    //     var accountId = _generateJWT.GetAccountidFromJwtToken(Request.Cookies["JWT"]);

    //     MyProfileVM ProfileDetails = new MyProfileVM();
    //     var AccountDetails = _context.Accounts.FirstOrDefault(u => u.Accountid == accountId) ?? new();
    //     var userdetail = _context.Users.FirstOrDefault(x => x.Accountid == accountId) ?? new();
    //     var RoleDetails = _context.Roles.FirstOrDefault(r => r.Roleid == userdetail.Roleid) ?? new();
    //     var countries = _context.Countries.ToList();
    //     var country = _context.Countries.FirstOrDefault(s => s.Countryid == userdetail.Country) ?? new();
    //     var states = _context.States.FirstOrDefault(s => s.Stateid == userdetail.State) ?? new();
    //     var cities = _context.Cities.FirstOrDefault(c => c.Cityid == userdetail.City) ?? new();

    //     ProfileDetails.Firstname = userdetail.Firstname;
    //     ProfileDetails.Lastname = userdetail.Lastname;
    //     ProfileDetails.Username = AccountDetails.Username;
    //     ProfileDetails.Phonenumber = userdetail.Phonenumber;
    //     ProfileDetails.Address = userdetail.Address;
    //     ProfileDetails.Zipcode = userdetail.Zipcode;
    //     ProfileDetails.Countries = countries;
    //     ProfileDetails.Country = country.Countryname;
    //     ProfileDetails.State = states.Statename;
    //     ProfileDetails.Stateid = states.Stateid;
    //     ProfileDetails.City = cities.Cityname;
    //     ProfileDetails.Cityid = cities.Cityid;
    //     ProfileDetails.Email = AccountDetails.Email;
    //     ProfileDetails.RoleName = RoleDetails.Rolename;
    //     ProfileDetails.Profileimage = userdetail.Profileimage;
    //     ProfileDetails.Accountid = accountId;

    //     return View(ProfileDetails);
    // }

    public IActionResult MyProfile()
    {
        var jwtToken = Request.Cookies["JWT"];
        var profileDetails = _userService.GetUserProfile(jwtToken);

        if (profileDetails == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect if JWT is missing/invalid
        }

        return View(profileDetails);
    }

    // [Authorize(Roles = "Account Manager")]
    // [HttpPost]
    // public async Task<IActionResult> UploadProfileImage(IFormFile profileImage, int accountId)
    // {
    //     try
    //     {
    //         if (profileImage == null || profileImage.Length == 0)
    //         {
    //             return Json(new { success = false, message = "No file selected" });
    //         }

    //         // Validate file type
    //         string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    //         string fileExtension = Path.GetExtension(profileImage.FileName).ToLowerInvariant();

    //         if (!allowedExtensions.Contains(fileExtension))
    //         {
    //             return Json(new { success = false, message = "Only image files (.jpg, .jpeg, .png, .gif) are allowed" });
    //         }

    //         // Get the current user's account ID
    //         // var accountId = _generateJWT.GetAccountidFromJwtToken(Request.Cookies["JWT"]);
    //         if (accountId == 0)
    //         {
    //             return Json(new { success = false, message = "User not authenticated" });
    //         }

    //         // Find the user
    //         var user = _context.Users.FirstOrDefault(x => x.Accountid == accountId);
    //         if (user == null)
    //         {
    //             return Json(new { success = false, message = "User not found" });
    //         }

    //         // Generate unique filename
    //         string uniqueFileName = $"profile_{accountId}_{Guid.NewGuid().ToString()}{fileExtension}";

    //         // Get the path to wwwroot/images/profiles
    //         string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    //         string uploadsFolder = Path.Combine(webRootPath, "images", "profiles");

    //         // Create directory if it doesn't exist
    //         if (!Directory.Exists(uploadsFolder))
    //         {
    //             Directory.CreateDirectory(uploadsFolder);
    //         }

    //         string filePath = Path.Combine(uploadsFolder, uniqueFileName);
    //         string relativeFilePath = $"/images/profiles/{uniqueFileName}";

    //         // Save the file
    //         using (var fileStream = new FileStream(filePath, FileMode.Create))
    //         {
    //             await profileImage.CopyToAsync(fileStream);
    //         }

    //         // Delete old profile image if exists
    //         if (!string.IsNullOrEmpty(user.Profileimage) && user.Profileimage != "/images/Default_pfp.svg.png")
    //         {
    //             string oldImagePath = Path.Combine(webRootPath, user.Profileimage.TrimStart('/'));
    //             if (System.IO.File.Exists(oldImagePath))
    //             {
    //                 try
    //                 {
    //                     System.IO.File.Delete(oldImagePath);
    //                 }
    //                 catch (Exception ex)
    //                 {
    //                     _logger.LogError($"Failed to delete old profile image: {ex.Message}");
    //                 }
    //             }
    //         }

    //         // Update the user's profile image path in the database
    //         user.Profileimage = relativeFilePath;
    //         _context.SaveChanges();

    //         return Json(new { success = true, filePath = relativeFilePath, message = "Profile Image Changed Successfully " });
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError($"Error uploading profile image: {ex.Message}");
    //         return Json(new { success = false, message = "Error uploading file: " + ex.Message });
    //     }
    // }

    [Authorize(Roles = "Account Manager")]
    [HttpPost]
    public async Task<IActionResult> UploadProfileImage(IFormFile profileImage, int accountId)
    {
        var result = await _userService.UploadProfileImageAsync(profileImage, accountId);

        return Json(result);
    }

    // [Authorize(Roles = "Account Manager")]
    // [HttpPost]
    // public IActionResult MyProfile(MyProfileVM myProfileVM)
    // {
    //     var accountId = _generateJWT.GetAccountidFromJwtToken(Request.Cookies["JWT"]);

    //     var AccountDetails = _context.Accounts.FirstOrDefault(u => u.Accountid == accountId) ?? new();
    //     var userdetail = _context.Users.FirstOrDefault(x => x.Accountid == accountId) ?? new();
    //     var RoleDetails = _context.Roles.FirstOrDefault(r => r.Roleid == userdetail.Roleid) ?? new();

    //     // Check if Phone Number already exists
    //     var existingPhoneNumber = _context.Users.FirstOrDefault(a => a.Phonenumber == myProfileVM.Phonenumber && a.Isdeleted != true && a.Accountid != accountId);
    //     if (existingPhoneNumber != null)
    //     {
    //         ModelState.AddModelError("Phonenumber", "This Phone Number is already registered. Please use another Phone Number.");
    //     }

    //     // Check if username already exists
    //     var existingUsername = _context.Accounts.FirstOrDefault(a => a.Username == myProfileVM.Username && a.Isdeleted != true && a.Accountid != accountId);
    //     if (existingUsername != null)
    //     {
    //         ModelState.AddModelError("Username", "This username is already taken. Please choose another.");
    //         // return new JsonResult ( new {success= false, message= "Email Already Exists! Try with Other Email!"}); 
    //     }
    //     if (ModelState.IsValid)
    //     {
    //         try
    //         {
    //             if (AccountDetails == null)
    //             {
    //                 return RedirectToAction("MyProfile", "Home");
    //             }
    //             userdetail.Firstname = myProfileVM.Firstname;
    //             userdetail.Lastname = myProfileVM.Lastname;
    //             AccountDetails.Username = myProfileVM.Username;
    //             userdetail.Phonenumber = myProfileVM.Phonenumber;
    //             userdetail.City = Int32.Parse(myProfileVM.City ?? string.Empty);
    //             userdetail.State = Int32.Parse(myProfileVM.State ?? string.Empty);
    //             userdetail.Country = Int32.Parse(myProfileVM.Country);
    //             userdetail.Address = myProfileVM.Address;
    //             userdetail.Zipcode = myProfileVM.Zipcode;

    //             _context.SaveChanges();

    //             return RedirectToAction("MyProfile", "Home");
    //         }
    //         catch (Exception ex)
    //         {
    //             // Log the error
    //             Console.WriteLine(ex);
    //             TempData["ErrorMessage"] = "An error occurred while updating Profile information.";

    //             // Reload countries for the form
    //             myProfileVM.Countries = _context.Countries.ToList();
    //             return View(myProfileVM);
    //         }
    //     }
    //     myProfileVM.Countries = _context.Countries.ToList();
    //     var PopulateCountry = _context.Countries.FirstOrDefault(s => s.Countryid == Int32.Parse(myProfileVM.Country)) ?? new();
    //     var PopulateState = _context.States.FirstOrDefault(s => s.Stateid == Int32.Parse(myProfileVM.State ?? string.Empty)) ?? new();
    //     var PopulateCity = _context.Cities.FirstOrDefault(c => c.Cityid == Int32.Parse(myProfileVM.City ?? string.Empty)) ?? new();
    //     myProfileVM.Country = PopulateCountry.Countryname;
    //     myProfileVM.State = PopulateState.Statename;
    //     myProfileVM.Stateid = PopulateState.Stateid;
    //     myProfileVM.City = PopulateCity.Cityname;
    //     myProfileVM.Cityid = PopulateCity.Cityid;
    //     myProfileVM.Profileimage = userdetail.Profileimage;
    //     myProfileVM.Email = AccountDetails.Email;
    //     myProfileVM.RoleName = RoleDetails.Rolename;

    //     return View(myProfileVM);
    // }

    [HttpPost]
    public async Task<IActionResult> MyProfile(MyProfileVM myProfileVM)
    {
        var result = await _userService.UpdateProfileAsync(myProfileVM, Request.Cookies["JWT"]);

        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
            return View(myProfileVM);
        }

        return RedirectToAction("MyProfile", "Home");
    }

    [Authorize(Roles = "Account Manager")]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [Authorize(Roles = "Account Manager")]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordVM changePassword)
    {
        if (!ModelState.IsValid)
        {
            if (Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
            {
                return View(changePassword);
            }
            return Json(new { success = false, message = "Please correct the validation errors." });
        }

        string jwtToken = Request.Cookies["JWT"];
        int result = await _userService.ChangePasswordAsync(jwtToken, changePassword.CurrentPassword, changePassword.NewPassword);

        switch (result)
        {
            case 0:
                return Json(new { success = false, message = "User not found." });
            case 1:
                ModelState.AddModelError("CurrentPassword", "Current password is incorrect.");
                return Json(new { success = false, message = "Current password is incorrect." });
            case 2:
                if (Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
                {
                    return Json(new { success = true, message = "Password changed successfully!" });
                }
                return RedirectToAction("Index", "Home");
        }

        if (Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
        {
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }
        return Json(new { success = false, message = "Please correct the validation errors." });
    }

    public IActionResult Login()
    {
        LoginVM user = new LoginVM();
        if (Request.Cookies.ContainsKey("UserEmail"))
        {
            return View("./Views/Home/Index.cshtml");
        }
        return View("./Views/Authentication/Index.cshtml", user);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}