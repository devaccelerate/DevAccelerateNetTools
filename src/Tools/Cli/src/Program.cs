// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using Ejyle.DevAccelerate.Core;
using Ejyle.DevAccelerate.Core.Configuration;
using Ejyle.DevAccelerate.Core.Data;
using Ejyle.DevAccelerate.EnterpriseSecurity.Apps;
using Ejyle.DevAccelerate.Identity;
using Ejyle.DevAccelerate.Identity.EF;
using Ejyle.DevAccelerate.Tools.Core.Commands;

namespace Ejyle.DevAccelerate.Tools.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Initialization in progress...");

                var configSource = new DaDefaultConfigurationSource();

                DaInitializationManager.AddModuleInitializer(new DaDefaultDataInitializer(configSource));
                DaInitializationManager.AddModuleInitializer(new DaDefaultIdentityInitializer(configSource));
                DaInitializationManager.AddModuleInitializer(new DaDefaultAppsInitializer(configSource));
   
                DaInitializationManager.Execute();

                Console.WriteLine("Initialization successful!");

                Console.Write($"{DaUser.GLOBAL_SUPER_ADMIN} email address: ");
                string email = Console.ReadLine();
                Console.Write($"{DaUser.GLOBAL_SUPER_ADMIN} password: ");
                string password = Console.ReadLine();
                Console.Write("Enter the name of your first app: ");
                var firstAppName = Console.ReadLine();

                var cmdQueue = new Queue();
                cmdQueue.Enqueue(new DaCreateSystemRolesCommand());
                cmdQueue.Enqueue(new DaCreateGlobalSuperAdminUserCommand(email, password));
                cmdQueue.Enqueue(new DaCreateProfilesPlansCommand());
                cmdQueue.Enqueue(new DaCreateAppCommand(firstAppName));
                cmdQueue.Enqueue(new DaCreateDefaultListsCommand());
                cmdQueue.Enqueue(new DaCreateSubscriptionPlansCommand());
                cmdQueue.Enqueue(new DaCreateFinancialsPlansCommand());

                IDaCommand cmd = null;
                DaCommandResult cmdResult = null;

                while(cmdQueue.Count > 0)
                {
                    cmd = cmdQueue.Dequeue() as IDaCommand;
                    cmdResult = cmd.Execute();

                    if (cmdResult.Messages != null && cmdResult.Messages.Count > 0)
                    {
                        foreach (var msg in cmdResult.Messages)
                        {
                            Console.WriteLine($"{msg.MessageType}: {msg.Message}");
                        }
                    }
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong. More information: {ex.Message}");
            }
        }
    }
}
