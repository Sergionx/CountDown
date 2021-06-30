using CountDown.Models;
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
    public partial class MomentsPage : ContentPage
    {
        MomentsViewModel viewModel;
        public string messageTimeLeft { get; set; }

        public MomentsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MomentsViewModel();
        }

        async void OnMomentSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Moment moment = args.SelectedItem as Moment;
            if (moment == null)
            {
                return;
            }
            //Poner MomentViewModel
            //await Navigation.PushModalAsync()
        }
        
        async void AddMoment_Clicked(object sender, EventArgs e)
        {
            //Poner MomentDetail
            await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage()));
        }
    }
}