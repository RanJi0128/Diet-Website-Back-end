using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDApi.Models.Auth
{
    public class CustomUserClaim : IdentityUserClaim<int> {
    }
}