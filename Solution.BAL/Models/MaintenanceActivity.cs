using System.ComponentModel.DataAnnotations;

namespace Solution.BAL.Models
{
    public class MaintenanceActivity
    {
        [Key]
        public int MaintenanceID { get; set; }

        [Required(ErrorMessage = "Vehicle is required.")]
        public int VehicleID { get; set; }

        [Required(ErrorMessage = "Maintenance Type is required.")]
        public int MaintenanceTypeID { get; set; }

        [Required(ErrorMessage = "Date of maintenance is required.")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Description of the maintenance is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Notes are required.")]
        public string Notes { get; set; }

        public Vehicle Vehicle { get; set; }
        public MaintenanceType MaintenanceType { get; set; }
    }
}
