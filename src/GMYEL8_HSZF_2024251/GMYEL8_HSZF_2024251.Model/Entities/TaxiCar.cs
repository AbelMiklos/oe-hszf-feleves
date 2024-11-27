using System.ComponentModel.DataAnnotations;

namespace GMYEL8_HSZF_2024251.Model.Entities
{
    public class TaxiCar
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(AllowEmptyStrings = false, ErrorMessage = "License plate is required.")]
        public string LicensePlate { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Driver name is required.")]
        public string Driver { get; set; } = string.Empty;

        public virtual ICollection<Service> Services { get; set; } = [];
    }
}
