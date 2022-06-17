using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;

namespace Laboratoriya
{
    public partial class FormListOrder : Form
    {
        public FormListOrder()
        {
            InitializeComponent();
            ShowOrders();
        }
        public void ShowOrders()
        {
            listViewOrders.Items.Clear();
            foreach (order _order in Program.labDB.order)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    _order.date_start.ToString("ddMMyyyy"),
                    _order.id.ToString(),
                    _order.code.ToString(),
                    _order.patient.name + " " + _order.patient.surname,
                    _order.patient.date.ToString("ddMMyyyy"),
                    _order.service.Price.ToString()
                });
                item.Tag = _order;
                listViewOrders.Items.Add(item);
            }
        }

        private async void buttonPDF_Click(object sender, EventArgs e)
        {
            PdfPTable pdfTable = new PdfPTable(listViewOrders.Columns.Count);
            string FONT_LOCATION = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font text = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.NORMAL);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 80;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            foreach (ColumnHeader column in listViewOrders.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.Text, text));
                pdfTable.AddCell(cell);
            }
            foreach (ListViewItem item in listViewOrders.Items)
            {
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    pdfTable.AddCell(item.SubItems[i].Text);
                }
            }
            string path = Environment.CurrentDirectory;
            using (FileStream stream = new FileStream(path + "/Отчет по услугам.pdf", FileMode.Create))
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();
                document.Add(pdfTable);
                document.Close();
                stream.Close();
                FormCodeOrder order = (FormCodeOrder)Application.OpenForms["FormCodeOrder"];
            }
        }
    }
}
