using BC = BCrypt.Net.BCrypt;
using Homestay_Management.Data;
using Homestay_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Homestay_Management.Controllers
{
	public class AccessController : Controller
	{
		private readonly DataContext _dataContext;//Sử dụng private readonly để đảm bảo tính ổn định trong suốt quá trình chạy ứng dụng
		private readonly UserManager<IdentityUser> _userManager;

		public AccessController(DataContext dataContext, UserManager<IdentityUser> userManager)
        {   //Constructor

            _dataContext = dataContext;           //Khởi tạo _dataContext từ tham số truyền vào
			_userManager = userManager;
		}


		//GET: Register
		public IActionResult Register()
		{
			return View();
		}

		//POST: Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Register(UserModel userModel)
		{
			if (ModelState.IsValid)
			{
				var check = _dataContext.tblUser.FirstOrDefault(e => e.Email == userModel.Email);
				if (check == null)
				{
					userModel.Password = BC.HashPassword(userModel.Password);
					userModel.ConfirmPassword = BC.HashPassword(userModel.ConfirmPassword);
					userModel.IsGroup = 1;        //Thiết lập mặc định IsGroup là 1 (tức là User)
					_dataContext.tblUser.Add(userModel);
					_dataContext.SaveChanges();

					ViewBag.success = "Registration successful, please log in to continue !";
                    //return RedirectToAction("Login", "Access");
					return View();
				}
				else
				{
					ViewBag.error = "Email already exists";
				}
			}
			return View();
		}


		//GET: Login		
		public IActionResult Login()
		{
			if(HttpContext.Session.GetString("Email")==null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		//POST: Login
		[HttpPost]
		//[ValidateAntiForgeryToken]				
		public IActionResult Login(UserModel userModel)
		{
			if(HttpContext.Session.GetString("Email") == null)
			{
                //Kiểm tra email và password của người dùng
                var user = _dataContext.tblUser.FirstOrDefault(x => x.Email == userModel.Email);   

				if(user != null)
				{
                    HttpContext.Session.SetString("Email", userModel.Email.ToString());
					bool verified = BC.Verify(userModel.Password, user.Password);
					if (verified)
					{
						//Nếu người dùng hợp lệ lưu Email dùng vào Session và chuyển sang trang Index
						HttpContext.Session.SetString("Email", userModel.Email.ToString());
						return RedirectToAction("Index", "Home");
					}
					else
					{
						ViewBag.error = "Invalid email or password";
					}
				}
            }
            return View();
		}


		//Logout
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("Email");
			return RedirectToAction("Login", "Access");
		}


		//Forgot-Password
		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
		{
			if(ModelState.IsValid)
			{
				//Kiểm tra địa chỉ email có tồn tại trong hệ thống hay không
				var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
				if(user != null)
				{
					//Tạo mã thông báo đặt lại mật khẩu và gửi email 
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					//Tạo đường dẫn đến trang Reset Password
					var resetLink = Url.Action("ResetPassword", "Access", new { userId = user.Id, token = token }, Request.Scheme);



                    ModelState.Clear();
                    forgotPasswordModel.EmailSent = true;
                }
				else
				{
					//Xử lý trường hợp email không tồn tại trong hệ thống
					ModelState.AddModelError(string.Empty, "Email does not exist in the system.");
				}
				
			}
			return View(forgotPasswordModel);
		}

		
		public IActionResult ResetPassword()
		{
			return View();
		}

		

	}
}
