using ControleDApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDApi.DAL
{
    public class AppUserManager : UserManager<Usuario, int>
    {
        public AppUserManager(IUserStore<Usuario,int> store)
            : base(store)
        { }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> option, IOwinContext context)
        {
            var contexto = context.Get<Context>();

            var store = new CustomUserStore(contexto);

            var userManager = new AppUserManager(
            new CustomUserStore(context.Get<Context>()));

            return userManager;
        }
    }
}