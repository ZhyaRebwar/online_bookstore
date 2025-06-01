using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using online_bookstore.Model;
using System.Composition;
using System.Data;
using System.Security.Claims;

namespace online_bookstore.Services
{
    public class RoleClaimService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleClaimService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // Create Role
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return IdentityResult.Failed(new IdentityError { Description = "Role already exists" });

            return await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        // Add Claim (Permission) to Role
        public async Task<IdentityResult> AddClaimToRoleAsync(string roleName, string claimType, string claimValue)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

            var claims = await _roleManager.GetClaimsAsync(role);
            if (claims.Any(c => c.Type == claimType && c.Value == claimValue))
                return IdentityResult.Failed(new IdentityError { Description = "Claim already exists for role" });

            return await _roleManager.AddClaimAsync(role, new Claim(claimType, claimValue));
        }

        // Assign Role to User
        public async Task<IdentityResult> AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            if (await _userManager.IsInRoleAsync(user, roleName))
                return IdentityResult.Failed(new IdentityError { Description = "User already in role" });

            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
