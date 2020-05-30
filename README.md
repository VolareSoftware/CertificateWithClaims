# CertificateWithClaims
ASP.NET Core 3.1 client certificate authentication sample project.

I needed to test out both claims authentication and client certificate authentication on a .NET Core project, so here it is.

The only page in the web app shows the currently installed client certificate values.

To get a client certificate with some values built and installed:

1) Install OpenSSL, create a certificate authority, create a client certificate from that, and put values you want on both self-signed certficates. Here's a guide that mostly works - https://blog.didierstevens.com/2015/03/30/howto-make-your-own-cert-with-openssl-on-windows/ 
2) Install your CA certificate to your computer Trusted Root Certificate Authorities for your Local Computer using the MMC snap in.
3) Install your client certificate from step 1 using your preferred browser.
4) In IIS, go to Service Certificates and install the CA certificate using the PFX file and password. Put it in the Web Hosting certificates.
5) In the IIS web site, go to bindings and set your SSL certificate to the CA certificate you added in the step above. 
6) Build the solution  and publish to this IIS web site. You should be able to browse the site now. Yes, you will get a warning that the certificate is not trusted because it is self-signed. That's expected.
7) For that IIS web site, go to SSL Settings and check Require SSL and Require client certificate. You have to have SSL running to get a client certificate passed through.
8) Now when you browse to the site again, you should get a prompt to choose your client certificate. Pick the one you created in step 1 and installed into the browser in step 3. You should see a page showing you client certificate values you can use in your app.
