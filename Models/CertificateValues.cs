using System;

namespace CertificateWithClaims.Models
{
    public class CertificateValues
    {
        public string Thumbprint { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string IssuerName { get; set; }
        public string SubjectName { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}