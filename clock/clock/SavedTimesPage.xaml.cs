using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace clock
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedTimesPage : ContentPage
    {
        public SavedTimesPage(ref List<DateTime> savedTimes)
        {
            InitializeComponent();
            List.ItemsSource = new List<DateTime>(savedTimes);
        }
    }
}