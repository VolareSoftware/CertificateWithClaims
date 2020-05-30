using System.Security.Cryptography.X509Certificates;

namespace CertificateWithClaims.Services
{
    public interface ICertificateValidator
    {
        bool ValidateCertificate(X509Certificate2 clientCertificate);
    }
}