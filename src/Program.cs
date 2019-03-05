using System;
using Ejyle.DevAccelerate.Apps;
using Ejyle.DevAccelerate.Core;
using Ejyle.DevAccelerate.Core.Configuration;
using Ejyle.DevAccelerate.Core.Data;
using Ejyle.DevAccelerate.Identity.AspNet;
using Ejyle.DevAccelerate.Tools.Cli.Commands;

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

                new DaCreateSystemRolesCommand().Execute();
                new DaCreateGlobalSuperAdminUserCommand().Execute();
                new DaCreateFirstAppCommand().Execute();
                new DaCreateDefaultListsCommand().Execute();
                new DaCreateSubscriptionPlansCommand().Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong. More information: {ex.Message}");
            }
        }
    }
}
