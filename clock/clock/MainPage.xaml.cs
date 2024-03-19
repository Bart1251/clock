using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace clock
{
    public partial class MainPage : TabbedPage
    {
        List<DateTime> savedTimes;
        static TimeSpan alarmTimespan = new TimeSpan(0);
        bool stoperStop = true;
        TimeSpan stoperTime = new TimeSpan(0);
        ObservableCollection<DateTime> stoperTimes = new ObservableCollection<DateTime>();

        public MainPage(ref List<DateTime> savedTimes)
        {
            InitializeComponent();
            this.savedTimes = savedTimes;

            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Time.Text = DateTime.Now.ToString("HH:mm:ss");
                return true;
            });
        }

        private void Save(object sender, EventArgs e)
        {
            savedTimes.Add(DateTime.Now);
        }

        private void OpenSaved(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SavedTimesPage(ref savedTimes));
        }

        private void StartAlarm(object sender, EventArgs e)
        {
            if (alarmTimespan.TotalSeconds > 0) return;
            alarmTimespan = TimeSpan.FromMinutes(double.Parse(AlarmTimeMinutes.Text));
            AlarmTimeMinutes.Text = "";
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                alarmTimespan = alarmTimespan.Subtract(TimeSpan.FromSeconds(1));
                if (alarmTimespan.TotalSeconds < 0)
                {
                    AlarmTimeLeft.Text = "00:00:00";
                    DisplayAlert("Alarm", "Alarm zakończył odliczanie", "OK");
                    return false;
                }
                else
                {
                    AlarmTimeLeft.Text = new DateTime(alarmTimespan.Ticks).ToString("HH:mm:ss");
                    return true;
                }
            });
        }

        private void StartStoper(object sender, EventArgs e)
        {
            if (!stoperStop) return;
            stoperStop = false;
            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    stoperTime = stoperTime.Add(TimeSpan.FromMilliseconds(1));
                    ElapsedTime.Text = new DateTime(stoperTime.Ticks).ToString("HH:mm:ss:ff");
                });
                if (stoperStop)
                {
                    return false;
                }
                else
                    return true;

            });
        }

        private void StopStoper(object sender, EventArgs e)
        {
            stoperStop = true;
        }

        private void ClearStoper(object sender, EventArgs e)
        {
            stoperStop = true;
            stoperTime = new TimeSpan(0);
            ElapsedTime.Text = new DateTime(stoperTime.Ticks).ToString("HH:mm:ss:ff");
            stoperTimes.Clear();
        }

        private void SaveTime(object sender, EventArgs e)
        {
            stoperTimes.Add(new DateTime(stoperTime.Ticks));
            List.ItemsSource = stoperTimes;
        }
    }
}
