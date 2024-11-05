using Solution.BAL.Models;

namespace Solution.BAL.Services.IServices
{
    public interface IMaintenanceActivityService
    {
        Task<IEnumerable<MaintenanceActivity>> GetAllAsync();
        Task<MaintenanceActivity> GetByIdAsync(int id);
        Task AddAsync(MaintenanceActivity model);
        Task UpdateAsync(MaintenanceActivity model);
        Task<bool> DeleteAsync(int id);
    }
}
