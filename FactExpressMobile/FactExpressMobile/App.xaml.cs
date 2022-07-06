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
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU3MzE4QDMxMzgyZTMzMmUzMGx3RlhRRXZKNW15cE9SOXNIVlpyRk0rOEIvai9HZTBOZVdJdWhuc1NHU289");

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
