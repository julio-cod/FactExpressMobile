using FactExpressMobile.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FactExpressMobile.ViewModel
{
    public class MenuOpcionesViewModel
    {
        public ObservableCollection<OpcionesModel> Opciones { get; set; }

        public MenuOpcionesViewModel()
        {
            Opciones = new ObservableCollection<OpcionesModel>
            {

                new OpcionesModel
            {
                IdOpcion=1,
                Imagen = "imgPedidosAsignados.jpg",
                nombreOpcion = "Pedido Asignados"

            },
            new OpcionesModel
            {
                IdOpcion=2,
                Imagen = "imgPedidosEntregados.jpg",
                nombreOpcion = "Pedidos Entregados"

            },
            new OpcionesModel
            {
                IdOpcion=3,
                Imagen = "imgCrearPedidos.jpg",
                nombreOpcion = "Crear Pedidos"

            },

            new OpcionesModel
            {
                IdOpcion=4,
                Imagen = "imgGestionarPedidos.jpg",
                nombreOpcion = "Gestionar Pedidos"

            }

             };

        }


    }
}
