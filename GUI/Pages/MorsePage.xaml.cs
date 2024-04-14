using Encrypt;

namespace GUI;

public partial class MorsePage : ContentPage
{

    Morse _morse = new Morse();
    
    
    public MorsePage()
	{
		InitializeComponent();
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



    public void OnClickedShowHideKeyInfoBtn(object sender, EventArgs e)
    {
        if (showHideKeyInfoBtn.Text == "Hide")
        {
            keyInfo1.IsVisible = false;
            keyInfo2.IsVisible = false;
            keyInfo3.IsVisible = false;
            showHideKeyInfoBtn.Text = "Show";

            mainContainer.RowDefinitions[0].Height = new GridLength(40, GridUnitType.Absolute);
        }
        else
        {
            keyInfo1.IsVisible = true;
            keyInfo2.IsVisible = true;
            keyInfo3.IsVisible = true;
            showHideKeyInfoBtn.Text = "Hide";

            mainContainer.RowDefinitions[0].Height = new GridLength(180, GridUnitType.Absolute);
        }
    }

    private void OnValueChangedFrequencySlider(object sender, EventArgs e)
    {
        _morse.SetFrequency((float)frequencySlider.Value);
        frequencyLabel.Text = Math.Round(frequencySlider.Value).ToString() + " MHz";
    }

    private void OnValueChangedSpeedSlider(object sender, EventArgs e)
    {
        _morse.SetSpeed((int)speedSlider.Value);
        wpmLabel.Text = Math.Round(speedSlider.Value).ToString() + " WPM";
    }

    private void OnCheckedChangedFarnsworthTimingCheckBox(object sender, EventArgs e)
    {
        //if (farnsworthTimingCheckBox.IsChecked == true)
        //{
        //    _morse.SetFarnsworthTiming(true);
        //}
        //else
        //{
        //    _morse.SetFarnsworthTiming(false);
        //}
        
    }

    private async void OnClickedPlayMorseBtn(object sender, EventArgs e)
    {
        try
        {
            _morse.PlayMessageTones(ciphertextEditor.Text);
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
            ciphertextEditor.Text = _morse.Encrypt(plaintextEditor.Text);
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
            plaintextEditor.Text = _morse.Decrypt(ciphertextEditor.Text);
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", ex.Message, "OKAY");
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