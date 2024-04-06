
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
        cipherTextEditor.Text = _ceasarCipher.Encrypt(plainTextEditor.Text);
    }

    private void OnClickedDecryptBtn(object sender, EventArgs e)
    {
        plainTextEditor.Text = _ceasarCipher.Decrypt(cipherTextEditor.Text);
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
}