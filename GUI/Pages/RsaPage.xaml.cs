
using Encrypt;

namespace GUI;

public partial class RsaPage : ContentPage
{

    private RSA _rsa;
    private List<string> _keys;
    
    public RsaPage()
	{
		InitializeComponent();
        //BindingContext = this;
	}


    private void OnLoad(object sender, EventArgs e)
    {
        GenerateNewKey();
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


    private async void GenerateNewKey()
    {
        _rsa = new RSA();
        _keys = _rsa.Keys();

        publicKeyEntry.Text = _keys[0];
        privateKeyEntry.Text = _keys[1];
        modulusEntry.Text = _keys[2];
    }

    private void OnClickedGenerateNewKeyBtn(object sender, EventArgs e)
    {
        GenerateNewKey();
    }


    private void OnClickedEncryptBtn(object sender, EventArgs e)
    {
        try
        {
            ciphertextEditor.Text = _rsa.Encrypt(_keys[0], _keys[2], plaintextEditor.Text);
        }
        catch (Exception ex)
        {
            throw new Exception("Keys must be generated before attempting encryption.");
        }
    }

    private void OnClickedDecryptBtn(object sender, EventArgs e)
    {
        try
        {
            plaintextEditor.Text = _rsa.Decrypt(_keys[1], _keys[2], ciphertextEditor.Text);
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public void OnClickedShowHideKeyInfoBtn(object sender, EventArgs e)
    {
        if (showHideKeyInfoBtn.Text == "Hide")
        {
            keyInfo1.IsVisible = false;
            keyInfo2.IsVisible = false;
            keyInfo3.IsVisible = false;
            keyInfo4.IsVisible = false;
            showHideKeyInfoBtn.Text = "Show";

            mainContainer.RowDefinitions[0].Height = new GridLength(40, GridUnitType.Absolute);
        }
        else
        {
            keyInfo1.IsVisible = true;
            keyInfo2.IsVisible = true;
            keyInfo3.IsVisible = true;
            keyInfo4.IsVisible = true;
            showHideKeyInfoBtn.Text = "Hide";

            mainContainer.RowDefinitions[0].Height = new GridLength(230, GridUnitType.Absolute);
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




    private void OnClickedShowHideKey(object sender, EventArgs e)
    {
        if (keyVisibility.Text == "Hide Key")
        {
            privateKeyEntry.IsPassword = true;
            modulusEntry.IsPassword = true;
            keyVisibility.Text = "Show Key";
            Preferences.Default.Set<bool>(nameof(keyVisibility), false);
        }
        else
        {
            privateKeyEntry.IsPassword = false;
            modulusEntry.IsPassword = false;
            keyVisibility.Text = "Hide Key";
            Preferences.Default.Set<bool>(nameof(keyVisibility), true);
        }

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



    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {

    }
}