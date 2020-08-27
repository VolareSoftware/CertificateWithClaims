using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CertificateWithClaims.Services
{
    public class CertificateValidator
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            // Only for testing
            //return true;

            // Check client certificate thumbprint against certificate thumbprint (using PFX file and password)
            //var cert = new X509Certificate2(Path.Combine("sts_dev_cert.pfx"), "1234");
            //return clientCertificate.Thumbprint == cert.Thumbprint;

            // Check client certificate thumbprint against know thumbprints for root, intermediate, and any other certificates
            var listOfValidThumbprints = new List<string>
            {
                "892d0279498528daea803a08f8c02000c6ae27f0", // root CA certificate
                "7ea4b7585ac981aaa26ccadb495cc3d2469a52a6"  // child/client certificate
            };
            return listOfValidThumbprints.Contains(clientCertificate.Thumbprint.ToLowerInvariant());
        }
    }
}