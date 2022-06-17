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
    public partial class FormPatients : Form
    {
        public FormPatients()
        {
            InitializeComponent();
            ShowPatietns();
        }
        public void ShowPatietns()
        {
            listViewPatietns.Items.Clear();
            foreach (patient patient in Program.labDB.patient)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                   patient.id.ToString(), patient.name, patient.surname,
                   patient.date.ToString("dd.MM.yyyy"),patient.pasport.ToString(),
                   patient.phone_number.ToString(),patient.email,
                   patient.insurance_policy.ToString()
                });
                item.Tag = patient;
                listViewPatietns.Items.Add(item);
            }
        }

        private void listViewPatietns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPatietns.SelectedItems.Count==1)
            {
                patient _patietn = listViewPatietns.SelectedItems[0].Tag as patient;
                textBoxName.Text = _patietn.name;
                textBoxSurname.Text = _patietn.surname;
                dateTimePickerBirthDay.Value = _patietn.date;
                textBoxPasport.Text = _patietn.pasport.ToString();
                textBoxPhone.Text = _patietn.phone_number.ToString();
                textBoxEmail.Text = _patietn.email;
                textBoxInsurancePolice.Text = _patietn.insurance_policy.ToString();
            }
            else
            {
                textBoxName.Text = "";
                textBoxSurname.Text = "";
                dateTimePickerBirthDay.Value = DateTime.Now;
                textBoxPasport.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
                textBoxInsurancePolice.Text = "";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                patient _patient = new patient();
                if (textBoxName.Text != "" && textBoxSurname.Text != "" && textBoxPasport.Text != "" &&
                    textBoxPhone.Text != "" && textBoxEmail.Text != "" && textBoxInsurancePolice.Text != "")
                {
                    _patient.name = textBoxName.Text;
                    _patient.surname = textBoxSurname.Text;
                    _patient.date = Convert.ToDateTime(dateTimePickerBirthDay.Value);
                    _patient.pasport = Convert.ToDouble(textBoxPasport.Text);
                    _patient.phone_number = Convert.ToDouble(textBoxPhone.Text);
                    _patient.email = textBoxEmail.Text;
                    _patient.insurance_policy = Convert.ToDouble(textBoxInsurancePolice.Text);
                    _patient.login = textBoxName.Text + textBoxSurname.Text;
                    _patient.password = "12345678";
                    Program.labDB.patient.Add(_patient);
                    Program.labDB.SaveChanges();
                    ShowPatietns();
                }
            }
            catch
            {
                MessageBox.Show("Заполните все данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (listViewPatietns.SelectedItems.Count == 1)
            {
                patient _patient = listViewPatietns.SelectedItems[0].Tag as patient;
                Program.labDB.patient.Remove(_patient);
                Program.labDB.SaveChanges();
                ShowPatietns();
            }
            textBoxName.Text = "";
            textBoxSurname.Text = "";
            textBoxPasport.Text = "";
            textBoxPhone.Text = "";
            textBoxEmail.Text = "";
            textBoxInsurancePolice.Text = "";
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewPatietns.SelectedItems.Count == 1)
                {
                    patient _patient = listViewPatietns.SelectedItems[0].Tag as patient;
                    if (textBoxName.Text != "" && textBoxSurname.Text != "" && textBoxPasport.Text != "" &&
                        textBoxPhone.Text != "" && textBoxEmail.Text != "" && textBoxInsurancePolice.Text != "")
                    {
                        _patient.name = textBoxName.Text;
                        _patient.surname = textBoxSurname.Text;
                        _patient.date = Convert.ToDateTime(dateTimePickerBirthDay.Value);
                        _patient.pasport = Convert.ToDouble(textBoxPasport.Text);
                        _patient.phone_number = Convert.ToDouble(textBoxPhone.Text);
                        _patient.email = textBoxEmail.Text;
                        _patient.insurance_policy = Convert.ToDouble(textBoxInsurancePolice.Text);
                        _patient.login = textBoxName.Text + textBoxSurname.Text;
                        _patient.password = "12345678";
                        Program.labDB.SaveChanges();
                        ShowPatietns();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Заполните все данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormCodeOrder codeOrder = new FormCodeOrder();
            codeOrder.Show();
        }
    }
}
