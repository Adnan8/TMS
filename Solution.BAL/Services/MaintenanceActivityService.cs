using Microsoft.EntityFrameworkCore;
using Solution.BAL.Data;
using Solution.BAL.Models;
using Solution.BAL.Services.IServices;

public class MaintenanceActivityService : IMaintenanceActivityService
{
    private readonly ApplicationDbContext _context;

    public MaintenanceActivityService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceActivity>> GetAllAsync()
    {
        return await _context.MaintenanceActivities.Include(x => x.MaintenanceType).Include(x => x.Vehicle).ToListAsync();
    }

    public async Task<MaintenanceActivity> GetByIdAsync(int id)
    {
        return await _context.MaintenanceActivities.FindAsync(id);
    }

    public async Task AddAsync(MaintenanceActivity model)
    {
        _context.MaintenanceActivities.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MaintenanceActivity model)
    {
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var maintenance = await _context.MaintenanceActivities.FindAsync(id);
        if (maintenance != null)
        {
            _context.MaintenanceActivities.Remove(maintenance);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
