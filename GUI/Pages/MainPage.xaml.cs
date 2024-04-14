


using Microsoft.Maui.ApplicationModel.DataTransfer;
using GUI.Resources.Themes;

namespace GUI
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}





        





        // --- NAVIGATION ---



        public async void GoToCeasarCipherPage(object sender, EventArgs e)
        {
            var ceasarCipherPage = new CeasarCipherPage();
            await Navigation.PushAsync(ceasarCipherPage, false);
        }

        public async void GoToMorseCodePage(object sender, EventArgs e)
        {
            var morseCodePage = new MorsePage();
            await Navigation.PushAsync(morseCodePage, false);
        }

        public async void GoToRsaPage(object sender, EventArgs e)
        {
            var rsaPage = new RsaPage();
            await Navigation.PushAsync(rsaPage, false);
        }

        public async void GoToAesPage(object sender, EventArgs e)
        {
            var aesPage = new AesPage();
            await Navigation.PushAsync(aesPage, false);
        }


        public async void GoToHelpPage(object sender, EventArgs e)
        {
            var helpPage = new HelpPage();
            await Navigation.PushAsync(helpPage, false);
        }

        public async void GoToAboutJesPage(object sender, EventArgs e)
        {
            var aboutJesPage = new AboutJesPage();
            await Navigation.PushAsync(aboutJesPage, false);
        }

        public async void GoToEncryptionInfoPage(object sender, EventArgs e)
        {
            var encryptionInfoPage = new EncryptionInfoPage();
            await Navigation.PushAsync(encryptionInfoPage, false);
        }






        // --- SETTINGS ---


        private void OnClickedShowHideKey(object sender, EventArgs e)
        {
            //if (keyVisibility.Text == "Hide Key")
            //{
            //    keyEntry.IsPassword = true;
            //    keyVisibility.Text = "Show Key";
            //}
            //else
            //{
            //    keyEntry.IsPassword = false;
            //    keyVisibility.Text = "Hide Key";
            //}

        }

        private void OnClickedSystemMode(object sender, EventArgs e)
        {
            //Application.Current.UserAppTheme = AppTheme.Unspecified;
            ThemeManager.SetTheme(nameof(GUI.Resources.Themes.Dark));
        }

        private void OnClickedLightMode(object sender, EventArgs e)
        {
            //Application.Current.UserAppTheme = AppTheme.Light;
            ThemeManager.SetTheme("Light");
        }

        private void OnClickedDarkMode(object sender, EventArgs e)
        {
            //Application.Current.UserAppTheme = AppTheme.Dark;
            ThemeManager.SetTheme(nameof(GUI.Resources.Themes.Dark));
        }

        private void OnClickedNightMode(object sender, EventArgs e)
        {
            //Application.Current.UserAppTheme = AppTheme.Unspecified;
            //Application.Current.UserAppTheme = Application.Current.RequestedTheme;

            //Preferences.Set("Theme", "Night");


            ThemeManager.SetTheme(nameof(GUI.Resources.Themes.Night));

        }





    } // end of MainPage
}