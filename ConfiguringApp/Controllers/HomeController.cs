using ConfiguringApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ConfiguringApp.Controllers
{
    public class HomeController : Controller
    {
        private UptimeService uptime;
        private ILogger<HomeController> logger;

        public HomeController(UptimeService uptime, ILogger<HomeController> logger)
        {
            this.uptime = uptime;
            this.logger = logger;
        }

        public ViewResult Index(bool throwException = false)
        {
            logger.LogDebug($" Handler {Request.Path} at time {uptime.Uptime}");

            if (throwException)
            {
                throw new NullReferenceException();
            }

            return View(new Dictionary<string, string>
            {
                ["Message"] = "This is the Index action",
                ["Uptime"] = $"{uptime.Uptime}ms"
            });
        }

        public ViewResult Error() => View(nameof(Index), new Dictionary<string, string>
        {
            ["Message"] = "This is the Error action"
        });

        //public IActionResult Index() => View(new Dictionary<string, string>
        //{
        //    ["Message"] = "This is the Index action",
        //    ["Uptime"] = $"{uptime.Uptime}ms"
        //});
    }
}
