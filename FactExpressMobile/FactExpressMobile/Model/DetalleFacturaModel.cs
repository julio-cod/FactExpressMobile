using System;
using System.Collections.Generic;
using System.Text;

namespace FactExpressMobile.Model
{
    public class DetalleFacturaModel
    {
        //public int Codigo { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
    }
}
