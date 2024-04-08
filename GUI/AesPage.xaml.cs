
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
    }

    private void ApplyPreferences()
    {
        //if (Preferences.Default.Get<bool>(nameof(keyVisibility), null) == true)
        //{
        //    OnClickedShowHideKey(null, EventArgs.Empty);
        //}
    }


    private void OnClickedGenerateNewKey(object sender, EventArgs e)
    {
        _numBits = int.Parse(picker.Items[picker.SelectedIndex]);

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


    private void OnClickedEncryptBtn(object sender, EventArgs e)
    {
        try
        {
            cipherTextEditor.Text = _aes.Encrypt(plainTextEditor.Text);
        }
        catch (Exception ex)
        {
            throw new Exception("Key must be generated before attempting encryption.");
        }
    }

    private void OnClickedDecryptBtn(object sender, EventArgs e)
    {
        try
        {
            plainTextEditor.Text = _aes.Decrypt(cipherTextEditor.Text);
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

    private async void OnClickedPaste(object sender, EventArgs e)
    {
        plainTextEditor.Text = await Clipboard.GetTextAsync();
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

}