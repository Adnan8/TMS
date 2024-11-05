using Microsoft.EntityFrameworkCore;
using Solution.BAL.Data;
using Solution.BAL.Models;
using Solution.BAL.Services.IServices;

public class MaintenanceTypeService : IMaintenanceTypeService
{
    private readonly ApplicationDbContext _context;

    public MaintenanceTypeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceType>> GetAllAsync()
    {
        return await _context.MaintenanceTypes.ToListAsync();
    }

    public async Task<MaintenanceType> GetByIdAsync(int id)
    {
        return await _context.MaintenanceTypes.FindAsync(id);
    }

    public async Task AddAsync(MaintenanceType model)
    {
        _context.MaintenanceTypes.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MaintenanceType model)
    {
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var maintenanceType = await _context.MaintenanceTypes.FindAsync(id);
        if (maintenanceType != null)
        {
            _context.MaintenanceTypes.Remove(maintenanceType);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

}
