using ControleDApi.Models;
using ControleDApi.Models.Auth;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ControleDApi.DAL
{
    public class CustomUserStore : UserStore<Usuario, CustomRole, int,
     CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(Context context)
            : base(context)
        {
        }
    }
}