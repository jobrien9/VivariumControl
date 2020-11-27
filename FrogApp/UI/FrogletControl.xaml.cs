using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrogApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrogletControl : NavigableContentPage
    {
        public FrogletControl()
        {
            InitializeComponent();

            //figure out if the froglet tank is on or off
            Task.Run(() =>
            {
                UpdateUI(ParticleCommunication.GetFrogletLightStatus());
            });
        }

        public void UpdateUI(LightStatus lightStatus)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                LightToggle.IsEnabled = true;
                LightToggle.IsVisible = true;
                OnOffLabel.IsVisible = true;
                Spinner.IsVisible = false;
                Spinner.IsRunning = false;

                if (lightStatus.Equals(LightStatus.On))
                {
                    //LightToggle.IsToggled = true;
                    Spinner.IsVisible = false;
                    OnOffLabel.TextColor = Color.Black;
                    OnOffLabel.Text = "On";
                    FrogletControlPage.BackgroundColor = (Color)Application.Current.Resources["FrogAppBackground"];
                }
                else
                {
                    //LightToggle.IsToggled = false;
                    Spinner.IsVisible = false;
                    OnOffLabel.TextColor = (Color)Application.Current.Resources["FrogAppSecondary"];
                    OnOffLabel.Text = "Off";
                    //FrogletControlPage.BackgroundColor = Color.Black;
                }
            });
        }

        public void ToggleFrogletLights(object sender, ToggledEventArgs e)
        {
            var isOn = ParticleCommunication.ToggleFrogletLight();
            UpdateUI(isOn ? LightStatus.On : LightStatus.Off);
        }
    }
}