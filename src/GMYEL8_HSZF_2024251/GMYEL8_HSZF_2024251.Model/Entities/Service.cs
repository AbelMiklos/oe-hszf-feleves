using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMYEL8_HSZF_2024251.Model.Entities
{
    public class Service
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(AllowEmptyStrings = false, ErrorMessage = "Taxi car's licence plate is required;")]
        public string TaxiCarId { get; set; } = string.Empty;

        [ForeignKey(nameof(TaxiCarId))]
        public virtual TaxiCar TaxiCar { get; set; } = default!;

        [Required(ErrorMessage = "Start location is required.", AllowEmptyStrings = false)]
        public string From { get; set; } = string.Empty;

        [Required(ErrorMessage = "End location is required.", AllowEmptyStrings = false)]
        public string To { get; set; } = string.Empty;

        [Required(ErrorMessage = "Distance is required.")]
        public int Distance { get; set; } = 0;

        [Required(ErrorMessage = "Fare is required.")]
        public int PaidAmount { get; set; } = 0;

        [Required(ErrorMessage = "Fare start date is required.")]
        public DateTime FareStartDate { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"- Taxi Car ID: {TaxiCarId}\n\t" +
                   $"- From: {From}\n\t" +
                   $"- To: {To}\n\t" +
                   $"- Distance: {Distance} km\n\t" +
                   $"- Paid Amount: ${PaidAmount}\n\t" +
                   $"- Fare Start Date: {FareStartDate:yyyy-MM-dd HH:mm}";
        }
    }
}
