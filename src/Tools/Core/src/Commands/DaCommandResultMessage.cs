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

namespace Ejyle.DevAccelerate.Tools.Core.Commands
{
    public class DaCommandResultMessage
    {
        public DaCommandResultMessage(DaCommandResultMessageType messageType, string message)
        {
            MessageType = messageType;
            Message = message;
        }

        public DaCommandResultMessageType MessageType
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
}
