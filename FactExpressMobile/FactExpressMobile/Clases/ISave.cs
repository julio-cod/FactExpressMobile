using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FactExpressMobile.Clases
{
    public interface ISave
    {
        //Metodo para guardar documento como un archivo y ver el documento guardado
        Task SaveAndView(string filename, string contentType, MemoryStream stream);
    }
}
