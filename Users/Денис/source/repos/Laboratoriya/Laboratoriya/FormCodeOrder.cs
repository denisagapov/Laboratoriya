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
    public partial class FormCodeOrder : Form
    {
        public static int id = 0;
        public FormCodeOrder()
        {
            InitializeComponent();
            ShowCode();
        }

        public void ShowCode()
        {
            id = (from _order in Program.labDB.order select _order.id).ToList().Last() + 1;
            textBoxCode.Text = id.ToString();
            textBoxCode.ForeColor = Color.Gray;
        }

        private void textBoxCode_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxCode.Text = "";
            textBoxCode.ForeColor = Color.Black;
        }
        private void buttonVerifCode_Click(object sender, EventArgs e)
        {
            bool key = false;
            if (textBoxCode.Text != "")
            {
                try
                {
                    foreach (order _order in Program.labDB.order)
                    {
                        if (id != _order.id)
                        {
                            key = true;
                        }
                    }
                    if (key)
                    {
                        FormNewOrder newOrder = new FormNewOrder();
                        newOrder.Show();
                        this.Hide();
                    }
                }
                catch
                {
                    MessageBox.Show("Введите другой номер заказа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCode.Text != "")
            {
                id = Convert.ToInt32(textBoxCode.Text);
            }
        }
    }
}
