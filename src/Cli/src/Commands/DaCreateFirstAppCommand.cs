// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejyle.DevAccelerate.EnterpriseSecurity.Apps;
using Ejyle.DevAccelerate.EnterpriseSecurity.EF;
using Ejyle.DevAccelerate.EnterpriseSecurity.EF.Apps;

namespace Ejyle.DevAccelerate.Tools.Cli.Commands
{
    public class DaCreateFirstAppCommand : IDaConsoleCommand
    {
        public void Execute()
        {
            var appManager = new DaAppManager(new DaAppRepository(new DaEnterpriseSecurityDbContext()));

            var apps = appManager.FindAll();

            if (apps == null || apps.Count <= 0)
            {
                Console.Write("Enter the name of your first app: ");
                var firstAppName = Console.ReadLine();

                var app = new DaApp();
                app.Name = firstAppName;
                app.Key = firstAppName;

                appManager.Create(app);
            }
        }
    }
}
