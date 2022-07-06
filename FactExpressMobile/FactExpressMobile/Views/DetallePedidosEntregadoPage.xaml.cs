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
    public partial class DetallePedidosEntregadoPage : ContentPage
    {
        public DetallePedidosEntregadoPage()
        {
            InitializeComponent();
        }

        public DetallePedidosEntregadoPage(int codPedido, string nombreCliente, decimal totalDescuentos, decimal total, DateTime fecha, string tipoPago)
        {
            InitializeComponent();
            BindingContext = new DetallePedidoViewModel(codPedido);

            Title = "COD-PEDIDO: " + codPedido.ToString();
            lblTipoPago.Text = "PAGO: " + tipoPago;
            lblCliente.Text = "CLIENTE: " + nombreCliente;
            lblTotal.Text = "TOTAL: " + total.ToString();
            lblDescuento.Text = "DESCUENTO: " + totalDescuentos.ToString();
            datePickerFecha.Date = fecha;

        }

    }
}