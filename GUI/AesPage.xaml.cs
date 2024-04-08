namespace GUI;

public partial class AesPage : ContentPage
{
	public AesPage()
	{
		InitializeComponent();
	}



    //public void OnClickedShowHideKeyInfoBtn(object sender, EventArgs e)
    //{
    //    if (showHideKeyInfoButton.Text == "Hide")
    //    {
    //        keyInfo1.IsVisible = false;
    //        keyInfo2.IsVisible = false;
    //        showHideKeyInfoButton.Text = "Show";

    //        mainContainer.RowDefinitions[0].Height = new GridLength(40, GridUnitType.Absolute);
    //    }
    //    else
    //    {
    //        keyInfo1.IsVisible = true;
    //        keyInfo2.IsVisible = true;
    //        showHideKeyInfoButton.Text = "Hide";

    //        mainContainer.RowDefinitions[0].Height = new GridLength(150, GridUnitType.Absolute);
    //    }
    //}

    ///// <summary>
    /////     Event handler that changes the button color to 
    /////     show it is being clicked.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //public void OnPressed(object sender, EventArgs e)
    //{
    //    if (sender is Button button)
    //    {
    //        button.BackgroundColor = Color.FromRgb(225, 25, 25);
    //    }
    //}


    ///// <summary>
    /////     Event handler that changes the button color back
    /////     to the original color after having been clicked on.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //public void OnReleased(object sender, EventArgs e)
    //{
    //    if (sender is Button button)
    //    {
    //        //button.BackgroundColor = Color.FromRgb(100, 100, 100);
    //        button.BackgroundColor = default;
    //        //button.BackgroundColor = null;
    //    }
    //}
    //public async void OnClickedCopyUseKeyBtn(object sender, EventArgs e)
    //{
    //    //if ()
    //    //{
    //    //    await Clipboard.SetTextAsync(keyEntry.Text);
    //    //}


    //}

    //public async void OnClickedCopyPlainTextBtn(object sender, EventArgs e)
    //{
    //    await Clipboard.SetTextAsync(plainTextEditor.Text);
    //}

    //public async void OnClickedCopyCipherTextBtn(object sender, EventArgs e)
    //{
    //    await Clipboard.SetTextAsync(cipherTextEditor.Text);
    //}

    //private async void OnClickedPaste(object sender, EventArgs e)
    //{
    //    plainTextEditor.Text = await Clipboard.GetTextAsync();
    //}




}