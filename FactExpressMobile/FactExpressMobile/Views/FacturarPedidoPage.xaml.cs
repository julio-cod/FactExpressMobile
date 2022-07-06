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
    public partial class FacturarPedidoPage : ContentPage
    {
        public FacturarPedidoPage()
        {
            InitializeComponent();
            BindingContext = new FacturarPedidoViewModel();


        }

        private async void CVListaPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection[0] is PedidoModel pedidoAsignadoModel)
            {

                await Navigation.PushAsync(new FacturacionPage(pedidoAsignadoModel.CodigoPedido, pedidoAsignadoModel.CodigoCliente, pedidoAsignadoModel.NombreCliente, pedidoAsignadoModel.TotalDescuentos, pedidoAsignadoModel.Total, pedidoAsignadoModel.TotalGanancia));
                BindingContext = new PedidoViewModel();

            }

        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            
            if (txtBuscarPorCliente.Text != "" || txtBuscarPorCliente.Text != null)
            {
                BindingContext = new FacturarPedidoViewModel();
                

            }
            else
            {
                BindingContext = new FacturarPedidoViewModel(txtBuscarPorCliente.Text);
            }
        }

        private void txtBuscarPorCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingContext = new FacturarPedidoViewModel(txtBuscarPorCliente.Text);

            if (txtBuscarPorCliente.Text == "")
            {
                BindingContext = new FacturarPedidoViewModel();

            }

        }
    }
}