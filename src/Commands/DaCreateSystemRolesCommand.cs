using Ejyle.DevAccelerate.Core;
using Ejyle.DevAccelerate.Identity.AspNet.EF;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejyle.DevAccelerate.Tools.Cli.Commands
{
    public class DaCreateSystemRolesCommand : IDaConsoleCommand
    {
        public void Execute()
        {
            string[] systemRoles = { DaRole.GLOBAL_SUPER_ADMIN, DaRole.TENANT_SUPER_ADMIN, DaRole.USER };

            var roleManager = new DaRoleManager(new DaRoleRepository(new DaAspNetIdentityDbContext()));
            DaRole role = null;

            foreach (var systemRole in systemRoles)
            {
                role = DaAsyncHelper.RunSync<DaRole>(() => roleManager.FindByNameAsync(systemRole));

                if (role == null)
                {
                    role = new DaRole();
                    role.Name = systemRole;

                    var result = DaAsyncHelper.RunSync<IdentityResult>(() => roleManager.CreateAsync(role));

                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Created the {systemRole} role.");
                    }
                    else
                    {
                        if (result.Errors != null && result.Errors.Count() > 0)
                        {
                            throw new Exception(result.Errors.FirstOrDefault());
                        }
                    }
                }
            }
        }
    }
}
