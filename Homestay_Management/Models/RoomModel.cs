using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homestay_Management.Models
{
    [Table("tblRoom")]
    public class RoomModel
    {
        [Key]
        public int Id { get; set; }


        [Required, MinLength(3, ErrorMessage = "Required to enter room name (minlength = 3).")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Required to enter room price.")]
        public int Price { get; set; }


        [Required(ErrorMessage = "Required to enter room size.")]
        public int Size { get; set; }


        [Required(ErrorMessage ="Required to enter room capacity.")]
        public int Capacity { get; set; }


        [Required, Range(1, int.MaxValue, ErrorMessage = "Required to choose room type.")]
        //Giá trị chọn từ 1 đến giá trị tối đa của kiểu dữ liệu int
        public int TypeRoomId { get; set; }
        [ForeignKey("TypeRoomId")]  //Chỉ  định khoá ngoại
        [Display(Name = "Type Room")] //Xác định tên hiển thị trên Web(html)
        public virtual TypeRoomModel TypeRoom { get; set; }
       
        

        [FileExtensions]    
        [DisplayName("Image")]
        public string Image { get; set; }


        [Required(ErrorMessage ="Required to enter room device.")]
        public string Device { get; set; }
        
       
        [Required, MinLength(3, ErrorMessage = "Required to enter room description (minlenght = 3).")]
        public string Description { get; set; }


        public enum RoomStatus
        {
            Unavailable,         //Unavailable là 0 trong SQL Server
            Available           //Available là 1 trong SQL Server
        }
        public RoomStatus Status { get; set; }
    }
}
