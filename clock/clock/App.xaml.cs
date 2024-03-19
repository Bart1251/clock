using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace clock
{
    public partial class App : Application
    {
        public List<DateTime> savedTimes = new List<DateTime>();
        private readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "dates.txt");
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage(ref savedTimes));
        }

        protected override void OnStart()
        {
            savedTimes.Clear();
            if (!File.Exists(path)) return;
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
                savedTimes.Add(DateTime.Parse(line));
        }

        protected override void OnSleep()
        {
            List<string> lines = new List<string>();
            foreach (DateTime time in savedTimes)
                lines.Add(time.ToString());
            File.WriteAllLines(path, lines);
        }

        protected override void OnResume()
        {
        }
    }
}
