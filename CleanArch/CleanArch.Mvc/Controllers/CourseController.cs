using CleanArch.Application.Intefaces;
using CleanArch.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Mvc.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {

        public CourseController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
