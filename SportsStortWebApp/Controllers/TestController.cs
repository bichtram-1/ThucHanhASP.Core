using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStortWebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Chào mừng bạn đến với cửa hàng thê thao! Đây là trang test";

            return View();
        }

        public IActionResult HelloWord()
        {
            return Content("XIn chào từ Action HelloWord của Test COntroller!");
        }

        public IActionResult Welcome(string name = "Khách")
        {
            return Content($"Chào mừng {name} đến với trang Test");
        }
    }
}