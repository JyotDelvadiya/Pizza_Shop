using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data.Data;
using PizzaShop.Data.Models;
using PizzaShop.Business.Interface;
using PizzaShop.Data.ViewModel;

public class RoleService : IRoleService
{
    private readonly PizzaShopDbContext _context;
    private readonly IRoleRepository _roleRepository;

    public RoleService(PizzaShopDbContext context, IRoleRepository RoleRepository)
    {
        _context = context;
        _roleRepository = RoleRepository;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<PermissionMatrixVM> GetPermissionMatrixForRoleAsync(int roleId)
    {
        // 1. Get the role
        var role = await _roleRepository.GetByIdAsync(roleId);

        // 2. Get all permissions
        var allPermissions = _roleRepository.GetAll();

        // 3. Get existing role permissions
        var rolePermissions = _roleRepository.GetByRoleId(roleId);

        var viewModel = new PermissionMatrixVM
        {
            RoleId = role.Roleid.ToString(),
            RoleName = role.Rolename,
        };

        // 4. Create permission items for each permission
        await foreach (var permission in allPermissions)
        {
            var rolePermission = rolePermissions.FirstOrDefault(rp => rp.Permissionid == permission.Permissionid);

            var permissionItem = new PermissionItemVM
            {
                PermissionId = permission.Permissionid,
                PermissionName = permission.Permissionname,
                // IsSelected = rolePermission != null, // Checked if there's a role permission entry
                CanView = rolePermission?.Canview ?? false,
                CanAddUpdate = rolePermission?.Canaddupdate ?? false,
                CanDelete = rolePermission?.Candelete ?? false
            };

            viewModel.Permissions.Add(permissionItem);
        }

        return viewModel;
    }

    public async Task<Role> GetRoleByIdAsync(int roleId)
    {
        return await _context.Roles.FindAsync(roleId) ?? new();
    }

    public async Task<bool> UpdatePermissionAsync(PermissionMatrixVM permissionMatrixVM){
        try
        {
            var role = await _context.Roles
                .Include(r => r.Rolepermissions)
                .FirstOrDefaultAsync(r => r.Rolename == permissionMatrixVM.RoleName);

            foreach (var permissionItem in permissionMatrixVM.Permissions)
            {
                var rolePermission = role.Rolepermissions.FirstOrDefault(rp => rp.Permissionid == permissionItem.PermissionId);
                if (rolePermission != null)
                {
                    // Update existing role permissions
                    rolePermission.Canview = permissionItem.CanView;
                    rolePermission.Canaddupdate = permissionItem.CanAddUpdate;
                    rolePermission.Candelete = permissionItem.CanDelete;
                }
                else
                {
                    // Add new permission mapping if it does not exist
                    role.Rolepermissions.Add(new Rolepermission
                    {
                        Roleid = role.Roleid,
                        Permissionid = permissionItem.PermissionId,
                        Canview = permissionItem.CanView,
                        Canaddupdate = permissionItem.CanAddUpdate,
                        Candelete = permissionItem.CanDelete
                    });
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    } 
}
