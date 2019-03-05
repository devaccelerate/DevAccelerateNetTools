﻿using Ejyle.DevAccelerate.Subscriptions;
using Ejyle.DevAccelerate.Subscriptions.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejyle.DevAccelerate.Tools.Cli.Commands
{
    public class DaCreateSubscriptionPlansCommand : IDaConsoleCommand
    {
        public void Execute()
        {
            var subscriptionPlanManager = new DaSubscriptionPlanManager(new DaSubscriptionPlanRepository(new DaSubscriptionsDbContext()));

            var sps = subscriptionPlanManager.FindAll();

            if (sps == null || sps.Count <= 0)
            {
                Console.Write($"{sps.Count} subscription plans exist.");
            }
        }
    }
}
