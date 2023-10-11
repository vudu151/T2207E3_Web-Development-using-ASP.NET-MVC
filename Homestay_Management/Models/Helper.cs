namespace Homestay_Management.Models;
using System;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;

public class Helper
{
    //Phương thức Getbase64 dùng để chuyển đổi 1 tệp tin hình ảnh (được đại diện bởi đối tượng IFormFile) thành chuỗi base64.
    public static string Getbase64(IFormFile file)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            file.CopyTo(ms);
            byte[] imageBytes = ms.ToArray();
            //Chuyển dữ liệu thành chuỗi thành chuỗi Base64
            return "data:image/webp;base64," + Convert.ToBase64String(imageBytes);
        }
        
    }


    ////Mã hóa mật khẩu
    //public static string HashPassword (string password)
    //{
    //    return BCrypt.HashPassword(password, BCrypt.GenerateSalt());
    //}
    ////Xác thực mật khẩu
    //public static bool VerifyPassword(string password, string hashPassword)
    //{
    //    return BCrypt.Verify(password, hashPassword);
    //}
}

