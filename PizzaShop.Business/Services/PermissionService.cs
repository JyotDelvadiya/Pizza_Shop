// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using Org.BouncyCastle.Crypto.Engines;
// using PizzaShop.Data.Data;
// using PizzaShop.Data.Models;
// using PizzaShop.Data.ViewModel;
// using PizzaShop.Business.Interface;

// public class PermissionService : IPermissionService
// {
//     private readonly PizzaShopDbContext _context;

//     public PermissionService(PizzaShopDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<RolePermissionVM> GetRolePermissionsAsync(int roleId)
//     {
//         var role = await _context.Roles.FindAsync(roleId);
//         if (role == null) return null;

//         var permissions = await _context.Permissions.ToListAsync();
//         var assignedPermissions = await _context.Rolepermissions
//                                                 .Where(rp => rp.Roleid == roleId)
//                                                 .Select(rp => rp.Permissionid)
//                                                 .ToListAsync();

//         return new RolePermissionVM
//         {
//             RoleId = roleId,
//             RoleName = role.Rolename,
//             Permissions = permissions,
//             AssignedPermissionIds = assignedPermissions
//         };
//     }

//     public async Task AssignPermissionsToRoleAsync(int roleId, List<int> permissionIds)
//     {
//         var existingPermissions = _context.Rolepermissions.Where(rp => rp.Roleid == roleId);
//         _context.Rolepermissions.RemoveRange(existingPermissions);

//         var newRolePermissions = permissionIds.Select(pid => new Rolepermission
//         {
//             Roleid = roleId,
//             Permissionid = pid
//         });

//         await _context.Rolepermissions.AddRangeAsync(newRolePermissions);
//         await _context.SaveChangesAsync();
//     }
// }
