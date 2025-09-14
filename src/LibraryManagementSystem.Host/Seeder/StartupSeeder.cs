﻿using LibraryManagementSystem.Infrastructure.Persistence.DataSeed;
using LibraryManagementSystem.Infrastructure.Persistence.DataSeed.Runtime.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Host.Seeder
{
    public static class StartupSeeder
    {
        /// <summary>
        /// Seeds initial data for the system: roles, default users, etc.
        /// Call this from Program.cs at startup.
        /// </summary>
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));

            // Seed Roles
            await IdentityDataSeed.SeedRolesAsync(serviceProvider);
            // Seed Default Users
            await IdentityDataSeed.SeedAdminUserAsync(serviceProvider);
            await IdentityDataSeed.SeedManagerUserAsync(serviceProvider);
            await IdentityDataSeed.SeedAuditorUserAsync(serviceProvider);
            // Seed Default Permissions
            await PermissionSeed.SeedPermissionsAsync(serviceProvider);
            await RolePermissionSeed.SeedRolePermissionsAsync(serviceProvider);
           
        }
    }
}
