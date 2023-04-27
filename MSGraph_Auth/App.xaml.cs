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
        static App()
        {
            CreateApplication();
        }

        public static void CreateApplication()
        {
            var builder = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{Tenant}")
                .WithDefaultRedirectUri();

            builder.WithWindowsBroker(true);
            _clientApp = builder.Build();
            //TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }

        private static string ClientId = "394b3057-2190-4686-92fa-dfbffdbd4bb5";

        private static string Tenant = "8258b4d0-de8b-4ef3-a120-1dec3cd88f75";

        private static string Instance = "https://login.microsoftonline.com/";

        private static IPublicClientApplication _clientApp;

        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
    }
    
}
