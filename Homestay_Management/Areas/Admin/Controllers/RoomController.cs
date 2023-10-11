using Homestay_Management.Data;
using Homestay_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Homestay_Management.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/room")]        //Đường dẫn đến controller mà nó xử lý
    public class RoomController : Controller
    {
        private readonly DataContext _dataContext;
        //Sử dụng private readonly đảm bảo tính ổn định trong suốt quá trình chạy ứng dụng
        public RoomController(DataContext dataContext)  //Constructor
        {
            _dataContext = dataContext;                 //Khởi tạo _dataContext từ tham số truyền vào
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        //Lấy danh sách phòng và phân trang
        [Route("listroom")]    //Khai báo đường dẫn
        public IActionResult ListRoom(int? page)   //Để phân trang được ta cần add trong Nuget:                                                 x.PagedList.Mvc.Core và x.PagedList
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listRoom = _dataContext.tblRoom
                            .Include(r => r.TypeRoom) // Kết hợp thông tin Tên loại Phòng
                            .AsNoTracking().OrderBy(r => r.Name)
                            .ToList();
            PagedList<RoomModel> list = new PagedList<RoomModel>(listRoom, pageNumber, pageSize); 
            return View(list);
        }


        //AddRoom 
        [Route("addroom")]
        //[HttpGet]           //Có thể không cần dòng này vì mặc định nó là [HttpGet]
        public IActionResult AddRoom(RoomModel roomModel)
        {
            //Lấy TypeRoomId hiển thị lên Name của TypeRoomId
            ViewBag.TypeRoomId = new SelectList(_dataContext.tblTypeRoom.ToList(), "Id", "Name");
            return View();
        }

        [Route("addroom")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoom([Bind("Id,Name,Price,Size,Capacity,TypeRoomId,Image,Device,Description,Status")] RoomModel roomModel, IFormFile file)
        {
            ModelState.Remove("TypeRoom");
            ViewBag.TypeRoomId = new SelectList(_dataContext.tblTypeRoom.ToList(), "Id", "Name");

            roomModel.Image = Helper.Getbase64(file);

            ModelState.Remove("Image");
            if(ModelState.IsValid)
            {
                _dataContext.Add(roomModel);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(ListRoom));
            }
            return View(roomModel);
        }


        //EditRoom
        [Route("editroom")]
        public async Task<IActionResult> EditRoom(int? roomId)
        {
            //Lấy Id hiển thị lên Name của TypeRommId
            ViewBag.TypeRoomId = new SelectList(_dataContext.tblTypeRoom.ToList(), "Id", "Name");
            if(roomId == null || _dataContext.tblRoom == null)
            {
                return NotFound();
            }
            var room = await _dataContext.tblRoom.FindAsync(roomId);
            if( room == null )
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        [Route("editroom")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoom(int roomId, [Bind("Id,Name,Price,Size,Capacity,TypeRoomId,Image,Device,Description,Status")] RoomModel roomModel, IFormFile file)
        {
            ModelState.Remove("TypeRoom");
            ModelState.Remove("Image");
            if(file != null)
            {
                roomModel.Image = Helper.Getbase64(file);
            }
            if(roomId != roomModel.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(roomModel);
                    await _dataContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException) 
                {
                    if(!RoomExsits(roomModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }    
                }
                return RedirectToAction("listroom");
            }
            ViewBag.TypeRoomId = new SelectList(_dataContext.tblTypeRoom.ToList(), "Id", "Name");
            return View(roomModel);
        }

        private bool RoomExsits(int roomId)
        {   //Mặc định của bool là false. "?." kiểm tra xem _dataContext.tblRoom có null hay không
            return (_dataContext.tblRoom?.Any(r=>r.Id == roomId)).GetValueOrDefault();
        }


        //DeleteRoom
        [Route("deleteroom")]
        public IActionResult DeleteRoom(int roomId)
        {
            //Trường hợp này khi nếu có hóa đơn đặt phòng rồi thì không được xóa.Mấy nữa làm đến bảng hóa đơn thì cần cho vào
            //TempData["Message"] = "This room cannot be deleted because it has already been booked.";
            //var bill = _dataContext.tblBill.Any(x => x.Id == roomId);
            //if (bill.Cout() > 0)
            //{
            //    return RedirectToAction("listroom");
            //}


            //Mấy nữa thiết kế nếu hình ảnh để ra 1 bảng riêng thì sẽ xóa cả hình ảnh liên quan ở đây
            //var imageRoom = _dataContext.tblImage.Where(x => x.Id == roomId);
            //if (imageRoom.Any()) //Nếu có ít nhất 1 ảnh
            //{
            //    _dataContext.RemoveRange(imageRoom);  //Xóa hàng loạt
            //}

            _dataContext.Remove(_dataContext.tblRoom.Find(roomId));
            _dataContext.SaveChanges();
            TempData["Message"] = "Room has been deleted";
            return RedirectToAction("listroom");
        }     
    }
}