using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CertificateWithClaims.Extensions;
using CertificateWithClaims.Services;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CertificateWithClaims
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CertificateValidator>();

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate(options =>
                {
                    options.AllowedCertificateTypes = CertificateTypes.All;
                    options.ValidateCertificateUse = true;
                    options.ValidateValidityPeriod = true;
                    options.RevocationFlag = X509RevocationFlag.EntireChain;
                    options.RevocationMode = X509RevocationMode.NoCheck;
                    options.Events = new CertificateAuthenticationEvents
                    {
                        OnCertificateValidated = context =>
                        {
                            // Check the certificate
                            var certificate = context.ClientCertificate;
                            var certificateValidator = context.HttpContext.RequestServices.GetService<CertificateValidator>();
                            var isCertificateValid = certificateValidator.ValidateCertificate(certificate);


                            if (isCertificateValid)
                            {
                                var claims = new[]
                                {
                                    new Claim(ClaimTypes.NameIdentifier, certificate.Subject, ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                    new Claim(ClaimTypes.Upn, certificate.GetNameInfo(X509NameType.UpnName, false), ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                    new Claim(ClaimTypes.Name, certificate.GetNameInfo(X509NameType.SimpleName, false), ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                    new Claim(ClaimTypes.Email, certificate.GetNameInfo(X509NameType.EmailName, false), ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                    new Claim(ClaimTypes.Locality, certificate.ParseFromSubject("L"), ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                    new Claim(ClaimTypes.StateOrProvince, certificate.ParseFromSubject("S"), ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                    new Claim(ClaimTypes.Country, certificate.ParseFromSubject("C"), ClaimValueTypes.String, context.Options.ClaimsIssuer)
                                };

                                context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                                context.Success();
                            }
                            else
                            {
                                context.Fail("Certificate could not be validated.");
                            }

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            context.Fail("Certificate authentication failed.");

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}