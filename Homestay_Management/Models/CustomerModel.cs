using System.ComponentModel.DataAnnotations;

namespace Homestay_Management.Models
{
    public class CustomerModel
    {
        [Key]
        public int CCCD { get; set; }
        [Required, MinLength(2, ErrorMessage = "Required to enter customer name.")]
        public string Name { get; set; }
        [Required, MinLength(10, ErrorMessage = "Required to enter customer phone.")]
        public int Phone { get; set; }
        [Required, MinLength(5, ErrorMessage = "Required to enter customer address.")]
        public string Address { get; set; }
    }
}
