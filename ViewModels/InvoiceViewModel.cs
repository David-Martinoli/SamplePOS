using SamplePOS_ServerSide.Models;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using PdfSharp.Drawing.Layout;
using MigraDoc.DocumentObjectModel;
using System.Linq.Expressions;
using MigraDoc.DocumentObjectModel.Tables;
using System.ComponentModel;
using SamplePOS_ServerSide.Core;
using System.Diagnostics;

namespace SamplePOS_ServerSide.ViewModels
{
    public class InvoiceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private Invoice _invoice;

        public Invoice Invoice
        {
            get => _invoice; 
            private set
            {
                _invoice = value;
                OnPropertyChanged(nameof(Invoice));
            }
        }

        public InvoiceViewModel()
        {
            MessageBus.InvoiceGenerated += OnInvoiceGenerated;
        }

        private void OnInvoiceGenerated(InvoiceGeneratedMessage message)
        {
            Invoice = message.Invoice;
            GeneratePDF();
        }

        private void GeneratePDF()
        {
            MigraDoc.DocumentObjectModel.Document document = new();
            Section section = document.AddSection();

            document.AddSection();

            // Título
            Paragraph titulo = section.AddParagraph("Factura");
            titulo.Format.Font.Size = 18;
            titulo.Format.Alignment = ParagraphAlignment.Center;

            // Información del Cliente
            section.AddParagraph($"Cliente: {Invoice.CustomerName}");
            section.AddParagraph($"Fecha: {Invoice.Date}");

            // Tabla de Detalles
            Table table = section.AddTable();
            table.Borders.Width = 0.75;
            Column column1 = table.AddColumn("5cm");
            Column column2 = table.AddColumn("2cm");
            Column column3 = table.AddColumn("3cm");
            Column column4 = table.AddColumn("3cm");

            Row row = table.AddRow();
            row.Cells[0].AddParagraph("Descripción");
            row.Cells[1].AddParagraph("Cantidad");
            row.Cells[2].AddParagraph("Precio");
            row.Cells[3].AddParagraph("Subtotal");

            foreach (var detail in Invoice.Details)
            {
                Row detailRow = table.AddRow();
                detailRow.Cells[0].AddParagraph(detail.Description);
                //detailRow.Cells[1].AddParagraph(detalle.Quantity.ToString());
                detailRow.Cells[2].AddParagraph(detail.Price.ToString("C"));
                //detailRow.Cells[3].AddParagraph(detalle.Subtotal.ToString("C"));
            }

            section.AddParagraph($"Total: {Invoice.Total.ToString("C")}");

            PdfDocumentRenderer renderer = new()
            {
                Document = document
            };
            renderer.RenderDocument();

            string filename = "factura.pdf";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            renderer.PdfDocument.Save(filePath);

            if (File.Exists(filePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
                Console.WriteLine("PDF generado exitosamente.");
                Console.WriteLine($"Ruta del PDF: {filePath}");
            }else
            {
                Console.WriteLine("No se pudo generar el PDF.");
            }

            // Notificar al usuario que el PDF se ha generado.

        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
