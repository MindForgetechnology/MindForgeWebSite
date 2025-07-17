using Microsoft.AspNetCore.Mvc;
using System.Data; 
using MindForgeWeb.Data;
using MindForgeWeb.Models;

namespace MindForge.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly Datalayer _datalayer;
        public BlogsController(Datalayer datalayer)
        {
            _datalayer = datalayer;
        }
         
        public IActionResult BlogComments()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }
            DataTable result = _datalayer.GetBlogCommentDetailsList();
            if (result == null || result.Rows.Count == 0)
            {
                return View(new List<CommentBlogsDto>());
            }
            var careers = new List<CommentBlogsDto>();
            foreach (DataRow row in result.Rows)
            {
                careers.Add(new CommentBlogsDto
                {
                    CommentId= Convert.ToInt32(row["CommentId"]),
                    Email = row["Email"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"]),
                    Name = row["Name"].ToString(),
                    Message = row["Message"].ToString(),
                    Action= row["BlogTittle"].ToString() 
                });
            }
            return View(careers);
        }

        public IActionResult DeleteComment(int id)
        {
            if (id > 0)
            {
                var success = _datalayer.DeleteComments(id);

                if (success)
                {
                    TempData["SuccessMessage"] = "Deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "There was an error deleting the career.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid   ID.";
            }
            return Redirect("/Admin/Blogs/BlogComments");
        }

        [HttpPost]
        public IActionResult BlogCommentHideShow([FromBody] CommentBlogsDto request)
        {
            if (request?.CommentId > 0)
            {
                var success = _datalayer.BlogCommentHideshow(request.CommentId, request.Checked);

                if (success)
                {
                    return Json(new { success = true, message = "Comment status updated successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "There was an error updating the Comment status." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid Comment ID." });
            }
        }



    }
}
