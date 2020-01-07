// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using Ejyle.DevAccelerate.Financials.EF;
using Ejyle.DevAccelerate.Financials.EF.Payment;
using Ejyle.DevAccelerate.Profiles.EF;
using Ejyle.DevAccelerate.Profiles.EF.UserProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejyle.DevAccelerate.Tools.Core.Commands
{
    public class DaCreateFinancialsPlansCommand : IDaCommand
    {
        public DaCommandResult Execute()
        {
            var paymentMethodManager = new DaPaymentMethodManager(new DaPaymentMethodRepository(new DaFinancialsDbContext()));
            paymentMethodManager.FindById(1);

            var accountManager = new DaAccountManager(new DaAccountRepository(new DaFinancialsDbContext()));
            accountManager.FindById(1);

            var messages = new List<DaCommandResultMessage>();
            messages.Add(new DaCommandResultMessage(DaCommandResultMessageType.Info, $"Executed a query against the financials module."));

            return new DaCommandResult(true, messages);
        }
    }
}
