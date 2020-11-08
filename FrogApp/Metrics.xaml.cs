using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrogApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Metrics : NavigableContentPage
    {
        public Metrics()
        {
            InitializeComponent();
            SetupMetrics();
        }

        private void SetupMetrics()
        {
            MetricsStack.Children.Add(GetFrame("Sunrise:", textValue: App.Sunrise.ToLocalTime().ToString()));
            MetricsStack.Children.Add(GetFrame("Sunset:", textValue: App.Sunset.ToLocalTime().ToString()));
            MetricsStack.Children.Add(GetFrame("Froglet Light:", ParticleCommunication.GetFrogletLightStatus()));
            MetricsStack.Children.Add(GetFrame("Vivarium Light:", ParticleCommunication.GetVivariumLightStatus()));
            MetricsStack.Children.Add(GetFrame("Temperature:", textValue: "---"));
        }

        private Frame GetFrame(string labelText, LightStatus? lightStatus = null, string textValue = null)
        {
            var mainColor = (Color)Application.Current.Resources["FrogAppSecondary"];
            var backgroundColor = mainColor;
            var textColor = Color.Black;
            var boldText = FontAttributes.None;
            if (lightStatus.HasValue)
            {
                boldText = FontAttributes.Bold;
                switch (lightStatus)
                {
                    case LightStatus.On:
                        textValue = "On";
                        backgroundColor = mainColor;
                        textColor = Color.AntiqueWhite;
                        break;

                    case LightStatus.Twilight:
                        textValue = "Twilight";
                        backgroundColor = Color.Blue;
                        textColor = Color.White;
                        break;

                    default:
                        textValue = "Off";
                        textColor = mainColor;
                        backgroundColor = Color.Black;
                        break;
                }
            }
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
            };

            var valueLabel = new Label()
            {
                HorizontalTextAlignment = TextAlignment.End,
                Text = textValue,
                TextColor = textColor,
                FontAttributes = boldText
            };

            var frameStackLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal
            };

            //add the two labels to the stack layout
            frameStackLayout.Children.Add(label);
            frameStackLayout.Children.Add(valueLabel);

            frame.Content = frameStackLayout;

            return frame;
        }
    }
}