using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CountDown.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About me";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/Sergionx"));
        }

        public ICommand OpenWebCommand { get; }
    }
}