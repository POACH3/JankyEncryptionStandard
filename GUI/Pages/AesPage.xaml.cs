
using Encrypt;
using System.Security.Cryptography;

namespace GUI;

public partial class AesPage : ContentPage
{

    AES _aes;
    int _numBits;

    public AesPage()
	{
		InitializeComponent();
        ApplyPreferences();
    }

    private void ApplyPreferences()
    {
        //if (Preferences.Default.Get<bool>(nameof(keyVisibility), null) == true)
        //{
        //    OnClickedShowHideKey(null, EventArgs.Empty);
        //}
    }

    public async void GoToHelpPage(object sender, EventArgs e)
    {
        var helpPage = new HelpPage();
        await Navigation.PushAsync(helpPage, true);
    }

    public async void GoToEncryptionInfoPage(object sender, EventArgs e)
    {
        var encryptionInfoPage = new EncryptionInfoPage();
        await Navigation.PushAsync(encryptionInfoPage, true);
    }

    public async void GoToAboutJesPage(object sender, EventArgs e)
    {
        var aboutJesPage = new AboutJesPage();
        await Navigation.PushAsync(aboutJesPage, true);
    }

    private void OnClickedGenerateNewKey(object sender, EventArgs e)
    {
        _numBits = int.Parse(keyTypePicker.Items[keyTypePicker.SelectedIndex]);

        _aes = new AES(_numBits);

        _aes.GenerateNewKey(_numBits);

        keyEntry.Text = (string)_aes.GetKey("base64");
    }

    private void OnClickedShowHideKey(object sender, EventArgs e)
    {
        if (keyVisibility.Text == "Hide Key")
        {
            keyEntry.IsPassword = true;
            keyVisibility.Text = "Show Key";
        }
        else
        {
            keyEntry.IsPassword = false;
            keyVisibility.Text = "Hide Key";
        }

    }

    public void OnClickedShowHideKeyInfoBtn(object sender, EventArgs e)
    {
        if (showHideKeyInfoBtn.Text == "Hide")
        {
            keyInfo1.IsVisible = false;
            keyInfo2.IsVisible = false;
            showHideKeyInfoBtn.Text = "Show";

            mainContainer.RowDefinitions[0].Height = new GridLength(40, GridUnitType.Absolute);
        }
        else
        {
            keyInfo1.IsVisible = true;
            keyInfo2.IsVisible = true;
            showHideKeyInfoBtn.Text = "Hide";

            mainContainer.RowDefinitions[0].Height = new GridLength(150, GridUnitType.Absolute);
        }
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

    private void OnClickedEncryptBtn(object sender, EventArgs e)
    {
        try
        {
            ciphertextEditor.Text = _aes.Encrypt(plaintextEditor.Text);
        }
        catch (Exception ex)
        {
            throw new Exception("Key must be generated before attempting encryption.");
        }
    }

    private async void OnClickedDecryptBtn(object sender, EventArgs e)
    {
        try
        {
            plaintextEditor.Text = _aes.Decrypt(ciphertextEditor.Text);
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", ex.Message, "OKAY");
        }
    }




    public async void OnClickedCopyUseKeyBtn(object sender, EventArgs e)
    {
        //if ()
        //{
        //    await Clipboard.SetTextAsync(keyEntry.Text);
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
        //ThemeManager.SetTheme(nameof(GUI.Resources.Themes.Dark));
        ThemeManager.SetTheme("Dark");
    }

    private void OnClickedNightMode(object sender, EventArgs e)
    {
        //Application.Current.UserAppTheme = AppTheme.Unspecified;
        //Application.Current.UserAppTheme = Application.Current.RequestedTheme;

        //Preferences.Set("Theme", "Night");


        ThemeManager.SetTheme(nameof(GUI.Resources.Themes.Night));

    }

}