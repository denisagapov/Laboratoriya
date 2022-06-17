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
    public partial class FormAuthorization : Form
    {
        public struct User
        {
            public int id;
            public string login;
            public string password;
            public string name;
            public string surname;
            public int id_type;
        }
        public static User user = new User();
        public FormAuthorization()
        {
            InitializeComponent();
            textBoxPassword.UseSystemPasswordChar = true;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            bool key = false;
            if (textBoxLogin.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Введите данные", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (user _user in Program.labDB.user)
                {
                    if (textBoxLogin.Text==_user.login && textBoxPassword.Text == _user.password)
                    {
                        key = true;
                        user.id = _user.id;
                        user.login = _user.login;
                        user.password = _user.password;
                        user.id_type = _user.id_type;
                        user.name = _user.name;
                        user.surname = _user.surname;
                    }
                }
                if (!key)
                {
                    MessageBox.Show("Неверные данные", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    FormMenu menu = new FormMenu();
                    menu.Show();
                    this.Hide();
                }
            }

        }

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPass.Checked) { textBoxPassword.UseSystemPasswordChar = false; }
            else textBoxPassword.UseSystemPasswordChar = true;
        }
    }
}
