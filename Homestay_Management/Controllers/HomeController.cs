using Homestay_Management.Models;
using Homestay_Management.Data;
using Homestay_Management.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Homestay_Management.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataContext _dataContext;
        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //[Authentication]        //Đăng nhập được thì mới vào trang Index
        public IActionResult Index()
        {
            List<RoomModel> rooms = _dataContext.tblRoom.Include(r => r.TypeRoom).Take(4).ToList();
            return View(rooms);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}