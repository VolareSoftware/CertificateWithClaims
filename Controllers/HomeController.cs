using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using CertificateWithClaims.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CertificateWithClaims.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var model = new CertificateValues();
            
            var cert = HttpContext.Connection.ClientCertificate;
            if (cert != null)
            {
                model.Thumbprint = cert.Thumbprint;
                model.ValidFrom = cert.NotBefore;
                model.ValidTo = cert.NotAfter;
                model.IssuerName = cert.IssuerName.Name;
                model.SubjectName  = cert.SubjectName.Name;
                model.UserName = cert.GetNameInfo(X509NameType.SimpleName, false);
                model.UserEmail = cert.GetNameInfo(X509NameType.EmailName, false);
            }

            return View(model);
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