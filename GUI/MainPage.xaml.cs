﻿


using Microsoft.Maui.ApplicationModel.DataTransfer;

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

        private void OnClickedAutoMode(object sender, EventArgs e)
        {
            Application.Current.UserAppTheme = AppTheme.Unspecified;
        }

        private void OnClickedLightMode(object sender, EventArgs e)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }

        private void OnClickedDarkMode(object sender, EventArgs e)
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }


        


    } // end of MainPage
}