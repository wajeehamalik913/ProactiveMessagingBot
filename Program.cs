// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema.Teams;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.BotBuilderSamples.Helper.NotificationHelper;
using Microsoft.BotBuilderSampless.Data.Models;

namespace Microsoft.BotBuilderSamples
{
    public class Program
    {


        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging((logging) =>
                    {
                        logging.AddDebug();
                        logging.AddConsole();
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
