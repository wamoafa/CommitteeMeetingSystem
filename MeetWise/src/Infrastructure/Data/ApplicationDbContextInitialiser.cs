using MeetWise.Domain.Constants;
using MeetWise.Domain.Entities;
using MeetWise.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MeetWise.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
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
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }
        try
        {
            // Default data
            // Seed, if necessary
            if (!_context.Committees.Any())
            {
                var member1 = new Member
                {
                    Name = "Member One",
                    NationalId = "1234567890",
                    Username = "member1",
                    PhoneNumber = "0501234567",
                    IsDeleted = false,
                    IsActive = true
                };

                var member2 = new Member
                {
                    Name = "Member Two",
                    NationalId = "0987654321",
                    Username = "member2",
                    PhoneNumber = "0507654321",
                    IsDeleted = false,
                    IsActive = true
                };

                var member3 = new Member
                {
                    Name = "Member Three",
                    NationalId = "1122334455",
                    Username = "member3",
                    PhoneNumber = "0501122334",
                    IsDeleted = false,
                    IsActive = true
                };

                var member4 = new Member
                {
                    Name = "Member Four",
                    NationalId = "5566778899",
                    Username = "member4",
                    PhoneNumber = "0505566778",
                    IsDeleted = false,
                    IsActive = true
                };

                var member5 = new Member
                {
                    Name = "Member Five",
                    NationalId = "6677889900",
                    Username = "member5",
                    PhoneNumber = "0506677889",
                    IsDeleted = false,
                    IsActive = true
                };

                var committee1 = new Committee
                {
                    Name = "Committee One",
                    Details = "Details of Committee One",
                    IsDeleted = false,
                    IsActive = true,
                    Members = { member1, member2 }
                };

                var committee2 = new Committee
                {
                    Name = "Committee Two",
                    Details = "Details of Committee Two",
                    IsDeleted = false,
                    IsActive = true,
                    Members = { member2, member3 }
                };

                var committee3 = new Committee
                {
                    Name = "Committee Three",
                    Details = "Details of Committee Three",
                    IsDeleted = false,
                    IsActive = true,
                    Members = { member3, member4 }
                };

                var committee4 = new Committee
                {
                    Name = "Committee Four",
                    Details = "Details of Committee Four",
                    IsDeleted = false,
                    IsActive = true,
                    Members = { member4, member5 }
                };

                var committee5 = new Committee
                {
                    Name = "Committee Five",
                    Details = "Details of Committee Five",
                    IsDeleted = false,
                    IsActive = true,
                    Members = { member1, member5 }
                };

                _context.Committees.AddRange(committee1, committee2, committee3, committee4, committee5);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}
