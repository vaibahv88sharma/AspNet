using Microsoft.AspNetCore.Identity;
using StudentHubApplication.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;

namespace StudentHubApplication.API
{
    public class CampIdentityInitializer
    {
        private RoleManager<IdentityRole> _roleMgr;
        private UserManager<CampUser> _userMgr;
        public CampIdentityInitializer(UserManager<CampUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        public async Task Seed()
        {
            var user = await _userMgr.FindByNameAsync("shawnwildermuth");
            // Add User
            if (user == null)
            {
                if (!(await _roleMgr.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");

                    // NOT DONE
                    //role.Claims.Add(new IdentityRoleClaim<string>() { ClaimType = "IsAdmin", ClaimValue = "True" });                    
                    await _roleMgr.CreateAsync(role);
                    //await _roleMgr.CreateAsync(role);
                    //await _roleMgr.AddClaimAsync(role, new Claim(ClaimTypes. = "IsAdmin"));
                }

                user = new CampUser()
                {
                    UserName = "shawnwildermuth",
                    FirstName = "Shawn",
                    LastName = "Wildermuth",
                    Email = "shawn@wildermuth.com"
                };

                var userResult = await _userMgr.CreateAsync(user, "P@ssw0rd!");
                var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
                var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }

            }
        }
    }
}
