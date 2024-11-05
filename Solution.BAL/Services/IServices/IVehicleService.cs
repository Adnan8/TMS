using Solution.BAL.Models;

namespace Solution.BAL.Services.IServices
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<Vehicle> GetByIdAsync(int id);
        Task AddAsync(Vehicle model);
        Task UpdateAsync(Vehicle model);
        Task<bool> DeleteAsync(int id);
    }
}
