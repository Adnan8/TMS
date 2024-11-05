using Microsoft.EntityFrameworkCore;
using Solution.BAL.Data;
using Solution.BAL.Models;
using Solution.BAL.Services.IServices;

public class VehicleService : IVehicleService
{
    private readonly ApplicationDbContext _context;

    public VehicleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<Vehicle> GetByIdAsync(int id)
    {
        return await _context.Vehicles.FindAsync(id);
    }

    public async Task AddAsync(Vehicle model)
    {
        _context.Vehicles.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vehicle model)
    {
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var maintenanceType = await _context.Vehicles.FindAsync(id);
        if (maintenanceType != null)
        {
            _context.Vehicles.Remove(maintenanceType);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
