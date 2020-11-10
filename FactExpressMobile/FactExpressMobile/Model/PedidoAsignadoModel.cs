using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FactExpressMobile.Model
{
    public class PedidoAsignadoModel
    {
        public int Codigo { get; set; }
        public int CodPedido { get; set; }
        public int CodUsuarioEnttrega { get; set; }
        public string NombreUsuario { get; set; }
        public int CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string LugarEntrega { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal TotalDescuentos { get; set; }
        public decimal Total { get; set; }
        public decimal TotalGanancia { get; set; }
        public string Estado { get; set; }
        public string Comentario { get; set; }
    }
}
