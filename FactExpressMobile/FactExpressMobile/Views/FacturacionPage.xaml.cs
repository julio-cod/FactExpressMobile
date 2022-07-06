using FactExpressMobile.Clases;
using FactExpressMobile.Model;
using FactExpressMobile.ViewModel;
using Newtonsoft.Json;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FactExpressMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacturacionPage : ContentPage
    {
        FacturarPedidoViewModel FactClass = new FacturarPedidoViewModel();

        DetallePedidoViewModel DetalleClass = new DetallePedidoViewModel();


        public static int pCodPedido = 0;
        public static decimal pTotalDescuentos = 0;
        public static decimal pTotal = 0;
        public static decimal pTotalGanancia = 0;
        public static int pCodigoCliente = 0;
        public static String pNombreCliente = "";
        public static String pTipoPago = "";

        decimal subTotal = 0;

        string descripcion = "";

        public FacturacionPage()
        {
            InitializeComponent();
            btnImprimir.IsVisible = false;
        }

        public FacturacionPage(int codPedido,int codigoCliente, string nombreCliente, decimal totalDescuentos, decimal total, decimal totalGanancia)
        {
            InitializeComponent();
            btnImprimir.IsVisible = false;
            pCodPedido = codPedido;
            pTotalDescuentos = totalDescuentos;
            pTotal = total;
            pTotalGanancia = totalGanancia;
            pCodigoCliente = codigoCliente;
            pNombreCliente = nombreCliente;
            BindingContext = new DetallePedidoViewModel(codPedido);
            lblCodPedido.Text = "COD-PEDIDO: " + codPedido.ToString();
            lblCliente.Text = "CLIENTE: " + nombreCliente;
            lblTotal.Text = "TOTAL: " + total.ToString();
            LlenarPickerTipoPago();

            //---prueba

            
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
                    subTotal = (detallePedidoModel.Cantidad * detallePedidoModel.Precio);
                    pTotal = pTotal - subTotal;
                    lblTotal.Text = "TOTAL: " + pTotal.ToString();
                    
                    FactClass.EliminarDetallePedido(detallePedidoModel.Codigo);
                    
                    BindingContext = new DetallePedidoViewModel(pCodPedido);

                    //Nuevamente  BindingContext para re-actualizar la solicutud a la clase
                    BindingContext = new DetallePedidoViewModel(pCodPedido);

                }
                else
                {
                    BindingContext = new DetallePedidoViewModel(pCodPedido);
                }



            }
        }

        private void pkTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            pTipoPago = pkTipoPago.Items[pkTipoPago.SelectedIndex];
        }

        private async void btnFacturar_Clicked(object sender, EventArgs e)
        {
            if (pTipoPago != "")
            {
                var Resp = await DisplayAlert("Facturacion", "Finalizar la entrega del Pedido?", "Si", "No");

                if (Resp == true)
                {
                    lblLista.TextColor = Xamarin.Forms.Color.Green;
                    btnImprimir.IsVisible = true;
                    lblLista.Text = "Pedido Entregado";
                    btnFacturar.IsEnabled = false;
                    pkTipoPago.IsVisible = false;
                    lblTipoPago.Text = "PAGO: " + pTipoPago;
                    //FactClass.RegistrarVentaPedido( pCodigoCliente, pNombreCliente,pCodPedido, pTotalDescuentos, pTotal, pTotalGanancia, pTipoPago, MainPage.codUsuario, MainPage.nombreUsuario);
                    CVListaDetallePedidos.IsVisible = false;


                }
                else
                {
                    
                }
            }
            else
            {
                await DisplayAlert("Atencion", "Seleccione el Tipo de Pago", "OK");
            }
        }

        private void btnImprimir_Clicked(object sender, EventArgs e)
        {

            DetalleClass.ImprimirFactura(pCodPedido, pNombreCliente, pTipoPago, pTotal);
           
        }

        

        

       


    }
}