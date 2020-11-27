using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FrogApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigableContentPage : ContentPage
    {
        public NavigableContentPage()

        {
            InitializeComponent();
        }
    }
}