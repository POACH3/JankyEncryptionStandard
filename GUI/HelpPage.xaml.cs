namespace GUI;

public partial class HelpPage : ContentPage
{
	public HelpPage()
	{
		InitializeComponent();
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