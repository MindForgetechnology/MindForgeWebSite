using Microsoft.AspNetCore.Mvc;
using MindForgeWeb.Data;
using MindForgeWeb.Models;
using System.Data;

namespace MindForge.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class MasterController : Controller
    {
        private readonly Datalayer _datalayer;
        public MasterController(Datalayer datalayer)
        {
            _datalayer = datalayer;
        }
 
        [HttpGet]
        public IActionResult AddBlogDetails(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            } 
            if (id > 0)
            {
                DataTable dt = _datalayer.GetBlogdetailsById(id);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var vendor = new BlogDetailsDto
                    {
                        BlogId = Convert.ToInt32(row["BlogId"]), 
                        BlogTittle = row["BlogTittle"].ToString(),
                        PostDate = row["PostDate"].ToString(),
                        BlogDescription = row["BlogDescription"].ToString(),
                        Filename = row["FilePath"].ToString()
                    };
                    return View(vendor);
                }

                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddBlogDetails(BlogDetailsDto model)
        {
            ModelState.Remove("FileName");
            ModelState.Remove("FilePath");
            ModelState.Remove("BlogId"); 
            bool isNameUsed = _datalayer.GetNameBlogDetailsData(model.BlogTittle);

            if (model.BlogId == 0 && isNameUsed)
            {
                ModelState.AddModelError("BlogTittle", "This Name is Already Used!");
                return View(model);
            }
            if (model.FilePath == null || model.FilePath.Length == 0)
            {
                if (model.BlogId != 0 && string.IsNullOrEmpty(model.Filename))
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                }
                if (model.BlogId == 0)
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                }
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.FilePath != null && model.FilePath.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.FilePath.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.FilePath.CopyTo(stream);
                    }
                    model.Filename = uniqueFileName;
                }

                else if (model.BlogId != 0 && string.IsNullOrEmpty(model.Filename))
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                    return View(model);
                }
                if (model.BlogId == 0)
                {
                    model.Action = "1";
                    _datalayer.InsertUpdateBlogDetails(model);
                    TempData["SuccessMessage"] = "Data saved successfully!";
                    return Redirect("/Admin/Master/AddBlogDetails");
                }
                else
                {
                    model.Action = "2";
                    _datalayer.InsertUpdateBlogDetails(model);
                    TempData["SuccessMessage"] = "Data updated successfully!";
                    return Redirect("/Admin/Master/BlogDetailsList");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
            }

            return View(model);
        }

        public IActionResult DeleteBlogDetails(int id)
        {
            if (id > 0)
            {
                var success = _datalayer.DeleteBlogDetails(id);

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
                TempData["ErrorMessage"] = "Invalid career ID.";
            }
            return Redirect("/Admin/Master/BlogDetailsList");
        }

        public IActionResult BlogDetailsList()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }
            DataTable result = _datalayer.GetBlogDetailsList();
            if (result == null || result.Rows.Count == 0)
            {
                return View(new List<BlogDetailsDto>());
            }
            var careers = new List<BlogDetailsDto>();
            foreach (DataRow row in result.Rows)
            {
                careers.Add(new BlogDetailsDto
                {
                    BlogId = Convert.ToInt32(row["BlogId"]),
                    BlogTittle = row["BlogTittle"].ToString(),
                    PostDate = row["PostDate"].ToString(),
                    BlogDescription = row["BlogDescription"].ToString(),
                    Filename = row["FilePath"].ToString()
                });
            }
            return View(careers);
        }



        [HttpGet]
        public IActionResult AddServiceDetails(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }
            ViewBag.service = PropertyClass.BindDDLService(_datalayer.GetServiceDropdown());
            if (id > 0)
            {
                DataTable dt = _datalayer.GetServicedetailsById(id);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var vendor = new ServiceDetailsDto
                    {
                        ServiceId = Convert.ToInt32(row["ServiceId"]),
                        ServicedId = Convert.ToInt32(row["ServicedId"]),
                        ServiceTittle = row["ServiceTittle"].ToString(),
                        ServiceDescription = row["ServiceDescription"].ToString(),
                        Filename = row["FilePath"].ToString()
                    };
                    return View(vendor);
                }

                return NotFound();
            }
            return View();
        } 
       
        [HttpPost]
        public IActionResult AddServiceDetails(ServiceDetailsDto model)
        { 
            ModelState.Remove("FileName");
            ModelState.Remove("FilePath");
            ModelState.Remove("ServicedId");
            ModelState.Remove("OrderAvailabilty");
            ViewBag.service = PropertyClass.BindDDLService(_datalayer.GetServiceDropdown());
            bool isNameUsed = _datalayer.GetNameserviceDetailsData(model.ServiceId, model.ServiceTittle);

            if (model.ServicedId == 0 && isNameUsed)
            {
                ModelState.AddModelError("ServiceTittle", "This Name is Already Used!");
                return View(model);
            }
            if (model.FilePath == null || model.FilePath.Length == 0)
            {
                if (model.ServicedId != 0 && string.IsNullOrEmpty(model.Filename))
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                }
                if (model.ServicedId == 0)
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                }
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.FilePath != null && model.FilePath.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.FilePath.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.FilePath.CopyTo(stream);
                    }
                    model.Filename = uniqueFileName;
                }

                else if (model.ServicedId != 0 && string.IsNullOrEmpty(model.Filename))
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                    return View(model);
                }
                if (model.ServicedId == 0)
                {
                    model.Action = "1";
                    _datalayer.InsertUpdateServiceDetails(model);
                    TempData["SuccessMessage"] = "Data saved successfully!";
                    return Redirect("/Admin/Master/AddServiceDetails");
                }
                else
                {
                    model.Action = "2";
                    _datalayer.InsertUpdateServiceDetails(model);
                    TempData["SuccessMessage"] = "Data updated successfully!";
                    return Redirect("/Admin/Master/ServiceDetailsList");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
            }

            return View(model);
        }

        public IActionResult DeleteServiceDetails(int id)
        {
            if (id > 0)
            {
                var success = _datalayer.DeleteServiceDetails(id);

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
                TempData["ErrorMessage"] = "Invalid career ID.";
            }
            return Redirect("/Admin/Master/ServiceDetailsList");
        }
         
        public IActionResult ServiceDetailsList()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }

            DataTable result = _datalayer.GetServiceDetailsList();

            if (result == null || result.Rows.Count == 0)
            {
                return View(new List<ServiceDetailsDto>());
            }

            var careers = new List<ServiceDetailsDto>();
            foreach (DataRow row in result.Rows)
            {  careers.Add(new ServiceDetailsDto
            {
                ServiceId = Convert.ToInt32(row["ServiceId"]),
                ServicedId = Convert.ToInt32(row["ServicedId"]),
                ServiceTittle = row["ServiceTittle"].ToString(),
                Action = row["ServiceName"].ToString(),
                ServiceDescription = row["ServiceDescription"].ToString(),
                Filename = row["FilePath"].ToString()
            });
            }

            return View(careers);
        }
        [HttpGet]
        public IActionResult AddService(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            } 
            if (id > 0)
            {
                DataTable dt = _datalayer.GetServiceById(id);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var vendor = new ServiceDto
                    {
                        ServiceId = Convert.ToInt32(row["ServiceId"]),
                        ServiceName = row["ServiceName"].ToString()

                    }; 
                    return View(vendor);
                }

                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddService(ServiceDto model)
        {
            if (model.ServiceId == 0)
            {
                bool isNameUsed = _datalayer.GetNameserviceData(model.ServiceName);
                if (isNameUsed)
                {
                    ModelState.AddModelError("ServiceName", "This Name is Already Used!");
                    return View(model);
                }
            }
            ModelState.Remove("ServiceId");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.ServiceId == 0)
                {
                    model.Action = "1"; 
                    _datalayer.InsertUpdateService(model);
                    TempData["SuccessMessage"] = "Data saved successfully!";
                    return Redirect("/Admin/Master/AddService");
                }
                else
                {
                    model.Action = "2";
                    _datalayer.InsertUpdateService(model);
                    TempData["SuccessMessage"] = "Data updated successfully!";
                    return Redirect("/Admin/Master/ServiceList");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
            }

            return View(model);
        } 
        public IActionResult DeleteService(int id)
        {
            if (id > 0)
            {
                var success = _datalayer.DeleteService(id);

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
                TempData["ErrorMessage"] = "Invalid career ID.";
            }
            return Redirect("/Admin/Master/ServiceList");
        }
        [HttpGet]
        public IActionResult ServiceList()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }

            DataTable result = _datalayer.GetServiceList();

            if (result == null || result.Rows.Count == 0)
            {
                return View(new List<ServiceDto>());
            }

            var careers = new List<ServiceDto>();
            foreach (DataRow row in result.Rows)
            {
                careers.Add(new ServiceDto
                {
                    ServiceId = Convert.ToInt32(row["ServiceId"]),
                    ServiceName = row["ServiceName"].ToString()
                });
            }

            return View(careers);
        }

        [HttpGet]
        public IActionResult AddPftDetails(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }
            ViewBag.service = PropertyClass.BindDDLpft(_datalayer.GetPftMasterDropdown());
            if (id > 0)
            {
                DataTable dt = _datalayer.GetPftdetailsById(id);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var vendor = new PftDetailsDto
                    {
                        PftId = Convert.ToInt32(row["PftId"]),
                        PftDId = Convert.ToInt32(row["PftDId"]),
                        Filename = row["FilePath"].ToString()
                    };
                    return View(vendor);
                }

                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddPftDetails(PftDetailsDto model)
        {
            ModelState.Remove("FileName");
            ModelState.Remove("FilePath");
            ModelState.Remove("PftDId");
            ViewBag.service = PropertyClass.BindDDLpft(_datalayer.GetPftMasterDropdown());
            
            if (model.FilePath == null || model.FilePath.Length == 0)
            {
                if (model.PftDId != 0 && string.IsNullOrEmpty(model.Filename))
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                }
                if (model.PftDId == 0)
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                }
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.FilePath != null && model.FilePath.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.FilePath.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.FilePath.CopyTo(stream);
                    }
                    model.Filename = uniqueFileName;
                }

                else if (model.PftDId != 0 && string.IsNullOrEmpty(model.Filename))
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                    return View(model);
                }
                if (model.PftDId == 0)
                {
                    model.Action = "1";
                    _datalayer.InsertUpdatePftDetails(model);
                    TempData["SuccessMessage"] = "Data saved successfully!";
                    return Redirect("/Admin/Master/AddPftDetails");
                }
                else
                {
                    model.Action = "2";
                    _datalayer.InsertUpdatePftDetails(model);
                    TempData["SuccessMessage"] = "Data updated successfully!";
                    return Redirect("/Admin/Master/PftDetailsList");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
            }

            return View(model);
        }

        public IActionResult DeletePftDetails(int id)
        {
            if (id > 0)
            {
                var success = _datalayer.DeletePftDetails(id);

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
                TempData["ErrorMessage"] = "Invalid career ID.";
            }
            return Redirect("/Admin/Master/PftDetailsList");
        }

        public IActionResult PftDetailsList()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }

            DataTable result = _datalayer.GetPftDetailsList();

            if (result == null || result.Rows.Count == 0)
            {
                return View(new List<PftDetailsDto>());
            }

            var careers = new List<PftDetailsDto>();
            foreach (DataRow row in result.Rows)
            {
                careers.Add(new PftDetailsDto
                {
                    PftId = Convert.ToInt32(row["PftId"]),
                    PftDId = Convert.ToInt32(row["PftDId"]),
                    Filename = row["FilePath"].ToString(),
                    Action = row["PftName"].ToString()
                });
            }

            return View(careers);
        }


        [HttpGet]
        public IActionResult AddPftMaster(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }
            if (id > 0)
            {
                DataTable dt = _datalayer.GetPftMasterById(id);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var vendor = new PftMasterDto
                    {
                        PftId = Convert.ToInt32(row["PftId"]),
                        PftName = row["PftName"].ToString()

                    };
                    return View(vendor);
                }

                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddPftMaster(PftMasterDto model)
        {
            if (model.PftId == 0)
            {
                bool isNameUsed = _datalayer.GetNamePftMasterData(model.PftName);
                if (isNameUsed)
                {
                    ModelState.AddModelError("PftName", "This Name is Already Used!");
                    return View(model);
                }
            }
            ModelState.Remove("PftId");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.PftId == 0)
                {
                    model.Action = "1";
                    _datalayer.InsertUpdatePftMaster(model);
                    TempData["SuccessMessage"] = "Data saved successfully!";
                    return Redirect("/Admin/Master/AddPftMaster");
                }
                else
                {
                    model.Action = "2";
                    _datalayer.InsertUpdatePftMaster(model);
                    TempData["SuccessMessage"] = "Data updated successfully!";
                    return Redirect("/Admin/Master/PftMasterList");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
            }

            return View(model);
        }
        public IActionResult DeletePftMaster(int id)
        {
            if (id > 0)
            {
                var success = _datalayer.DeletePftMaster(id);

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
                TempData["ErrorMessage"] = "Invalid career ID.";
            }
            return Redirect("/Admin/Master/PftMasterList");
        }
        [HttpGet]
        public IActionResult PftMasterList()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }

            DataTable result = _datalayer.GetPftMasterList();

            if (result == null || result.Rows.Count == 0)
            {
                return View(new List<PftMasterDto>());
            }

            var careers = new List<PftMasterDto>();
            foreach (DataRow row in result.Rows)
            {
                careers.Add(new PftMasterDto
                {
                    PftId = Convert.ToInt32(row["PftId"]),
                    PftName = row["PftName"].ToString()
                });
            }

            return View(careers);
        }

        [HttpGet]
        public IActionResult AddOurTeamDetails(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }
            if (id > 0)
            {
                DataTable dt = _datalayer.GetOurTeamdetailsById(id);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var vendor = new TeamDetailsDto
                    {
                        TeamsId = Convert.ToInt32(row["TeamsId"]),
                        Name = row["Name"].ToString(),
                        Designation = row["Designation"].ToString(),
                        Filename = row["FilePath"].ToString()
                    };
                    return View(vendor);
                }

                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddOurTeamDetails(TeamDetailsDto model)
        {
            ModelState.Remove("FileName");
            ModelState.Remove("FilePath");
            ModelState.Remove("TeamsId");
            bool isNameUsed = _datalayer.GetNameOurTeamDetailsData(model.Name,model.Designation);

            if (model.TeamsId == 0 && isNameUsed)
            {
                ModelState.AddModelError("Name", "This Name is Already Used!");
                return View(model);
            }
            if (model.FilePath == null || model.FilePath.Length == 0)
            {
                if (model.TeamsId != 0 && string.IsNullOrEmpty(model.Filename))
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                }
                if (model.TeamsId == 0)
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                }
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.FilePath != null && model.FilePath.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.FilePath.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.FilePath.CopyTo(stream);
                    }
                    model.Filename = uniqueFileName;
                }

                else if (model.TeamsId != 0 && string.IsNullOrEmpty(model.Filename))
                {
                    ModelState.AddModelError("FilePath", "Please choose a file.");
                    return View(model);
                }
                if (model.TeamsId == 0)
                {
                    model.Action = "1";
                    _datalayer.InsertUpdateOurTeamDetails(model);
                    TempData["SuccessMessage"] = "Data saved successfully!";
                    return Redirect("/Admin/Master/AddOurTeamDetails");
                }
                else
                {
                    model.Action = "2";
                    _datalayer.InsertUpdateOurTeamDetails(model);
                    TempData["SuccessMessage"] = "Data updated successfully!";
                    return Redirect("/Admin/Master/OurTeamDetailsList");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
            }

            return View(model);
        }

        public IActionResult DeleteOurTeamDetails(int id)
        {
            if (id > 0)
            {
                var success = _datalayer.DeleteOurTeamDetails(id);

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
                TempData["ErrorMessage"] = "Invalid career ID.";
            }
            return Redirect("/Admin/Master/OurTeamDetailsList");
        }

        public IActionResult OurTeamDetailsList()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Account/Login");
            }
            DataTable result = _datalayer.GetOurTeamDetailsList();
            if (result == null || result.Rows.Count == 0)
            {
                return View(new List<TeamDetailsDto>());
            }
            var careers = new List<TeamDetailsDto>();
            foreach (DataRow row in result.Rows)
            {
                careers.Add(new TeamDetailsDto
                {
                    TeamsId = Convert.ToInt32(row["TeamsId"]),
                    Name = row["Name"].ToString(),
                    Designation = row["Designation"].ToString(),
                    Filename = row["FilePath"].ToString()
                });
            }
            return View(careers);
        }




    }
}
