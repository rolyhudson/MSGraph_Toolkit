using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace MSGraph_Auth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        /***************************************************/
        /**** Public  Fields                            ****/
        /***************************************************/

        public string Token = string.Empty;

        public string Error = string.Empty;

        /***************************************************/
        /**** Constructors                              ****/
        /***************************************************/
        public MainWindow()
        {
            InitializeComponent();
        }

        /***************************************************/
        /**** Private  Methods                          ****/
        /***************************************************/
        public async Task<string> Authenticate(int signInMethod)
        {
            AuthenticationResult authResult = null;
            string[] scopes = new string[] { "user.read" };
            var app = App.PublicClientApp;
            //ResultText.Text = string.Empty;
            //TokenInfoText.Text = string.Empty;

            IAccount firstAccount;

            switch (signInMethod)
            {
                // 0: Use account used to signed-in in Windows (WAM)
                case 0:
                    // WAM will always get an account in the cache. So if we want
                    // to have a chance to select the accounts interactively, we need to
                    // force the non-account
                    firstAccount = PublicClientApplication.OperatingSystemAccount;
                    break;

                //  1: Use one of the Accounts known by Windows(WAM)
                case 1:
                    // We force WAM to display the dialog with the accounts
                    firstAccount = null;
                    break;

                //  Use any account(Azure AD). It's not using WAM
                default:
                    var accounts = await app.GetAccountsAsync();
                    firstAccount = accounts.FirstOrDefault();
                    break;
            }

            try
            {
                authResult = await app.AcquireTokenSilent(scopes, firstAccount)
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilent. 
                // This indicates you need to call AcquireTokenInteractive to acquire a token
                System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                try
                {
                    authResult = await app.AcquireTokenInteractive(scopes)
                        .WithAccount(firstAccount)
                        .WithParentActivityOrWindow(new WindowInteropHelper(this).Handle)
                        .WithPrompt(Prompt.SelectAccount)
                        .ExecuteAsync();
                }
                catch (MsalException msalex)
                {
                    Error = $"Error Acquiring Token:{System.Environment.NewLine}{msalex}";
                }
            }
            catch (Exception ex)
            {
                Error = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                return string.Empty;
            }

            if (authResult != null)
            {
                //ResultText.Text = await GetHttpContentWithToken(graphAPIEndpoint, authResult.AccessToken);
                //DisplayBasicTokenInfo(authResult);
                //this.SignOutButton.Visibility = Visibility.Visible;
                Token = authResult.AccessToken;
                return authResult.AccessToken;
            }
            return string.Empty;
        }
    }
}
