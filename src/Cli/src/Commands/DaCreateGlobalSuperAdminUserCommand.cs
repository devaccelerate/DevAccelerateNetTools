// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Core;
using Ejyle.DevAccelerate.Identity.EF;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejyle.DevAccelerate.Tools.Cli.Commands
{
    public class DaCreateGlobalSuperAdminUserCommand : IDaConsoleCommand
    {
        public void Execute()
        {
            IdentityResult result = null;

            var userManager = new DaUserManager(new DaUserRepository(new DaIdentityDbContext()));

            var user = DaAsyncHelper.RunSync<DaUser>(() => userManager.FindByNameAsync(DaUser.GLOBAL_SUPER_ADMIN));

            if (user == null)
            {
                Console.Write($"{DaUser.GLOBAL_SUPER_ADMIN} email address: ");
                string email = Console.ReadLine();

                Console.Write($"{DaUser.GLOBAL_SUPER_ADMIN} password: ");
                string password = Console.ReadLine();

                user = new DaUser();
                user.UserName = DaUser.GLOBAL_SUPER_ADMIN;
                user.Email = email;

                result = userManager.Create(user, password);

                if (result.Succeeded)
                {
                    Console.WriteLine($"Successfully created the {DaUser.GLOBAL_SUPER_ADMIN} account.");
                    result = DaAsyncHelper.RunSync<IdentityResult>(() => userManager.AddToRoleAsync(user.Id, DaRole.GLOBAL_SUPER_ADMIN));
                }
                else
                {
                    if(result.Errors != null && result.Errors.Count() > 0)
                    {
                        throw new Exception(result.Errors.FirstOrDefault());
                    }
                }
            }
        }
    }
}
