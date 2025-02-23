using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SH_BusinessObjects.Common.Interface;
using SH_BusinessObjects.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.Context
{
    public class SaplingHubContextInitialiser(ILogger<SaplingHubContextInitialiser> logger, SaplingHubContext context
        //, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
        )
    {
        private readonly ILogger<SaplingHubContextInitialiser> _logger = logger;
        private readonly SaplingHubContext _context = context;
        //private readonly UserManager<ApplicationUser> _userManager = userManager;
        //private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
