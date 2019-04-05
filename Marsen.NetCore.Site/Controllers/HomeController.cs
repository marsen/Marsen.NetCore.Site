using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElmahCore;
using Microsoft.AspNetCore.Mvc;
using Marsen.NetCore.Site.Models;
using Microsoft.Extensions.Logging;

namespace Marsen.NetCore.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Index()
        {
            this._logger.Log(LogLevel.Trace,"HomeController Trace:0");
            this._logger.Log(LogLevel.Debug,"HomeController Debug:1");
            this._logger.Log(LogLevel.Information,"HomeController Information:2");
            this._logger.Log(LogLevel.Warning,"HomeController Warning:3");
            this._logger.Log(LogLevel.Error,"HomeController Error:4");
            this._logger.Log(LogLevel.Critical,"HomeController Critical:5");
            this._logger.Log(LogLevel.None,"HomeController None:6");
            HttpContext.RiseError(new Exception("Test for Elmah"));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
