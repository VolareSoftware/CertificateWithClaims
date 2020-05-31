using System.Collections.Generic;
using System.Security.Claims;

namespace CertificateWithClaims.Models
{
    public class IdentityValues
    {
        public bool IsAuthenticated { get; set; }
        public string AuthenticationType { get; set; }
        public string Name { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}