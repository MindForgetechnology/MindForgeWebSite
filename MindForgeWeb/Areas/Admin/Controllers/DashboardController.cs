using Microsoft.AspNetCore.Mvc;
using MindForgeWeb.Data;
using MindForgeWeb.Models;
using System.Data;
using System.Reflection;

namespace MindForge.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly Datalayer _datalayer;
        public DashboardController(Datalayer datalayer)
        {

            _datalayer = datalayer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Home/Login");
            } 
            var model = new DashboardDto(); 
            model.TotalBlogs = _datalayer.GetCountallBlogData().Rows[0]["TotalBlogs"].ToString();
            model.TotalServiceDetails = _datalayer.GetCountServiceDetailsData().Rows[0]["TotalServiceDetails"].ToString();
            model.TotalComments = _datalayer.GetCountCommentData().Rows[0]["TotalComments"].ToString();
            model.TotalService = _datalayer.GetCountServiceData().Rows[0]["TotalService"].ToString();
            model.TotalMspf = _datalayer.GetCountMaspftData().Rows[0]["TotalMspf"].ToString();
            model.TotalPftDetails = _datalayer.GetCountAllPftDetailsData().Rows[0]["TotalPftDetails"].ToString();
           
            return View(model);
        }


        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {

                return Redirect("/Home/Login");

            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var sessionId = HttpContext.Session.GetString("UserId");
            if (int.TryParse(sessionId, out int userId) && userId > 0)
            {
                DataTable userData = _datalayer.GetUserById(userId);
                if (userData.Rows.Count > 0)
                {
                    var row = userData.Rows[0];
                    var userProfile = new UserProfileDto
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        UserName = row["UserName"].ToString(),
                        Email = row["Email"].ToString(),
                        MobileNu = row["Mobile"].ToString(),
                        Password = row["Password"].ToString()
                    };
                    return View(userProfile);
                }
                return NotFound();
            }
            return View(new UserProfileDto());
        }





        [HttpPost]
        public IActionResult ChangePassword(UserProfileDto user)
        {
            ModelState.Remove("Email");
            if (user == null || !ModelState.IsValid)
            {
                return View(user);
            }
            try
            {
                user.Action = "8";
                _datalayer.UpdateUserDataById(user);
                TempData["SuccessMessage"] = "Change Password Successfully!";
                return Redirect("/Admin/Dashboard/ChangePassword");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
            }
            return View(user);
        }



    }
}
