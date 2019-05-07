using LacViet.SurePortal.Identity.Business;
using LacViet.SurePortal.Identity.Domain;
using LacViet.SurePortal.Identity.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartup(typeof(ShoppingEcommerce.Web.Startup))]
namespace ShoppingEcommerce.Web
{
  public partial class Startup
  {
    // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
    public void ConfigureAuth(IAppBuilder appBuilder)
    {
      appBuilder.CreatePerOwinContext(() => new AppBuilderProvider(appBuilder));

      // Enable the application to use a cookie to store information for the signed in user
      // and to use a cookie to temporarily store information about a user logging in with a third party login provider
      // Configure the sign in cookie
      appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/account/login"),
        Provider = new CookieAuthenticationProvider
        {
          // Enables the application to validate the security stamp when the user logs in.
          // This is a security feature which is used when you change a password or add an external login to your account.  
          OnValidateIdentity = SecurityStampValidator
                  .OnValidateIdentity<UserManager, User, Guid>(TimeSpan.FromMinutes(5)
                      , (authenticateUserManager, user) => user.GenerateUserIdentityAsync(authenticateUserManager)
                      , claimsIdentity => Guid.Parse(claimsIdentity.GetUserId()))
        }
      });

      var idProvider = new UserIdProvider();
      GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);
      appBuilder.MapSignalR();
    }
  }
}
