using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using CertificateWithClaims.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CertificateWithClaims.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new AuthenticationValues();

            var cert = HttpContext.Connection.ClientCertificate;
            if (cert != null)
            {
                model.CertificateValues.Thumbprint = cert.Thumbprint;
                model.CertificateValues.FriendlyName = cert.FriendlyName;
                model.CertificateValues.ValidFrom = cert.NotBefore;
                model.CertificateValues.ValidTo = cert.NotAfter;
                model.CertificateValues.IssuerName = cert.IssuerName.Name;
                model.CertificateValues.SubjectName = cert.SubjectName.Name;
                model.CertificateValues.UserUpn = cert.GetNameInfo(X509NameType.UpnName, false);
                model.CertificateValues.UserName = cert.GetNameInfo(X509NameType.SimpleName, false);
                model.CertificateValues.UserEmail = cert.GetNameInfo(X509NameType.EmailName, false);
            }

            var identity = ControllerContext.HttpContext.User.Identity;
            if (identity!= null)
            {
                model.IdentityValues.IsAuthenticated= identity.IsAuthenticated;
                model.IdentityValues.AuthenticationType = identity.AuthenticationType;
                model.IdentityValues.Name = identity.Name;
                model.IdentityValues.Claims = HttpContext.User.Claims;
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
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}