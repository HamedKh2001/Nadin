using Nadin.Domain.Entities;
using System.Collections.Generic;

namespace Nadin.Infrastucture.Persistence
{
    internal static class SystemRolesInitializer
    {
        internal static List<Role> Roles = new List<Role>
        {
            #region Admins
            
            new Role{ Title ="Admins", DisplayTitle ="کاربران", Discriminator = "Menu"},
            new Role{ Title ="SSO-Admin", DisplayTitle ="ویرایش دسترسی کاربر به گروه", Discriminator = "Admins"},

            #endregion

        };
    }
}