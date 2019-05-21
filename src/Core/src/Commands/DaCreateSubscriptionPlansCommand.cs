// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.EnterpriseSecurity.EF;
using Ejyle.DevAccelerate.EnterpriseSecurity.EF.SubscriptionPlans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejyle.DevAccelerate.Tools.Core.Commands
{
    public class DaCreateSubscriptionPlansCommand : IDaCommand
    {
        public DaCommandResult Execute()
        {
            var subscriptionPlanManager = new DaSubscriptionPlanManager(new DaSubscriptionPlanRepository(new DaEnterpriseSecurityDbContext()));

            var sps = subscriptionPlanManager.FindAll();

            var messages = new List<DaCommandResultMessage>();

            if (sps == null || sps.Count <= 0)
            {
                messages.Add(new DaCommandResultMessage( DaCommandResultMessageType.Info, $"{sps.Count} subscription plans exist."));
            }

            return new DaCommandResult(true, messages);
        }
    }
}
