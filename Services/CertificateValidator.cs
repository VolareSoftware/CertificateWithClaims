using System.Security.Cryptography.X509Certificates;

namespace CertificateWithClaims.Services
{
    public class CertificateValidator : ICertificateValidator
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            return true;

            // Check client certificate thumbprint against certificate thumbprint (using PFX file and password)
            //var cert = new X509Certificate2(Path.Combine("sts_dev_cert.pfx"), "1234");
            //return clientCertificate.Thumbprint == cert.Thumbprint;

            // Check client certificate thumbprint against know thumbprints for root, intermediate, and any other certificates
            //var listOfValidThumbprints = new List<string>
            //{
            //    "8d4b4a4b102b0624e1a3ac805cdd694a4a851cf0", // root CA certificate
            //    "fdd088f8baff2a263008649101bc1f2004cd22d1", // intermediate certificate
            //    "f618c404e7ad4a52149de30fafe09c97f4064e12"  // child/client certificate
            //};
            //return listOfValidThumbprints.Contains(clientCertificate.Thumbprint);
        }
    }
}