using System.ComponentModel.DataAnnotations;

namespace Solution.BAL.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }

        [Required(ErrorMessage = "License number is required.")]
        public string LicenseNumber { get; set; }

        [Required(ErrorMessage = "Make of the vehicle is required.")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model of the vehicle is required.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Year of the vehicle is required.")]
        public int Year { get; set; }

        public ICollection<MaintenanceActivity> MaintenanceActivities { get; set; }
    }
}
