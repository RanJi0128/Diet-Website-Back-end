using ControleDApi.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDApi.DAL
{
    public class AppSignInManager : SignInManager<Usuario, int>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        { }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> option, IOwinContext context)
        {
            var manager = context.GetUserManager<AppUserManager>();

            var sign = new AppSignInManager(manager, context.Authentication);

            return sign;
        }
    }
}