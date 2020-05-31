namespace CertificateWithClaims.Models
{
    public class AuthenticationValues
    {
        public CertificateValues CertificateValues { get; set; }
        public IdentityValues IdentityValues { get; set; }

        public AuthenticationValues()
        {
            CertificateValues = new CertificateValues();
            IdentityValues = new IdentityValues();
        }
    }
}