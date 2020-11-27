using Xamarin.Forms;

namespace FrogApp
{
    //this is intended to fix the invisible track when the black background is toggled
    //based on https://stackoverflow.com/questions/48515171/is-there-a-way-i-can-customize-a-switch-colors
    public class SwitchFix : Switch
    {
        public static readonly BindableProperty SwitchOffColorProperty =
          BindableProperty.Create(nameof(SwitchOffColor),
              typeof(Color), typeof(SwitchFix),
              Color.Default);

        public Color SwitchOffColor
        {
            get { return (Color)GetValue(SwitchOffColorProperty); }
            set { SetValue(SwitchOffColorProperty, value); }
        }
    }
}