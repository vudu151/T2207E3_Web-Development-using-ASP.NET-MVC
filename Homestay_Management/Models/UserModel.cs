using System.ComponentModel.DataAnnotations;

namespace Homestay_Management.Models
{
	public class UserModel
	{
		[Key]
		public int UserId { get; set; }


		[Required, MinLength(3, ErrorMessage = "UserName must be at least 3 characters.")]
		public string UserName { get; set; }


		[Required]
		[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
		public string Email { get; set; }


		[Required, MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required, MinLength(8, ErrorMessage = "Password incorrect")]
		[DataType(DataType.Password), Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }


		public int IsGroup { get; set; }
		//Đây là phân quyền tk: 0 là Admin, 1 là User nhưng chưa dùng tới
		//Khi User đăng kí thì mặc định là 1 (Đã được thiết lập sẵn bên Controller)
	}
}
