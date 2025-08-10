using Microsoft.AspNetCore.Mvc;
using MindForgeWeb.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace MindForgeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SchoolErpSystem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SchoolErpSystem(BookDemodto model)
        {
            ModelState.Remove("ContactId");
            ModelState.Remove("Phone");

            if (ModelState.IsValid)
            {
                try
                { 
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("itdeskmindforgetechnology@gmail.com", "cepapxczfdelcrgw"),
                        EnableSsl = true,
                    };
                     
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("mindforgetechnology@gmail.com"),
                        Subject = "ERP Demo Request",
                        Body = $@"
                        <h3>ERP Form Submission Details</h3>
                        <p><strong>Name:</strong> {model.Name}</p>
                        <p><strong>Email:</strong> {model.Email}</p>
                        <p><strong>Subject:</strong> {model.Address}</p>
                        <p><strong>Message:</strong> {model.Message}</p>",
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add("sc339599@gmail.com");

                    smtpClient.Send(mailMessage);

                    ViewBag.Message = "Message sent successfully!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Mail send failed: " + ex.Message;
                }
            }

            return View();  
        }

        [HttpGet]
        public IActionResult HRMS()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HRMS(BookDemodto model)
        {
            ModelState.Remove("ContactId");
            ModelState.Remove("Phone");

            if (ModelState.IsValid)
            {
                try
                { 
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("itdeskmindforgetechnology@gmail.com", "cepapxczfdelcrgw"),
                        EnableSsl = true,
                    };
                     
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("itdeskmindforgetechnology@gmail.com"),
                        Subject = "HRMS Demo Request",
                        Body = $@"
                        <h3>ERP Form Submission Details</h3>
                        <p><strong>Name:</strong> {model.Name}</p>
                        <p><strong>Email:</strong> {model.Email}</p>
                        <p><strong>Subject:</strong> {model.Address}</p>
                        <p><strong>Message:</strong> {model.Message}</p>",
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add("mindforgetechnology@gmail.com");

                    smtpClient.Send(mailMessage);

                    ViewBag.Message = "Message sent successfully!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Mail send failed: " + ex.Message;
                }
            }

            return View(); 
        }


        public IActionResult Services()
        {
            return View();
        }






    }
}
