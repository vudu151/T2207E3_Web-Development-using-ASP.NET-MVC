using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Homestay_Management.Models.Authentication
{
	public class Authentication : ActionFilterAttribute 
		//Tạo bộ lọc hành động (action filters) cho các phương thức điều khiển (controller methods).
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if(context.HttpContext.Session.GetString("Email")==null)   
				//Nếu chưa login thì chuyển tới trang Login
			{
				context.Result = new RedirectToRouteResult(
					new RouteValueDictionary
					{
						{"Controller", "Access"},
						{"Action", "Login"}
					});
			}	
		}
	}

	//tạo một bộ lọc hành động (action filter) để kiểm tra xem người dùng đã đăng nhập hay chưa trước khi họ truy cập một phương thức điều khiển (controller method) cụ thể
}
