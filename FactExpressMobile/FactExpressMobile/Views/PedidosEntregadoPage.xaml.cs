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
    public partial class PedidosEntregadoPage : ContentPage
    {
        String tipoPago = "";

        public PedidosEntregadoPage()
        {
            InitializeComponent();
            BindingContext = new PedidosEntregadoViewModel();
            LlenarPickerTipoPago();
            
        }

        private async void CVListaPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection[0] is VentaModel ventaModel)
            {

                await Navigation.PushAsync(new DetallePedidosEntregadoPage(ventaModel.CodigoPedido, ventaModel.NombreCliente, ventaModel.TotalDescuentos, ventaModel.Total, ventaModel.Fecha, ventaModel.TipoPago));
                BindingContext = new PedidosEntregadoViewModel();

            }
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (tipoPago == "EFECTIVO")
            {
                BindingContext = new PedidosEntregadoViewModel(tipoPago);
            }
            else if (tipoPago == "CREDITO")
            {
                BindingContext = new PedidosEntregadoViewModel(tipoPago);
            }
            else
            {
                BindingContext = new PedidosEntregadoViewModel();
            }
        }

        public void LlenarPickerTipoPago()
        {
            pkTipoPago.Items.Add("TODOS");
            pkTipoPago.Items.Add("EFECTIVO");
            pkTipoPago.Items.Add("CREDITO");

            pkTipoPago.SelectedItem = "TODOS";

            if (tipoPago == "EFECTIVO")
            {
                BindingContext = new PedidosEntregadoViewModel(tipoPago);
            }
            else if (tipoPago == "CREDITO")
            {
                BindingContext = new PedidosEntregadoViewModel(tipoPago);
            }
            else
            {
                BindingContext = new PedidosEntregadoViewModel();
            }

        }

        private void pkTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoPago = pkTipoPago.Items[pkTipoPago.SelectedIndex];

            if (txtBuscarPorCliente.Text == "")
            {
                if (tipoPago == "EFECTIVO")
                {
                    BindingContext = new PedidosEntregadoViewModel(tipoPago);
                }
                else if (tipoPago == "CREDITO")
                {
                    BindingContext = new PedidosEntregadoViewModel(tipoPago);
                }
                else
                {
                    BindingContext = new PedidosEntregadoViewModel();
                }

            }
            else
            {
                if (tipoPago == "EFECTIVO")
                {
                    BindingContext = new PedidosEntregadoViewModel(MainPage.codUsuario, tipoPago, txtBuscarPorCliente.Text);
                }
                else if (tipoPago == "CREDITO")
                {
                    BindingContext = new PedidosEntregadoViewModel(MainPage.codUsuario, tipoPago, txtBuscarPorCliente.Text);
                }
                else
                {
                    BindingContext = new PedidosEntregadoViewModel(tipoPago);
                }
            }

            

        }

        private void txtBuscarPorCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingContext = new PedidosEntregadoViewModel(MainPage.codUsuario, txtBuscarPorCliente.Text);

            if (txtBuscarPorCliente.Text == "")
            {
                BindingContext = new PedidosEntregadoViewModel();

            }

            if (tipoPago == "EFECTIVO")
            {
                BindingContext = new PedidosEntregadoViewModel(MainPage.codUsuario, tipoPago, txtBuscarPorCliente.Text);
            }
            else if (tipoPago == "CREDITO")
            {
                BindingContext = new PedidosEntregadoViewModel(MainPage.codUsuario, tipoPago, txtBuscarPorCliente.Text);
            }
            else
            {
                if (tipoPago == "TODOS")
                {
                    BindingContext = new PedidosEntregadoViewModel(MainPage.codUsuario, txtBuscarPorCliente.Text);
                }
                else
                {
                    BindingContext = new PedidosEntregadoViewModel();
                }
                
            }

        }
    }
}