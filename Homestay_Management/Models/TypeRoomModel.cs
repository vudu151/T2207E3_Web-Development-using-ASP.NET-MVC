using System.ComponentModel.DataAnnotations;

namespace Homestay_Management.Models
{
    public class TypeRoomModel
    {
        [Key]
        public int Id { get; set; }


        [Required, MinLength(3, ErrorMessage = "Required to enter room type (minlength = 3).")]
        public string Name { get; set; }


        [Required, MinLength(3, ErrorMessage = "Required to enter room type description (minlength = 3).")]
        public string Description { get; set; }
    }
}
