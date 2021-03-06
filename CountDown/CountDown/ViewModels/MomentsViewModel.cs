using CountDown.Models;
using CountDown.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using System.Linq;

namespace CountDown.ViewModels
{
    public class MomentsViewModel : BaseViewModel
    {
        public ObservableCollection<Moment> Moments { get; set; }
        public Command LoadMomentCommand { get; set; }
        public Command AddMomentCommand { get; set; }
        public Command<Moment> MommentTapped { get; set; }

        const string localFileName = "Moments.txt";
        string localpath;

        public MomentsViewModel()
        {   
            Title = "My moments";
            Moments = new ObservableCollection<Moment>();
            localpath = Path.Combine(FileSystem.AppDataDirectory, localFileName);

            LoadMomentCommand = new Command(async () => await ExecuteLoadMomentComand());

            MessagingCenter.Subscribe<MomentDetailPage, Moment>(this, "SaveMoment",
                async (sender, moment) =>
                {
                    Moments.Add(moment);
                    await momentDataStore.AddMomentAsync(moment);

                    //using (var stream = await FileSystem.OpenAppPackageFileAsync(localFileName))
                    //{
                    //    using (var writer = new StreamWriter(stream))
                    //    {
                    //        await writer.WriteLineAsync($"{moment.Id}, {moment.Name}, {moment.Color}, {moment.FinishTime}, {moment.Importance}, {moment.MessageTimeLeft}, {moment.TimeLeft}");
                    //    }
                    //}
                    
                    LoadMomentCommand.Execute(null);
                });

            MessagingCenter.Subscribe<MomentDetailPage, Moment>(this, "UpdateMoment",
                async (sender, moment) =>
                {
                    await momentDataStore.UpdateMomentAsync(moment);
                    LoadMomentCommand.Execute(null);
                });

            MessagingCenter.Subscribe<MomentDetailPage, Moment>(this, "DeleteMoment",
                async (sender, moment) =>
                {
                    await momentDataStore.DeleteMomentAsync(moment);
                    LoadMomentCommand.Execute(null);
                });
        }

        async Task ExecuteLoadMomentComand()
        {
            IsBusy = true;

            try
            {
                Moments.Clear();
                //using (Stream stream = await FileSystem.OpenAppPackageFileAsync(localFileName))
                //{

                //}
                //var appdirectory = FileSystem.AppDataDirectory;
                var moments = await momentDataStore.GetMomentsAsync();
                
                foreach (var moment in moments)
                {
                    if (moment.TimeLeft.Days > 365)
                    {
                        moment.MessageTimeLeft = "more than a year left";
                    }
                    else if (moment.TimeLeft.Days > 30)
                    {
                        moment.MessageTimeLeft = "more than a month left";
                    }
                    else if (moment.TimeLeft.Days < 30 && moment.TimeLeft.Days >= 1)
                    {
                        moment.MessageTimeLeft = $"{Decimal.Round((decimal)moment.TimeLeft.TotalDays)} days left";
                    }
                    else if (moment.TimeLeft.Days < 1)
                    {
                        moment.MessageTimeLeft = "It's today!";
                    }
                    else
                    {
                        moment.MessageTimeLeft = "What the heck just happened?";
                    }
                    Moments.Add(moment);
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
