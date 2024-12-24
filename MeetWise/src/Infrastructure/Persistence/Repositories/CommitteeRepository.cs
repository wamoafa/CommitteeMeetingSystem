using MeetWise.Domain.Entities;
using MeetWise.Infrastructure.Data;

public class CommitteeRepository : ICommitteeRepository
{
    private readonly ApplicationDbContext _context;

    public CommitteeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Committee>> GetAllAsync()
    {
        return await _context.Committees
            .Where(c => !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<Committee> GetByIdAsync(int id)
    {
        return await _context.Committees
            .Include(c => c.Members)
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
    }

    public async Task AddAsync(Committee committee)
    {
        _context.Committees.Add(committee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Committee committee)
    {
        _context.Committees.Update(committee);
        await _context.SaveChangesAsync();
    }
}

