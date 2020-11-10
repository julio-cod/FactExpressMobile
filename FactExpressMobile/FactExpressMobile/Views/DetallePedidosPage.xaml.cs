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
        public DetallePedidosPage()
        {
            InitializeComponent();
        }

        public DetallePedidosPage(int codPedido)
        {
            InitializeComponent();
            BindingContext = new DetallePedidoViewModel(codPedido);

            lblCodPedido.Text = "Cod-Pedido: " + codPedido.ToString();

        }

        private void CVListaDetallePedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}