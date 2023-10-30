using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanVali.Models;
namespace WebBanVali.Controllers
{
    public class AccessController1 : Controller
    {
        DbBanValiContext db = new DbBanValiContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "AccessController1");
        }
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Register(TUser user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Kiểm tra xem tên người dùng đã tồn tại chưa
        //        if (db.TUsers.Any(u => u.Username == user.Username))
        //        {
        //            ModelState.AddModelError("Username", "Tên người dùng đã tồn tại.");
        //            return View(user);
        //        }
        //        if (user.Password != user.ConfirmPassword)
        //        {
        //            ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
        //            return View(user);
        //        }
        //        // Tạo một đối tượng KhachHang và đặt giá trị các thuộc tính
        //        var TUser = new TUser
        //        {
        //            Username = user.Username,
        //            Password = user.Password,
        //            LoaiUser = 0 // LoaiUser=0 cho khách hàng
        //        };

        //        // Thêm user mới vào DbContext và lưu vào cơ sở dữ liệu
        //        db.TUsers.Add(TUser);
        //        db.SaveChanges();

        //        // Chuyển hướng đến trang đăng nhập hoặc trang chính của ứng dụng
        //        return RedirectToAction("Login", "AccessController1"); // Thay thế bằng tên trang đăng nhập của bạn
        //    }
        //    return View();
        //}
    }
}
