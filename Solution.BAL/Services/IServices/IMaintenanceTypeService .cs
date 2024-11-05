using Solution.BAL.Models;

namespace Solution.BAL.Services.IServices
{
    public interface IMaintenanceTypeService
    {
        Task<IEnumerable<MaintenanceType>> GetAllAsync();
        Task<MaintenanceType> GetByIdAsync(int id);
        Task AddAsync(MaintenanceType model);
        Task UpdateAsync(MaintenanceType model);
        Task<bool> DeleteAsync(int id);
    }
}
