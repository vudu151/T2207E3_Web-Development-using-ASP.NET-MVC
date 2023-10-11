using Homestay_Management.Data;
using Homestay_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homestay_Management.Controllers
{
	public class RoomsController : Controller
	{
		private readonly DataContext _dataContext;
		public RoomsController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public IActionResult ListRoom()
		{
			//Truy vấn danh sách các phòng từ CSDL SQL Server
			List<RoomModel> rooms = _dataContext.tblRoom.Include(r => r.TypeRoom).ToList();
			return View(rooms);
		}

		public IActionResult RoomDetail(int roomId) {

			//Lấy thông tin phòng dựa trên roomId
			var room = _dataContext.tblRoom.SingleOrDefault(r => r.Id == roomId);
			if(room == null)
			{
				return NotFound();  //Trả về không tìm thấy 
			}
			
			return View(room);		//Truyền thông tin phòng cho trang RoomDetail
		}
	}
}
