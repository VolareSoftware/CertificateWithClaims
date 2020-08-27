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

            var certificate = HttpContext.Connection.ClientCertificate;
            if (certificate != null)
            {
                model.CertificateValues.Thumbprint = certificate.Thumbprint;
                model.CertificateValues.FriendlyName = certificate.FriendlyName;
                model.CertificateValues.ValidFrom = certificate.NotBefore;
                model.CertificateValues.ValidTo = certificate.NotAfter;
                model.CertificateValues.IssuerName = certificate.IssuerName.Name;
                model.CertificateValues.SubjectName = certificate.SubjectName.Name;
                model.CertificateValues.UserUpn = certificate.GetNameInfo(X509NameType.UpnName, false);
                model.CertificateValues.UserName = certificate.GetNameInfo(X509NameType.SimpleName, false);
                model.CertificateValues.UserEmail = certificate.GetNameInfo(X509NameType.EmailName, false);
            }

            var identity = ControllerContext.HttpContext.User.Identity;
            if (identity != null)
            {
                model.IdentityValues.IsAuthenticated = identity.IsAuthenticated;
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