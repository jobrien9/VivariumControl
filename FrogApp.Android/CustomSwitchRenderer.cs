using Android.Content;
using Android.Graphics;
using Android.Widget;
using FrogApp;
using FrogApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//todo: do this for ios, too
//https://stackoverflow.com/questions/48515171/is-there-a-way-i-can-customize-a-switch-colors
//https://stackoverflow.com/questions/48564792/switchrender-switchrender-is-obsolete-with-xamarin-forms-renderer
[assembly: ExportRenderer(typeof(SwitchFix), typeof(CustomSwitchRenderer))]

namespace FrogApp.Droid
{
    public class CustomSwitchRenderer : Xamarin.Forms.Platform.Android.SwitchRenderer
    {
        private SwitchFix view;

        public CustomSwitchRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
            {
                return;
            }

            view = (SwitchFix)Element;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
            {
                if (this.Control != null)
                {
                    if (!this.Control.Checked)
                    {
                        this.Control.TrackDrawable.SetColorFilter(view.SwitchOffColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                    }

                    this.Control.CheckedChange += this.OnCheckedChange;
                }
            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            //https://stackoverflow.com/questions/49448206/xamarin-forms-switchs-toggled-event-not-being-triggered-when-using-custom-rende
            Element.IsToggled = Control.Checked;

            if (!this.Control.Checked)
            {
                this.Control.TrackDrawable.SetColorFilter(view.SwitchOffColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }
    }
}