using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FactExpressMobile
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterPageApp { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
