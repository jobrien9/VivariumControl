using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FrogApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : NavigableContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var titleLabel = (Label)this.FindByName("NavBarText");
            titleLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
        }

        private async void OpenMetricsPage(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Metrics());
        }

        private async void OpenFrogletLightControl(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new FrogletControl());
        }
    }
}