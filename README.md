# CertificateWithClaims
ASP.NET Core 3.1 client certificate authentication sample project.

I needed to test out both claims authentication and client certificate authentication on a .NET Core project, so here it is.

The only page in the web app shows the currently installed client certificate values.

Here's how to get this working:

1) Install OpenSSL, create a certificate authority, create a client certificate from that, and put values you want on both self-signed certficates. Here's a guide that mostly works - https://blog.didierstevens.com/2015/03/30/howto-make-your-own-cert-with-openssl-on-windows/ 
2) Install your CA certificate to your computer Trusted Root Certificate Authorities for your Local Computer using the MMC snap in.
3) Install your client certificate from step 1 using your preferred browser.
4) Build and run the solution. It should fail.
5) Make some manual changes to your web server config:
  a) If using IIS Express, change your applicationHost.config `<security><access>` section to `<access sslFlags="Ssl, SslNegotiateCert, SslRequireCert" />` 
  b) If using IIS and not IIS Express, go to SSL Settings and check Require SSL and Require client certificate.
6) Now when you browse to the site again, you should get a prompt to choose your client certificate. Pick the one you created in step 1 and installed into the browser in step 3. 
7) You should see a page showing your client certificate values, the current users values, and the current user claims.
8) In `CertificateValidator.cs`, change the authentication logic to whatever you need. You can check the certificate thumbprint, or that it comes from a specific root certificate thumbprint, or whatever external validation logic you need.
