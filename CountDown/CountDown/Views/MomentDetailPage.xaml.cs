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
    public partial class MomentDetailPage : ContentPage
    {
        MomentDetailViewModel viewModel;

        public MomentDetailPage(MomentDetailViewModel viewModel = null)
        {
            InitializeComponent();

            this.viewModel = viewModel ?? new MomentDetailViewModel();
            BindingContext = this.viewModel;
        }

        public void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            Navigation.PopModalAsync();
        }

        public void Save_Clicked(object sender, EventArgs eventArgs)
        {
            var message = viewModel.IsNewMoment ? "SaveMoment" : "UpdateMoment";

            MessagingCenter.Send(this, message, viewModel.Moment);

            Navigation.PopModalAsync();
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            var momentIndex = viewModel.momentDataStore.DeleteMomentAsync(viewModel.Moment);

            MessagingCenter.Send(this, "DeleteMoment", viewModel.Moment);

            Navigation.PopModalAsync();
            
        }
    }
}