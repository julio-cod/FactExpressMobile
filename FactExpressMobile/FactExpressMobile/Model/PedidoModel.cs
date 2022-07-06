using System;
using System.Collections.Generic;
using System.Text;

namespace FactExpressMobile.Model
{
    public class PedidoModel
    {
        public int CodigoPedido { get; set; }
        public int CodUsuarioDelSistema { get; set; }
        public string NombreUsuarioDelSistema { get; set; }
        public int CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string LugarEntrega { get; set; }
        public DateTime FechaOrden { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal TotalDescuentos { get; set; }
        public decimal Total { get; set; }
        public decimal TotalGanancia { get; set; }
        public string Estado { get; set; }
        public string Comentario { get; set; }
        public int CodUsuarioEntrega { get; set; }
        public string NombreUsuarioEntrega { get; set; }
    }

}
