using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrogApp
{
    public partial class App : Application
    {
        //save these sunrise/set times at UTC - only set to Eastern in UI
        public static DateTime Sunrise { get; private set; }

        public static DateTime Sunset { get; private set; }

        private const string BASE_SUNRISE_URL = "https://api.sunrise-sunset.org/";
        public const string BASE_PARTICE_URL = "https://api.particle.io/v1/devices/31001c001851373237343331/";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Black
            };

            FindAndSetSunriseSunset();
        }

        private async void FindAndSetSunriseSunset()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_SUNRISE_URL);
                //get sunrise/sunset for Columbus, GA
                var result = await client.GetAsync("json?lat=32.552212&lng=-84.895098&formatted=0");
                if (result.IsSuccessStatusCode)
                {
                    var resultAsJson = JsonConvert.DeserializeObject<SunData>(result.Content.ReadAsStringAsync().Result);

                    var sunResults = resultAsJson.Results;
                    Sunrise = DateTime.Parse(sunResults.Sunrise);
                    Sunset = DateTime.Parse(sunResults.SunSet);
                }
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}