using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrogApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Metrics : NavigableContentPage
    {
        private static readonly Color MainColor = (Color)Application.Current.Resources["FrogAppSecondary"];

        public Metrics()
        {
            InitializeComponent();
            SetupMetrics();
        }

        private void SetupMetrics()
        {
            MetricsStack.Children.Add(GetTextFrame("Sunrise:", textValue: App.Sunrise.ToLocalTime().ToString()));
            MetricsStack.Children.Add(GetTextFrame("Sunset:", textValue: App.Sunset.ToLocalTime().ToString()));
            MetricsStack.Children.Add(GetLightStatusFrame("Froglet Light:", ParticleCommunication.GetFrogletLightStatus));
            MetricsStack.Children.Add(GetLightStatusFrame("Vivarium Light:", ParticleCommunication.GetVivariumLightStatus));
            MetricsStack.Children.Add(GetTextFrame("Temperature:", textValue: "---"));
        }

        private Frame GetLightStatusFrame(string labelText, Func<LightStatus> lightStatusFunction)
        {
            var frame = GetTextFrame(labelText, "---");
            var textLabel = (Label)((StackLayout)frame.Content).Children.FirstOrDefault(x => x.ClassId == "TextLabel");
            textLabel.IsVisible = false;
            var titleLabel = (Label)((StackLayout)frame.Content).Children.FirstOrDefault(x => x.ClassId == "TitleLabel");
            var spinner = (ActivityIndicator)((StackLayout)frame.Content).Children.FirstOrDefault(x => x.ClassId == "Spinner");
            spinner.IsRunning = true;
            spinner.IsVisible = true;

            //the API calls take a second, so do them in a new thread
            Task.Run(() =>
            {
                //pull the light status out of the function via an API call
                var lightStatus = lightStatusFunction();
                var textValue = "---";
                var backgroundColor = Color.LightGray;
                var textColor = Color.Black;
                switch (lightStatus)
                {
                    case LightStatus.On:
                        textValue = "On";
                        backgroundColor = MainColor;
                        textColor = Color.AntiqueWhite;
                        break;

                    case LightStatus.Twilight:
                        textValue = "Twilight";
                        backgroundColor = Color.Blue;
                        textColor = Color.White;
                        break;

                    default:
                        textValue = "Off";
                        textColor = MainColor;
                        backgroundColor = Color.Black;
                        break;
                }

                //update the UI
                Device.BeginInvokeOnMainThread(() =>
                {
                    textLabel.Text = textValue;
                    textLabel.TextColor = textColor;
                    titleLabel.TextColor = textColor;
                    frame.BackgroundColor = backgroundColor;
                    textLabel.FontAttributes = FontAttributes.Bold;
                    textLabel.IsVisible = true;

                    //hide the spinner
                    spinner.IsRunning = false;
                    spinner.IsVisible = false;
                });
            });

            return frame;
        }

        private Frame GetTextFrame(string labelText, string textValue = null)
        {
            var backgroundColor = MainColor;
            var textColor = Color.Black;
            var frame = new Frame()
            {
                BackgroundColor = backgroundColor,
                CornerRadius = 20,
                WidthRequest = 150
            };

            var label = new Label()
            {
                BackgroundColor = Color.Transparent,
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = textColor,
                Text = labelText,
                ClassId = "TitleLabel"
            };

            var valueLabel = new Label()
            {
                HorizontalTextAlignment = TextAlignment.End,
                Text = textValue,
                TextColor = textColor,
                ClassId = "TextLabel"
            };

            var loadingIndicator = new ActivityIndicator()
            {
                IsVisible = false,
                IsRunning = false,
                HeightRequest = 20,
                WidthRequest = 20,
                ClassId = "Spinner",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            var frameStackLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal
            };

            //add the two labels to the stack layout
            frameStackLayout.Children.Add(label);
            frameStackLayout.Children.Add(loadingIndicator);
            frameStackLayout.Children.Add(valueLabel);

            frame.Content = frameStackLayout;

            return frame;
        }
    }
}