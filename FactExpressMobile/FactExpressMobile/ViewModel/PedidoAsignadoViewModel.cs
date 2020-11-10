using FactExpressMobile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FactExpressMobile.ViewModel
{
    public class PedidoAsignadoViewModel : INotifyPropertyChanged
    {
        private List<PedidoAsignadoModel> _GetsList { get; set; }
        public List<PedidoAsignadoModel> GetsList
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

        public ICommand BuscarPedidosAsignadosCmd { get; set; }

        public PedidoAsignadoViewModel()
        {
            ListaPedidosAsignados();

            BuscarPedidosAsignadosCmd = new Command(() =>
            {
                


            });

            


        }




        private async void ListaPedidosAsignados()
        {
            int codUsuario = MainPage.codUsuario;

            var uri = new Uri("http://FactExpressAPI.somee.com/api/Pedido?codUsuario=");

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri + codUsuario.ToString());

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var gets = JsonConvert.DeserializeObject<List<PedidoAsignadoModel>>(content);

                GetsList = new List<PedidoAsignadoModel>(gets);

            }
            else
            {
                Debug.WriteLine("un error ha ocurrido mientras cargaba la data");
            }


        }


    }
}
