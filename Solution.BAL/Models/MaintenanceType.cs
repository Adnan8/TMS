using System.ComponentModel.DataAnnotations;

namespace Solution.BAL.Models
{
    public class MaintenanceType
    {
        [Key]
        public int MaintenanceTypeID { get; set; }
        [Required(ErrorMessage = "Maintenance Type  is required.")]
        public string TypeName { get; set; }
        public ICollection<MaintenanceActivity> MaintenanceActivities { get; set; }
    }
}
