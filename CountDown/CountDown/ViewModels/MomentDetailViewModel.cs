using CountDown.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountDown.ViewModels
{
    public class MomentDetailViewModel : BaseViewModel
    {
        public Moment Moment { get; set; }
        public IList<Moment> Moments { get; set; }
        public IList<int> Importances => new List<int> { 1, 2, 3, 4, 5 };


        // True when adding a new Moment; false when editing existing Moment
        public bool IsNewMoment { get; set; }

        public MomentDetailViewModel(Moment moment = null)
        {
            IsNewMoment = moment == null;

            Title = IsNewMoment ? "Add moment" : "Edit moment";

            InitalizeMoments();
            Moment = moment ?? new Moment();
        }

        async private void InitalizeMoments()
        {
            Moments = await momentDataStore.GetMomentsAsync();
        }
    }
}
