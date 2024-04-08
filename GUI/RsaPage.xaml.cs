
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




    private void GenerateNewKey()
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
            cipherTextEditor.Text = _rsa.Encrypt(_keys[0], _keys[2], plainTextEditor.Text);
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
            plainTextEditor.Text = _rsa.Decrypt(_keys[1], _keys[2], cipherTextEditor.Text);
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    /// <summary>
    ///     Event handler that changes the button color to 
    ///     show it is being clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnPressed(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            button.BackgroundColor = Color.FromRgb(225, 25, 25);
        }
    }


    /// <summary>
    ///     Event handler that changes the button color back
    ///     to the original color after having been clicked on.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnReleased(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            //button.BackgroundColor = Color.FromRgb(100, 100, 100);
            button.BackgroundColor = default;
            //button.BackgroundColor = null;
        }
    }

    public void OnClickedShowHideKeyInfoBtn(object sender, EventArgs e)
    {
        if (showHideKeyInfoButton.Text == "Hide")
        {
            keyInfo1.IsVisible = false;
            keyInfo2.IsVisible = false;
            keyInfo3.IsVisible = false;
            keyInfo4.IsVisible = false;
            showHideKeyInfoButton.Text = "Show";

            mainContainer.RowDefinitions[0].Height = new GridLength(40, GridUnitType.Absolute);
        }
        else
        {
            keyInfo1.IsVisible = true;
            keyInfo2.IsVisible = true;
            keyInfo3.IsVisible = true;
            keyInfo4.IsVisible = true;
            showHideKeyInfoButton.Text = "Hide";

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

    public async void OnClickedCopyPlainTextBtn(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(plainTextEditor.Text);
    }

    public async void OnClickedCopyCipherTextBtn(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(cipherTextEditor.Text);
    }

    private async void OnClickedPastePlainTextBtn(object sender, EventArgs e)
    {
        plainTextEditor.Text = await Clipboard.GetTextAsync();
    }

    private async void OnClickedPasteCipherTextBtn(object sender, EventArgs e)
    {
        cipherTextEditor.Text = await Clipboard.GetTextAsync();
    }




    private void OnClickedShowHideKey(object sender, EventArgs e)
    {
        if (keyVisibility.Text == "Hide Key")
        {
            privateKeyEntry.IsPassword = true;
            modulusEntry.IsPassword = true;
            keyVisibility.Text = "Show Key";
        }
        else
        {
            privateKeyEntry.IsPassword = false;
            modulusEntry.IsPassword = false;
            keyVisibility.Text = "Hide Key";
        }

    }


    private void OnClickedSystemMode(object sender, EventArgs e)
    {
        //Application.Current.UserAppTheme = AppTheme.Unspecified;
        ThemeManager.SetTheme(nameof(GUI.Resources.Themes.Default));
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



    public async void GoToHelpPage(object sender, EventArgs e)
    {
        var helpPage = new HelpPage();
        await Navigation.PushAsync(helpPage, false);
    }

    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {

    }
}