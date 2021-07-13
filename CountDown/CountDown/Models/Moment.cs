using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CountDown.Models
{
    public class Moment : INotifyPropertyChanged
    {
        public Moment()
        {
        }

        private string id;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private DateTime finishTime;

        public DateTime FinishTime
        {
            get { return finishTime; }
            set
            {
                finishTime = value;
                OnPropertyChanged();
            }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private int importance;

        public int Importance
        {
            get { return importance; }
            set
            {
                importance = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan TimeLeft
        {
            get { return finishTime - DateTime.Now.Date; }
            set { OnPropertyChanged(); }
        }

        public string MessageTimeLeft { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
