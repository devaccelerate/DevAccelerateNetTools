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
    public class DaCreateSystemRolesCommand : IDaCommand
    {
        public DaCommandResult Execute()
        {
            bool success = false;
            var messages = new List<DaCommandResultMessage>();

            string[] systemRoles = { DaRole.GLOBAL_SUPER_ADMIN, DaRole.TENANT_SUPER_ADMIN, DaRole.USER };

            var roleManager = new DaRoleManager(new DaRoleRepository(new DaIdentityDbContext()));
            DaRole role = null;

            foreach (var systemRole in systemRoles)
            {
                role = DaAsyncHelper.RunSync<DaRole>(() => roleManager.FindByNameAsync(systemRole));

                if (role == null)
                {
                    role = new DaRole();
                    role.Name = systemRole;

                    var result = DaAsyncHelper.RunSync<IdentityResult>(() => roleManager.CreateAsync(role));
                    success = result.Succeeded;

                    if (result.Succeeded)
                    {
                        messages.Add(new DaCommandResultMessage(DaCommandResultMessageType.Success, $"Created the {systemRole} role."));
                    }
                    else
                    {
                        if (result.Errors != null && result.Errors.Count() > 0)
                        {
                            foreach(var err in result.Errors)
                            {
                                messages.Add(new DaCommandResultMessage(DaCommandResultMessageType.Error, err));
                            }
                        }
                    }
                }
            }

            return new DaCommandResult(success, messages);
        }
    }
}
