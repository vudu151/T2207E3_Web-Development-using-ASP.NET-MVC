using Homestay_Management.Data;
using Homestay_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace Homestay_Management.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/typeroom")]
    public class TypeRoomController : Controller
    {
        private readonly DataContext _dataContext; //readonly đảm bảo tính ổn định (.) quá trình chạy
        public TypeRoomController(DataContext dataContext)
        {
            _dataContext = dataContext;    //constructor
        }

        //Lấy danh sách kiểu phòng và phân trang
        [Route("listtyperoom")]
        public IActionResult ListTypeRoom (int? page)
        {
            int sizePage = 10;
            int pageNumber = page == null || page < 0 ? 1: page.Value;
            var listTypeRoom = _dataContext.tblTypeRoom.ToList();
            PagedList<TypeRoomModel> list = new PagedList<TypeRoomModel> (listTypeRoom, pageNumber, sizePage);
            return View(list);
        }


        //AddTypeRoom
        [Route("addtyperoom")]        //Khai báo đường dẫn
        [HttpGet]
        public IActionResult AddTypeRoom()
        {
            return View();
        }

        [Route("addtyperoom")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTypeRoom(TypeRoomModel typeRoom)
        {
            if(ModelState.IsValid)
            {
                _dataContext.tblTypeRoom.Add(typeRoom);
                _dataContext.SaveChanges();
                return RedirectToAction("listtyperoom");
            }
            else
            {
                return View(typeRoom);
            }    
        }

        //Edit TypeRoom
        [Route("edittyperoom")]
        [HttpGet]
        public IActionResult EditTypeRoom(int typeRoomId)
        {
            var typeRoom = _dataContext.tblTypeRoom.Find(typeRoomId);
            return View(typeRoom);
        }

        [Route("edittyperoom")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTypeRoom(TypeRoomModel typeRoom)
        {
            if (ModelState.IsValid)
            {
                _dataContext.tblTypeRoom.Update(typeRoom);
                _dataContext.SaveChanges();
                return RedirectToAction("listtyperoom");
            }
            else
            {
                return View(typeRoom);
            }    
        }

        //Delete TypeRoom
        [Route("deletetyperoom")]
        [HttpGet]
        public IActionResult DeleteTypeRoom(int typeRoomId)
        {
            ////Delete TypeRoom Fail
            ////TH:Khi TypeRoomId là khóa ngoại của Room(tức đã có phòng có kiểu phòng xác định rồi)
            //var typeRoomHasRoom = _dataContext.tblTypeRoom.Where(x => x.Id == typeRoomId);
            //if(typeRoomHasRoom.Count() > 0) 
            //{
            //    TempData["Message"] = "This TypeRoom can not be deleted because TypeRoomId is Foreign Key of Room";
            //    return RedirectToAction("listtyperoom");
            //}


            ////Delete TypeRoom Success
            //var typeRoom = _dataContext.tblTypeRoom.Find(typeRoomId);
            //_dataContext.tblTypeRoom.Remove(typeRoom);
            //_dataContext.SaveChanges();
            //TempData["Message"] = "TypeRoom has been deleted";
            //return RedirectToAction("listtyperoom");  


            var typeRoom = _dataContext.tblTypeRoom.Find(typeRoomId);
            if(typeRoom == null)
            {
                TempData["Message"] = "TypeRoom not found";
                return RedirectToAction("listtyperoom");
            }

            //Kiểm tra xem TypeRoomId có liên quan đến Room không
            var TypeRoomHasRoom = _dataContext.tblRoom.Any(x => x.Id == typeRoomId);
            if(TypeRoomHasRoom)
            {
                TempData["Message"] = "This TypeRoom cannot be deleted because it's associated with one or more rooms.";
                return RedirectToAction("listtyperoom");
            }

            //Delete TypeRoom Success
            _dataContext.tblTypeRoom.Remove(typeRoom);
            _dataContext.SaveChanges();
            TempData["Message"] = "TypeRoom has been deleted";
            return RedirectToAction("listtyperoom");
        }
      
    }
}
