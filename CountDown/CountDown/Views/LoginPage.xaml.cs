using CountDown.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountDown.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        public LoginPage()
        {
            viewModel = new LoginViewModel();
            BindingContext = viewModel;

            viewModel.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            InitializeComponent();

            //Email.Completed += (object sender, EventArgs e) =>
            //{
            //    Password.Focus();
            //};

            //Password.Completed += (object sender, EventArgs e) =>
            //{
            //    viewModel.SubmitCommand.Execute(null);
            //};
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Email) || !viewModel.Email.Contains("@") || string.IsNullOrWhiteSpace(viewModel.Password))
            {
                viewModel.DisplayInvalidLoginPrompt();
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
        }
    }
}