using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Desktop;
using Microsoft.Identity.Client.Broker;

namespace MSGraph_Auth
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //static App()
        //{
        //    CreateApplication();
        //}

        public static void CreateApplication(string ClientId, string Tenant, string Instance)
        {
            var builder = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{Tenant}")
                .WithDefaultRedirectUri();

            builder.WithWindowsBroker(true);
            _clientApp = builder.Build();
            //TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }

        private static IPublicClientApplication _clientApp;

        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
    }
    
}
