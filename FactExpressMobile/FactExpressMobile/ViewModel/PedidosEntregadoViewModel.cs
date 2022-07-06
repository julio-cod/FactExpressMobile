using FactExpressMobile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

namespace FactExpressMobile.ViewModel
{
    public class PedidosEntregadoViewModel : INotifyPropertyChanged
    {
        private List<VentaModel> _GetsList { get; set; }
        public List<VentaModel> GetsList
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



        public PedidosEntregadoViewModel()
        {
            ListaPedidosEntregados();






        }

        public PedidosEntregadoViewModel(string tipoPago)
        {
            ListaPedidosEntregadosPorTipoPago(tipoPago);



        }

        public PedidosEntregadoViewModel(int codUsuario, string nombreCliente)
        {
            ListaPedidosEntregadosPorCliente(codUsuario, nombreCliente);



        }

        public PedidosEntregadoViewModel(int codUsuario, string tipoPago, string nombreCliente)
        {
            ListaPedidosEntregadosPorClienteTipoPago(codUsuario, tipoPago, nombreCliente);



        }

        private async void ListaPedidosEntregados()
        {
            int codUsuario = MainPage.codUsuario;

            var uri = new Uri("http://FactExpressAPI.somee.com/api/Venta?codUsuario=");

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri + codUsuario.ToString());

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var gets = JsonConvert.DeserializeObject<List<VentaModel>>(content);

                GetsList = new List<VentaModel>(gets);

            }
            else
            {
                Debug.WriteLine("un error ha ocurrido mientras cargaba la data");
            }


        }

        private async void ListaPedidosEntregadosPorTipoPago(string tipoPago)
        {
            int codUsuario = MainPage.codUsuario;

            var uri = new Uri("http://FactExpressAPI.somee.com/api/Venta?codUsuario=" + codUsuario.ToString() + "&tipoPago=");

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri + tipoPago);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var gets = JsonConvert.DeserializeObject<List<VentaModel>>(content);

                GetsList = new List<VentaModel>(gets);

            }
            else
            {
                Debug.WriteLine("un error ha ocurrido mientras cargaba la data");
            }


        }

        private async void ListaPedidosEntregadosPorCliente(int codUsuario, string nombreCliente)
        {
            
            var uri = new Uri("http://FactExpressAPI.somee.com/api/Venta?codUsuario="+ codUsuario.ToString() + "&nombreCliente=");

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri + nombreCliente);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var gets = JsonConvert.DeserializeObject<List<VentaModel>>(content);

                GetsList = new List<VentaModel>(gets);

            }
            else
            {
                Debug.WriteLine("un error ha ocurrido mientras cargaba la data");
            }


        }

        private async void ListaPedidosEntregadosPorClienteTipoPago(int codUsuario, string tipoPago, string nombreCliente)
        {

            var uri = new Uri("http://FactExpressAPI.somee.com/api/Venta?codUsuario="+ codUsuario.ToString() + "&tipoPago="+ tipoPago + "&nombreCliente=");

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri + nombreCliente);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var gets = JsonConvert.DeserializeObject<List<VentaModel>>(content);

                GetsList = new List<VentaModel>(gets);

            }
            else
            {
                Debug.WriteLine("un error ha ocurrido mientras cargaba la data");
            }


        }


    }
}
