using MeetWise.Domain.Entities;
using MeetWise.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
   DbSet<Committee> Committees { get;}
    DbSet<Member> Members { get; }
    DbSet<Session> Sessions { get;}


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
