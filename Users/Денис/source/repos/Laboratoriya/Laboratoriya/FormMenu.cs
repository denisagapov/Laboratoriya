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
    public partial class FormMenu : Form
    {
        public string role;
        public FormMenu()
        {
            InitializeComponent();
            checkUser();
            showButtons();
            labelName.Text = FormAuthorization.user.name + " " + FormAuthorization.user.surname;
            labelRole.Text = role;
        }

        public void checkUser() // проверка роли у пользователя
        {
            if (FormAuthorization.user.id_type == 1) { role = "Администратор"; }
            if (FormAuthorization.user.id_type == 2) { role = "Лаборант"; }
        }

        public void showButtons() // отображение кнопок для определенной роли
        {
            if (role=="Администратор")
            {
                buttonPacients.Visible = true;
                buttonService.Visible = true;

            }

            if (role == "Лаборант")
            {
                buttonOrder.Visible = true;
                buttonPacients.Visible = true;
            }
        }

        private void buttonPacients_Click(object sender, EventArgs e)
        {
            FormListOrder listOrder = new FormListOrder();
            this.Hide();
            listOrder.Show();
        }

        private void buttonService_Click(object sender, EventArgs e)
        {
            FormServices services = new FormServices();
            this.Hide();
            services.Show();

        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            FormCodeOrder newOrder = new FormCodeOrder();
;           this.Hide();
            newOrder.Show();
        }
    }
}
