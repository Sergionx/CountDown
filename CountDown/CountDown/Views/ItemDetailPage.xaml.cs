using CountDown.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CountDown.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}