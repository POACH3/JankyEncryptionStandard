namespace GUI;

public partial class EncryptionInfoPage : ContentPage
{
	public EncryptionInfoPage()
	{
		InitializeComponent();
	}


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

}