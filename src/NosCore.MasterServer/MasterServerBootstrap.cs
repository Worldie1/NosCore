﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NosCore.MasterServer
{
    public static class MasterServerBootstrap
    {
	    public static void Main()
	    {
		    var host = BuildWebHost(null);
		    // ReSharper disable PossibleNullReferenceException
		    (host.ServerFeatures[typeof(IServerAddressesFeature)] as IServerAddressesFeature).Addresses.Add((host.Services.GetService(typeof(IServerAddressesFeature)) as IServerAddressesFeature)?.Addresses.First());
		    // ReSharper enable PossibleNullReferenceException
            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
	            .ConfigureLogging(logging =>
	            {
		            logging.ClearProviders();
		            logging.AddLog4Net(@"../../configuration/log4net.config");
	            })
                .UseStartup<Startup>()
	            .PreferHostingUrls(true)
	            .SuppressStatusMessages(true)
                .Build();
        }
    }
}