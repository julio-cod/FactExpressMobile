using FactExpressMobile.Model;
using FactExpressMobile.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace FactExpressMobile.ViewModel
{
    public class FacturarPedidoViewModel : INotifyPropertyChanged
    {

        private List<PedidoModel> _GetsList { get; set; }
        public List<PedidoModel> GetsList
        {
            get
            {
                return _GetsList;
            }
            set
            {
                if (value != _GetsList)
                {
                    _GetsList = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        public FacturarPedidoViewModel()
        {
            ListaPedidosAsignados();



            
        }

        public FacturarPedidoViewModel(string nombreCliente)
        {
            ListaPedidosAsignadosPorCliente(nombreCliente);




        }

        private async void ListaPedidosAsignados()
        {
            int codUsuario = MainPage.codUsuario;

            var uri = new Uri("http://FactExpressAPI.somee.com/api/PedidosAsignados?codUsuario=");

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri + codUsuario.ToString());

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var gets = JsonConvert.DeserializeObject<List<PedidoModel>>(content);

                GetsList = new List<PedidoModel>(gets);

            }
            else
            {
                Debug.WriteLine("un error ha ocurrido mientras cargaba la data");
            }


        }

        private async void ListaPedidosAsignadosPorCliente(string nombreCliente)
        {
            int codUsuario = MainPage.codUsuario;

            var uri = new Uri("http://FactExpressAPI.somee.com/api/PedidosAsignados?codUsuario="+ codUsuario + "&nombreCliente=");

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri + nombreCliente);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var gets = JsonConvert.DeserializeObject<List<PedidoModel>>(content);

                GetsList = new List<PedidoModel>(gets);

            }
            else
            {
                Debug.WriteLine("un error ha ocurrido mientras cargaba la data");
            }


        }



        public async void EjecutarFacturaPedido(int codigoPedido)
        {
            var uri = new Uri("http://FactExpressAPI.somee.com/api/Factura");

            var httpClient = new HttpClient();
            //La api solo se actualizara el estado como Entregado
            var pedidoAsignadoModel = new PedidoModel()
            {
                CodigoPedido = codigoPedido,
                Estado = "Entregado"

            };
            var jsonObject = JsonConvert.SerializeObject(pedidoAsignadoModel);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

                Debug.WriteLine("Datos Actualizados");


            }
            else
            {
                Debug.WriteLine("Error ha ocurrido mientras se Actualizaba la data");
            }
            

        }

        public async void RegistrarVentaPedido(int codigoCliente, string nombreCliente,int codigoPedido, decimal totalDescuentos, decimal total, decimal totalGanancia, string tipoPago, int codUsuarioEntrega, string nombreUsuarioEntrega)
        {

            var uri = new Uri("http://FactExpressAPI.somee.com/api/Venta");

            var httpClient = new HttpClient();

            var ventaModel = new VentaModel()
            {

                
                CodigoCliente = codigoCliente,
                NombreCliente = nombreCliente,
                CodigoPedido = codigoPedido,
                TotalDescuentos = totalDescuentos,
                Total = total,
                TotalGanancia = totalGanancia,
                TipoPago = tipoPago,
                Fecha = DateTime.Today,
                CodUsuarioEntrega = codUsuarioEntrega,
                NombreUsuarioEntrega = nombreUsuarioEntrega

            };
            var jsonObject = JsonConvert.SerializeObject(ventaModel);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                EjecutarFacturaPedido(codigoPedido);
                Debug.WriteLine("Datos Guardados");


            }
            else
            {
                Debug.WriteLine("Error ha ocurrido mientras se Guardaba la data");
            }


        }

        public async void EditarPedidoCambioDetalle()
        {
            var uri = new Uri("http://FactExpressAPI.somee.com/api/PedidosAsignados");

            var httpClient = new HttpClient();

            var pedidoAsignadoModel = new PedidoModel()
            {
                CodigoPedido = FacturacionPage.pCodPedido,
                TotalDescuentos = FacturacionPage.pTotalDescuentos,
                Total = FacturacionPage.pTotal,
                TotalGanancia = FacturacionPage.pTotalGanancia,
                Estado = "Asignado"

            };
            var jsonObject = JsonConvert.SerializeObject(pedidoAsignadoModel);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

                Debug.WriteLine("Datos Actualizados");


            }
            else
            {
                Debug.WriteLine("Error ha ocurrido mientras se Actualizaba la data");
            }


        }

        public async void EliminarDetallePedido(int codigo)
        {

            var uri = "http://FactExpressAPI.somee.com/api/DetallePedido?codigo=";

            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(uri + codigo);

            if (response.IsSuccessStatusCode)
            {
                EditarPedidoCambioDetalle();
                Debug.WriteLine("Elemento eliminado Correctamente");

            }
            else
            {
                Debug.WriteLine("Error al eliminar");
            }


        }




    }
}
