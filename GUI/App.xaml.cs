using GUI;
using Microsoft.Maui.Platform;

namespace GUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            //MauiWinUIWindow.Current.UpdateMinimumSize(100, 200);
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);
            
            if (window != null)
            {
                window.Title = "JANKY ENCRYPTION STANDARD";
                window.Height = 450;
                window.Width = 500;
            }

            window.X = -5;
            window.Y = 30;

            window.MinimumHeight = 400;
            window.MinimumWidth = 325;

            return window;
        }
    }
}