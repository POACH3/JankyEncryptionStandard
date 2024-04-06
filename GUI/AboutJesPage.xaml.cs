namespace GUI;

public partial class AboutJesPage : ContentPage
{
	public AboutJesPage()
	{
		InitializeComponent();
	}

    public async void GoToHelpPage(object sender, EventArgs e)
    {
        var helpPage = new HelpPage();
        await Navigation.PushAsync(helpPage, false);
    }

    public async void GoToEncryptionInfoPage(object sender, EventArgs e)
    {
        var encryptionInfoPage = new EncryptionInfoPage();
        await Navigation.PushAsync(encryptionInfoPage, false);
    }
}