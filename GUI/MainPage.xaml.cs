namespace GUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}



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
                button.BackgroundColor = Color.FromRgb(100, 100, 100);
            }
        }







    } // end of MainPage
}