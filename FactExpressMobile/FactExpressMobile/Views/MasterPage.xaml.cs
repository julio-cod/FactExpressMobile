using FactExpressMobile.Model;
using FactExpressMobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FactExpressMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();
            BindingContext = new MenuOpcionesViewModel();
        }

        private async void CVListaMenuOpciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.MasterPageApp.IsPresented = false;
            if (e.CurrentSelection[0] is OpcionesModel opcionesModel)
            {
                if (opcionesModel.IdOpcion == 1)
                {
                    await App.MasterPageApp.Detail.Navigation.PushAsync(new ListaPedidosAsignadosPage());
                    BindingContext = new MenuOpcionesViewModel();

                }


                BindingContext = new MenuOpcionesViewModel();

            }

        }
    }
}