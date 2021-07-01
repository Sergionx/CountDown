using CountDown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountDown.Services
{
    class MockMomentDataStore : IMomentDataStore
    {
        private static List<Moment> mockMoments;

        private static List<Moment> MockMoments { get => mockMoments; set => mockMoments = value; }

        static MockMomentDataStore()
        {
            mockMoments = new List<Moment>
            {
                new Moment {Id = "0", Color="Red",
                    FinishTime = DateTime.Now.AddDays(4), Importance= 1, Name= "Sacarse los mocos" },

                new Moment {Id = "1", Color="Pink",
                    FinishTime = DateTime.Now.AddDays(40), Importance= 5, Name= "PAPAPAaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" }
            };
        }

        public async Task<string> AddMomentAsync(Moment moment)
        {
            lock (this)
            {
                moment.Id = mockMoments.Count.ToString();
                mockMoments.Add(moment);
            }
            return await Task.FromResult(moment.Id);
        }

        public async Task<Moment> GetMomentAsync(string id)
        {
            var moment = mockMoments.FirstOrDefault((m) => m.Id == id);

            var returnMoment = CopyMoment(moment);
            return await Task.FromResult(returnMoment);
        }

        public async Task<IList<Moment>> GetMomentsAsync()
        {
            var returnNotes = new List<Moment>();
            foreach (var note in mockMoments)
            {
                returnNotes.Add(note);
            }
            return await Task.FromResult(mockMoments);
        }

        public async Task<bool> UpdateMomentAsync(Moment moment)
        {
            var momentIndex = mockMoments.FindIndex((Moment m) => m.Id == moment.Id);
            var momentFound = momentIndex != -1;
            if (momentFound)
            {
                mockMoments[momentIndex].Color = moment.Color;
                mockMoments[momentIndex].FinishTime = moment.FinishTime;
                mockMoments[momentIndex].Importance = moment.Importance;
                mockMoments[momentIndex].Name = moment.Name;
                mockMoments[momentIndex].TimeLeft = moment.TimeLeft;
            }
            return await Task.FromResult(momentFound);
        }

        private static Moment CopyMoment(Moment moment)
        {
            return new Moment { Color = moment.Color, FinishTime = moment.FinishTime, Id = moment.Id, Importance = moment.Importance, Name = moment.Name, TimeLeft = moment.TimeLeft };
        }

        public async Task<IList<Moment>> DeleteMomentAsync(Moment moment)
        {
            var momentIndex = mockMoments.FindIndex((Moment m) => m.Id == moment.Id);
            var momentFound = momentIndex != -1;
            if (momentFound)
            {
                mockMoments.Remove(moment);
            }
            return await Task.FromResult(mockMoments);
        }
    }
}
