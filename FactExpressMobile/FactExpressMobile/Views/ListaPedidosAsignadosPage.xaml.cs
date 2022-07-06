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
    public partial class ListaPedidosAsignadosPage : ContentPage
    {
        public ListaPedidosAsignadosPage()
        {
            InitializeComponent();
            BindingContext = new PedidoViewModel();
        }

        private async void CVListaPedidosAsignados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection[0] is PedidoModel pedidoAsignadoModel)
            {

                await Navigation.PushAsync(new DetallePedidosPage(pedidoAsignadoModel.CodigoPedido, pedidoAsignadoModel.Total, pedidoAsignadoModel.NombreCliente));
                BindingContext = new PedidoViewModel();

            }
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            BindingContext = new PedidoViewModel();
        }
    }
}