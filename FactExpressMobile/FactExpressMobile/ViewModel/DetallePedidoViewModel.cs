using FactExpressMobile.Clases;
using FactExpressMobile.Model;
using Newtonsoft.Json;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
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


     /*   public DetallePedidoViewModel()
        {
            



        }*/

        public DetallePedidoViewModel(int codPedido)
        {
            ListaDetallePedidos(codPedido);

            

        }

        public DetallePedidoViewModel()
        {
            



        }

        public async void ListaDetallePedidos(int codPedido)
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

        public List<DetalleFacturaModel> listaDetallePedidoModel = new List<DetalleFacturaModel>();

        public async void ImprimirFactura(int codPedido, string nombreCliente, string pTipoPago, decimal total)
        {
            var uri = new Uri("http://FactExpressAPI.somee.com/api/DetallePedido?codPedido=");

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri + codPedido.ToString());

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var gets = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(content);

                listaDetallePedidoModel = new List<DetalleFacturaModel>(gets);
                CrearyAbrirPDF(codPedido, nombreCliente, pTipoPago, total);
            }
            else
            {
                Debug.WriteLine("un error ha ocurrido mientras cargaba la data");
                listaDetallePedidoModel = null;
            }

        }

        public async void CrearyAbrirPDF(int codPedido, string nombreCliente, string pTipoPago, decimal total)
        {
            //Creates a new PDF document
            PdfDocument document = new PdfDocument();

            //Adds page settings
            //pagina horizontal
            //document.PageSettings.Orientation = PdfPageOrientation.Landscape;

            //pagina vertical
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 50;
            //Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            //bounds
            RectangleF bounds = new RectangleF(176, 0, 390, 130); 

            //Presentar titulo
            PdfFont subHeadingFontTitulo = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            //Creates a text element to add the invoice number
            PdfTextElement elementTitulo = new PdfTextElement("FACTURA", subHeadingFontTitulo);

            //Draws the heading on the page
            PdfLayoutResult resultTitulo = elementTitulo.Draw(page, new PointF(10, bounds.Top));

            PdfPen linePenTitulo = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPointTitul = new PointF(0, resultTitulo.Bounds.Bottom + 3);
            PointF endPointTitul = new PointF(graphics.ClientSize.Width, resultTitulo.Bounds.Bottom + 3);
            graphics.DrawLine(linePenTitulo, startPointTitul, endPointTitul);
            //fin titulo
           
            //-----------
            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            bounds = new RectangleF(0, bounds.Bottom - 80, graphics.ClientSize.Width, 30);
            //Draws a rectangle to place the heading in that region.
            graphics.DrawRectangle(solidBrush, bounds);
            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            //Creates a text element to add the invoice number
            PdfTextElement element = new PdfTextElement("CODIGO-PEDIDO: " + codPedido.ToString(), subHeadingFont);
            element.Brush = PdfBrushes.White;

            //Draws the heading on the page
            PdfLayoutResult result = element.Draw(page, new PointF(10, bounds.Top +8)); // +8
            string currentDate = "FECHA " + DateTime.Now.ToString("MM/dd/yyyy");
            //Measures the width of the text to place it in the correct location
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y);
            //Draws the date by using DrawString method
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, textPosition);
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);
            //Creates text elements to add the address and draw it to the page.
            string tipoPago = "TIPO DE PAGO: " + pTipoPago;
            element = new PdfTextElement("CLIENTE: " + nombreCliente + "                                 "+ tipoPago, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            
            PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPoint = new PointF(0, result.Bounds.Bottom + 3);  // +3
            PointF endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);

            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();

            pdfGrid.DataSource = listaDetallePedidoModel;

            PdfGridCellStyle cellStyle = new PdfGridCellStyle();

            pdfGrid.Columns[0].Width = 60;
            pdfGrid.Columns[1].Width = 230;
            pdfGrid.Columns[2].Width = 60;
            pdfGrid.Columns[3].Width = 70;
            pdfGrid.Columns[4].Width = 70;

            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = pdfGrid.Headers[0];
            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

            int posicionTotal = 70;
            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 1)
                {
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                }

                else
                {
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
                }

                

            }
            posicionTotal = posicionTotal + (15 * pdfGrid.Rows.Count);

            //Applies the header style
            header.ApplyStyle(headerStyle);
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;


            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = pdfGrid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

            //Total
            PdfTextElement elementTotal = new PdfTextElement("TOTAL: " + total.ToString(), timesRoman);
            elementTotal.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = elementTotal.Draw(page, new PointF(10, result.Bounds.Bottom + posicionTotal));

            //Save the document to the stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Close the document
            document.Close(true);

            //Save the stream as a file in the device and invoke it for viewing
            await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("FactExpressMobileFactura.pdf", "application/pdf", stream);
        }



    }
}
