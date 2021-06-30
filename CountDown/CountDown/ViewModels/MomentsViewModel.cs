using CountDown.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CountDown.ViewModels
{
    class MomentsViewModel : BaseViewModel
    {
        public ObservableCollection<Moment> Moments { get; set; }
        public Command LoadMomentCommand { get; set; }
        public Command AddMomentCommand { get; set; }
        public Command<Moment> MommentTapped { get; set; }

        public MomentsViewModel()
        {
            Title = "My moments";
            Moments = new ObservableCollection<Moment>();
            LoadMomentCommand = new Command(async () => await ExecuteLoadMomentComand());

        }

        async Task ExecuteLoadMomentComand()
        {
            IsBusy = true;

            try
            {
                Moments.Clear();
                var moments = await momentDataStore.GetMomentsAsync();
                foreach (var moment in moments)
                {
                    Moments.Add(moment);
                }

                foreach (var moment in Moments)
                {
                    if (moment.TimeLeft.Days > 365)
                    {
                        moment.MessageTimeLeft = "more than a year left";
                    }
                    else if (moment.TimeLeft.Days > 30)
                    {
                        moment.MessageTimeLeft = "more than a month left";
                    }
                    else if (moment.TimeLeft.Days < 30 || moment.TimeLeft.Days > 1)
                    {
                        moment.MessageTimeLeft = $"{Decimal.Round((decimal)moment.TimeLeft.TotalDays)} days left";
                    }
                    else if (moment.TimeLeft.Hours > 1)
                    {
                        moment.MessageTimeLeft = $"{Decimal.Round((decimal)moment.TimeLeft.TotalHours)} hours left";
                    }
                    else if (moment.TimeLeft.Hours < 1)
                    {
                        moment.MessageTimeLeft = $"{Decimal.Round((decimal)moment.TimeLeft.TotalMinutes)} minutes left";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
