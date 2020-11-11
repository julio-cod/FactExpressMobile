using FactExpressMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FactExpressMobile
{
    public partial class MainPage : MasterDetailPage
    {
        public static int codUsuario = 1;
        public MainPage()
        {
            InitializeComponent();

            this.Master = new MasterPage();
            this.Detail = new NavigationPage(new DetailPage());

            App.MasterPageApp = this;
        }
    }
}
