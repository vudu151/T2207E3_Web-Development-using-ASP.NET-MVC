using System.ComponentModel.DataAnnotations;

namespace Homestay_Management.Models
{
    public class ServiceModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Required to enter service name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required to enter service price.")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Check-in date is mandatory.")]
        public DateTime DateCheckIn { get; set; }
        [Required(ErrorMessage = "Check-out date is mandatory.")]
        public DateTime DateCheckOut { get; set; }
    }
}
