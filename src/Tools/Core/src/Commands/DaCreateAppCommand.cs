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

namespace Ejyle.DevAccelerate.Tools.Core.Commands
{
    public class DaCreateAppCommand : IDaCommand
    {
        public DaCreateAppCommand()
        { }

        public DaCreateAppCommand(string appName)
        {
            if(string.IsNullOrEmpty(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            AppName = appName;
        }

        public DaCommandResult Execute()
        {
            var appManager = new DaAppManager(new DaAppRepository(new DaEnterpriseSecurityDbContext()));

            var app = new DaApp();
            app.Name = AppName;
            app.Key = AppName;

            appManager.Create(app);

            return new DaCommandResult(true, null);
        }

        public string AppName
        {
            get;
            private set;
        }
    }
}
