using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratoriya
{
    public partial class FormServices : Form
    {
        public FormServices()
        {
            InitializeComponent();
            ShowServices();
        }
        public void ShowServices()
        {
            listViewServices.Items.Clear();
            foreach (service service in Program.labDB.service)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    service.Code.ToString(), service.Service1,
                    service.Price.ToString(),service.Deadline.ToString()
                });
                item.Tag = service;
                listViewServices.Items.Add(item);
            }
        }

        private void listViewServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewServices.SelectedItems.Count==1)
            {
                service _service = listViewServices.SelectedItems[0].Tag as service;
                textBoxCode.Text = _service.Code.ToString();
                textBoxName.Text = _service.Service1;
                textBoxPrice.Text = _service.Price.ToString();
                textBoxDeeadline.Text = _service.Deadline.ToString();
            }
            else
            {
                textBoxCode.Text = "";
                textBoxName.Text = "";
                textBoxPrice.Text = "";
                textBoxDeeadline.Text = "";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                service _service = new service();
                if (textBoxCode.Text != "" && textBoxName.Text != "" && textBoxPrice.Text != "")
                {
                    _service.Code = Convert.ToInt32(textBoxCode.Text);
                    _service.Service1 = textBoxName.Text;
                    _service.Price = Convert.ToDouble(textBoxPrice.Text);
                    if (textBoxDeeadline.Text!="") { _service.Deadline = Convert.ToInt32(textBoxDeeadline.Text); }
                    Program.labDB.service.Add(_service);
                    Program.labDB.SaveChanges();
                    ShowServices();
                }
            }
            catch
            {
                MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                service _service = listViewServices.SelectedItems[0].Tag as service;
                if (textBoxCode.Text != "" && textBoxName.Text != "" && textBoxPrice.Text != "")
                {
                    _service.Code = Convert.ToInt32(textBoxCode.Text);
                    _service.Service1 = textBoxName.Text;
                    _service.Price = Convert.ToDouble(textBoxPrice.Text);
                    if (textBoxDeeadline.Text != "") { _service.Deadline = Convert.ToInt32(textBoxDeeadline.Text); }
                    Program.labDB.SaveChanges();
                    ShowServices();
                }
            }
            catch
            {
                MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewServices.SelectedItems.Count == 1)
                {
                    service _service = listViewServices.SelectedItems[0].Tag as service;
                    Program.labDB.service.Remove(_service);
                    Program.labDB.SaveChanges();
                    ShowServices();
                }
                textBoxCode.Text = "";
                textBoxName.Text = "";
                textBoxPrice.Text = "";
                textBoxDeeadline.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
