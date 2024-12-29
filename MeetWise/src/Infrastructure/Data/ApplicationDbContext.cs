using System.Reflection;
using MeetWise.Application.Interfaces;
using MeetWise.Domain.Entities;
using MeetWise.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Member = MeetWise.Domain.Entities.Member;

namespace MeetWise.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TodoList> TodoLists => Set<TodoList>();
        public DbSet<TodoItem> TodoItems => Set<TodoItem>();
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning));
        }

    }
}
