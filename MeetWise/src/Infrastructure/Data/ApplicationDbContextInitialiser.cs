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
            if (!_context.Committees.Any() && !_context.Members.Any() && !_context.Sessions.Any())
            {
                var member1 = new Member
                {
                    Name = "عبدالله بن محمد",
                    NationalId = "1010101010",
                    Username = "a.mohammad",
                    PhoneNumber = "0500000001",
                    IsActive = true,
                    IsDeleted = false
                };

                var member2 = new Member
                {
                    Name = "سعود بن عبدالعزيز",
                    NationalId = "2020202020",
                    Username = "s.abdulaziz",
                    PhoneNumber = "0500000002",
                    IsActive = true,
                    IsDeleted = false
                };

                var member3 = new Member
                {
                    Name = "فيصل بن خالد",
                    NationalId = "3030303030",
                    Username = "f.khalid",
                    PhoneNumber = "0500000003",
                    IsActive = true,
                    IsDeleted = false
                };

                _context.Members.AddRange(member1, member2, member3);

                var financeCommittee = new Committee
                {
                    Name = "لجنة المالية",
                    Details = "لجنة مختصة بالشؤون المالية.",
                    IsActive = true,
                    IsDeleted = false,
                    Members = new[] { member1, member2 }
                };

                _context.Committees.Add(financeCommittee);

                var session1 = new Session
                {
                    Name = "اجتماع مناقشة الميزانية",
                    Date = DateTime.Now.AddDays(-10),
                    Details = "مناقشة ميزانية العام القادم.",
                    IsActive = true,
                    IsDeleted = false,
                    Committee = financeCommittee,
                    Members = new[] { member1, member2 }
                };

                _context.Sessions.Add(session1);

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
