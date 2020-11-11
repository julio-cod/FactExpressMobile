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
    public class DetallePedidoViewModel : INotifyPropertyChanged
    {

        private List<DetallePedidoModel> _GetsList { get; set; }
        public List<DetallePedidoModel> GetsList
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

        public DetallePedidoViewModel()
        {
            



        }

        public DetallePedidoViewModel(int codPedido)
        {
            ListaDetallePedidos(codPedido);

            

        }

        private async void ListaDetallePedidos(int codPedido)
        {

            var uri = new Uri("http://FactExpressAPI.somee.com/api/DetallePedido?codPedido=");

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri + codPedido.ToString());

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var gets = JsonConvert.DeserializeObject<List<DetallePedidoModel>>(content);

                GetsList = new List<DetallePedidoModel>(gets);

            }
            else
            {
                Debug.WriteLine("un error ha ocurrido mientras cargaba la data");
            }


        }


    }
}
