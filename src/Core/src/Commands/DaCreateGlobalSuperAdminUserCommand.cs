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

namespace Ejyle.DevAccelerate.Tools.Core.Commands
{
    public class DaCreateGlobalSuperAdminUserCommand : IDaCommand
    {
        public DaCreateGlobalSuperAdminUserCommand()
        { }

        public DaCreateGlobalSuperAdminUserCommand(string email, string password)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if(string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            Email = email;
            Password = password;
        }

        public DaCommandResult Execute()
        {
            bool success = false;
            List<DaCommandResultMessage> messages = new List<DaCommandResultMessage>();

            IdentityResult result = null;

            var userManager = new DaUserManager(new DaUserRepository(new DaIdentityDbContext()));

            var user = DaAsyncHelper.RunSync<DaUser>(() => userManager.FindByNameAsync(DaUser.GLOBAL_SUPER_ADMIN));

            if (user == null)
            {
                user = new DaUser();
                user.UserName = DaUser.GLOBAL_SUPER_ADMIN;
                user.Email = Email;

                result = userManager.Create(user, Password);
                success = result.Succeeded;

                if (result.Succeeded)
                {
                    messages.Add(new DaCommandResultMessage(DaCommandResultMessageType.Info, $"Successfully created the {DaUser.GLOBAL_SUPER_ADMIN} account."));
                    result = DaAsyncHelper.RunSync<IdentityResult>(() => userManager.AddToRoleAsync(user.Id, DaRole.GLOBAL_SUPER_ADMIN));
                }
                else
                {
                    if(result.Errors != null && result.Errors.Count() > 0)
                    {
                        foreach(var err in result.Errors)
                        {
                            messages.Add(new DaCommandResultMessage(DaCommandResultMessageType.Error, err));
                        }
                    }
                }
            }

            return new DaCommandResult(success, messages);
        }

        public string Email
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }
    }
}
