using cinemaSite.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace cinemaSite.Utitlity.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if(_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            if(_roleManager.Roles.IsNullOrEmpty())
            {
                _roleManager.CreateAsync(new(SD.SuperAdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new(SD.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new(SD.CustomerRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new(SD.CompanyRole)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new()
                {
                    Name = "SuperAdmin",
                    Email = "SuperAdmin@gamil.com",
                    EmailConfirmed = true,
                    UserName = "SuperAdmin",
                }, "Admin123$").GetAwaiter().GetResult();

                var user = _userManager.FindByNameAsync("SuperAdmin").GetAwaiter().GetResult(); ;

                if(user is not null)
                    _userManager.AddToRoleAsync(user, SD.SuperAdminRole).GetAwaiter().GetResult(); ;
            }
        }
    }
}
