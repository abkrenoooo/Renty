using DAL.Enum;
using Microsoft.AspNetCore.Identity;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Seeds
{
    public static class DefaultUsers
    {

        #region Server
        public static async Task SeedServerAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "Server",
                FullName = "Server",
                Email = "Server@domain.com",
                EmailConfirmed = true,
                IsActive = true,
                
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Server@123");
                await userManager.AddToRoleAsync(defaultUser, Roles.Server.ToString());
            }
            await roleManger.SeedServerClaims();
        }
        private static async Task SeedServerClaims(this RoleManager<IdentityRole> roleManager)
        {
            var server = await roleManager.FindByNameAsync(Roles.Server.ToString());
            await roleManager.AddServerPermissionClaims(server);
        }
        public static async Task AddServerPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GenerateServerPermissions();

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
        #endregion

        #region Super Admin
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "SuperAdmin",
                FullName = "SuperAdmin",
                Email = "SuperAdmin@domain.com",
                EmailConfirmed = true,
                IsActive = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "SuperAdmin@123");
                await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
            }
            await roleManger.SeedSuperAdminClaims();

        }
        private static async Task SeedSuperAdminClaims(this RoleManager<IdentityRole> roleManager)
        {
            var superAdminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            await roleManager.AddSuperAdminPermissionClaims(superAdminRole);
        }
        public static async Task AddSuperAdminPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GenerateSuperAdminPermissions();

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
        #endregion

        #region Admin
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "Admin",
                FullName = "Admin",
                Email = "Admin@domain.com",
                EmailConfirmed = true,
                IsActive = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Admin@123");
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
            }
            await roleManger.SeedAdminClaims();

        }
        
        private static async Task SeedAdminClaims(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.Admin.ToString());
            await roleManager.AddAdminPermissionClaims(adminRole);
        }
        

        public static async Task AddAdminPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GenerateAdminPermissions();

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
        
        #endregion
    }
}