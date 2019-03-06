// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Apps;
using Ejyle.DevAccelerate.Apps.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejyle.DevAccelerate.Tools.Cli.Commands
{
    public class DaCreateFirstAppCommand : IDaConsoleCommand
    {
        public void Execute()
        {
            var appManager = new DaAppProfileManager(new DaAppProfileRepository(new DaAppsDbContext()));

            var apps = appManager.FindAll();

            if (apps == null || apps.Count <= 0)
            {
                Console.Write("Enter the name of your first app: ");
                var firstAppName = Console.ReadLine();

                var app = new DaAppProfile();
                app.Name = firstAppName;
                app.Key = firstAppName;

                appManager.Create(app);
            }
        }
    }
}
