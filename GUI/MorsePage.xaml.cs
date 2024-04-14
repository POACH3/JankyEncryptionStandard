using Encrypt;

namespace GUI;

public partial class MorsePage : ContentPage
{

    Morse _morse = new Morse();
    
    
    public MorsePage()
	{
		InitializeComponent();
	}

    private async void OnClickedPlayMorseBtn(object sender, EventArgs e)
    {
        try
        {
            _morse.PlayMessageTones(cipherTextEditor.Text);
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", ex.Message, "OKAY");
        }
    }


    private async void OnClickedEncryptBtn(object sender, EventArgs e)
    {
        try
        {
            cipherTextEditor.Text = _morse.Encrypt(plainTextEditor.Text);
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", ex.Message, "OKAY");
        }
    }

    private async void OnClickedDecryptBtn(object sender, EventArgs e)
    {
        try
        {
            plainTextEditor.Text = _morse.Decrypt(cipherTextEditor.Text);
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", ex.Message, "OKAY");
        }
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


}