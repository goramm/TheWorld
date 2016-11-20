using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;

namespace TheWorld.Controllers
{

    public class HomeController : Controller
    {
        private IMailService mailService;
        private IConfigurationRoot config;
        private IWorldRepository repository;

        public HomeController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository)
        {
            this.mailService = mailService;
            this.config = config;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var data = repository.GetAllTrips();
            return View(data);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact form";

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "We don't support gmail addresses");
            }
            if (ModelState.IsValid)
            {
                mailService.SendEmail(
                   to: config["MailSettings:To"],
                   from: model.Email,
                   subject: "From The World",
                   body: model.Message
               );

                TempData["UserMessage"] = "Email sent";

                return RedirectToAction(nameof(HomeController.Contact));
            }
           

            return View();
        }

        public IActionResult Tours()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
