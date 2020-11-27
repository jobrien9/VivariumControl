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

                //this is annoying, but I have to unregister, then re-register the toggled event
                //this prevents it from continuously toggling the switch back and forth
                LightToggle.Toggled -= ToggleFrogletLights;

                if (lightStatus.Equals(LightStatus.On))
                {
                    LightToggle.IsToggled = true;
                    OnOffLabel.TextColor = Color.Black;
                    OnOffLabel.Text = "On";
                    FrogletControlPage.BackgroundColor = (Color)Application.Current.Resources["FrogAppBackground"];
                }
                else
                {
                    LightToggle.IsToggled = false;
                    OnOffLabel.TextColor = (Color)Application.Current.Resources["FrogAppSecondary"];
                    OnOffLabel.Text = "Off";
                    FrogletControlPage.BackgroundColor = Color.Black;
                }

                LightToggle.Toggled += ToggleFrogletLights;
            });
        }

        public void ToggleFrogletLights(object sender, ToggledEventArgs e)
        {
            var isOn = ParticleCommunication.ToggleFrogletLight();
            UpdateUI(isOn ? LightStatus.On : LightStatus.Off);
        }
    }
}