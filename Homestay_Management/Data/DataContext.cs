using Homestay_Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homestay_Management.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoomModel>().ToTable("tblRoom");//tblRoom là tên bảng bên SQL Server tương ứng với mô hình RoomModel trong dự án
            modelBuilder.Entity<TypeRoomModel>().ToTable("tblTypeRoom");
            modelBuilder.Entity<UserModel>().ToTable("tblUser");
        }
		// Thuộc tính kiểu DbSet đại diện cho bảng or tập hợp các đối tượng có kiểu RoomModel.
		public DbSet<TypeRoomModel> tblTypeRoom { get; set; }
        public DbSet<RoomModel> tblRoom { get; set; }
        public DbSet<UserModel> tblUser { get; set; }
        

    }
}







