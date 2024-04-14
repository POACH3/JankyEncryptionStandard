
using Encrypt;


namespace GUI;

public partial class CeasarCipherPage : ContentPage
{

    private CeasarCipher _ceasarCipher;
    
    
    public CeasarCipherPage()
	{
		InitializeComponent();
	}




    private void CreateNewKey(int offset)
    {
        _ceasarCipher = new CeasarCipher(offset);
    }






    // --- OTHER ACTION HANDLERS ---


    private async void OnUnfocusedShiftEntry(object sender, EventArgs e)
    {
        int shift;

        if (int.TryParse(keyEntry.Text, out shift)) {
            if (shift > 0 && shift < 95)
            {
                CreateNewKey(int.Parse(keyEntry.Text));
            }
            else
            {
                keyEntry.Text = "";
                await DisplayAlert("INVALID KEY", "Enter a valid offset that is between 1 and 94 (inclusive).", "OKAY");
            }
        }
        else
        {
            keyEntry.Text = "";
            await DisplayAlert("INVALID KEY", "Enter a valid offset that is between 1 and 94 (inclusive).", "OKAY");
        }
        
    }

    private void OnClickedEncryptBtn(object sender, EventArgs e)
    {
        ciphertextEditor.Text = _ceasarCipher.Encrypt(plaintextEditor.Text);
    }

    private void OnClickedDecryptBtn(object sender, EventArgs e)
    {
        plaintextEditor.Text = _ceasarCipher.Decrypt(ciphertextEditor.Text);
    }




    // --- SETTINGS ---


    public void ShowHideKeyInfoButtonClicked(object sender, EventArgs e)
    {
        if (showHideKeyInfoButton.Text == "Hide")
        {
            keyInfo1.IsVisible = false;
            showHideKeyInfoButton.Text = "Show";

            mainContainer.RowDefinitions[0].Height = new GridLength(40, GridUnitType.Absolute);
        }
        else
        {
            keyInfo1.IsVisible = true;
            showHideKeyInfoButton.Text = "Hide";

            mainContainer.RowDefinitions[0].Height = new GridLength(100, GridUnitType.Absolute);
        }
    }

    public async void OnClickedCopyUseKeyBtn(object sender, EventArgs e)
    {
        //if ()
        //{
        //    await Clipboard.SetTextAsync(keyEntry.Text);
        //}
        
        
    }

    public async void OnClickedCopyPlaintextBtn(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(plaintextEditor.Text);
    }

    public async void OnClickedCopyCiphertextBtn(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(ciphertextEditor.Text);
    }

    private async void OnClickedPastePlaintextBtn(object sender, EventArgs e)
    {
        plaintextEditor.Text = await Clipboard.GetTextAsync();
    }

    private async void OnClickedPasteCiphertextBtn(object sender, EventArgs e)
    {
        ciphertextEditor.Text = await Clipboard.GetTextAsync();
    }




    // --- NAVIGATION ---


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



}