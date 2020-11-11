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
    public partial class DetallePedidosPage : ContentPage
    {
        String tipoPago = "";
        int _codProducto;
        public DetallePedidosPage()
        {
            InitializeComponent();
        }

        public DetallePedidosPage(int codPedido, decimal total, string nombreCliente)
        {
            InitializeComponent();
            _codProducto = codPedido;
            BindingContext = new DetallePedidoViewModel(codPedido);

            lblCodPedido.Text = "COD-PEDIDO: " + codPedido.ToString();
            lblCliente.Text = "CLIENTE: " + nombreCliente;
            lblTotal.Text = "TOTAL: " + total.ToString();
            LlenarPickerTipoPago();

        }

        public void LlenarPickerTipoPago()
        {
            pkTipoPago.Items.Add("EFECTIVO");
            pkTipoPago.Items.Add("CREDITO");
        }

        private async void CVListaDetallePedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection[0] is DetallePedidoModel detallePedidoModel)
            {
                var Resp = await DisplayAlert("Eliminar Producto", "Desea eliminar este producto de la lista?", "Si", "No");

                if (Resp == true)
                {
                    //await Navigation.PushAsync(new DetallePedidosPage(detallePedidoModel.Codigo));
                    BindingContext = new DetallePedidoViewModel(_codProducto);
                }
                else
                {
                    BindingContext = new DetallePedidoViewModel(_codProducto);
                }

                

            }
            
        }

        private void pkTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoPago = pkTipoPago.Items[pkTipoPago.SelectedIndex];

        }

        private async void btnFacturar_Clicked(object sender, EventArgs e)
        {
            if (tipoPago != "")
            {
                var Resp = await DisplayAlert("Facturacion", "Finalizar la entrega del Pedido?", "Si", "No");

                if (Resp == true)
                {
                    lblLista.TextColor = Color.Green;
                    lblLista.Text = "Pedido Entregado";
                    btnFacturar.IsEnabled = false;
                    pkTipoPago.IsVisible = false;
                    lblTipoPago.Text = "PAGO: " + tipoPago;
                    BindingContext = new DetallePedidoViewModel(0);
                    

                }
                else
                {
                    BindingContext = new DetallePedidoViewModel(_codProducto);
                }
            }
            else
            {
                await DisplayAlert("Atencion", "Seleccione el Tipo de Pago", "OK");
            }
        }
    }
}