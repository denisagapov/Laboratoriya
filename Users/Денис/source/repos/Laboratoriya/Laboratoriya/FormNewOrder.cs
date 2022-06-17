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
    public partial class FormNewOrder : Form
    {
        string namePacient;
        string namePacietnDb;
        int idPacient;
        public FormNewOrder()
        {
            InitializeComponent();
            textBoxCode.Text = DateTime.Now.ToString("ddMMyyyy") + "000000";
            ShowServices();
        }
        public void ShowServices()
        {
            comboBoxServices.Items.Clear();
            foreach (service _service in Program.labDB.service)
            {
                string[] item = { _service.Service1, "-", _service.Deadline.ToString() };
                comboBoxServices.ValueMember = _service.Code.ToString();
                comboBoxServices.Items.Add(string.Join(" ",item));
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            bool key = false;
            namePacient = textBoxPacient.Text;
            foreach (patient _patient in Program.labDB.patient)
            {
                namePacietnDb = _patient.name + " " + _patient.surname;
                if (namePacient == namePacietnDb)
                {
                    key = true;
                    idPacient = _patient.id;
                }
            }
            if (!key)
            {
                MessageBox.Show("Пациент не найден в базе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                FormPatients patietns = new FormPatients();
                patietns.Show();
            }
            else
            {
                MessageBox.Show("Пациент найден в базе", "Успешный поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            order _order = new order();
            _order.code = textBoxCode.Text;
            _order.id = FormCodeOrder.id;
            _order.id_service = Convert.ToInt32(comboBoxServices.ValueMember);
            _order.date_start = DateTime.Now;
            int number = Convert.ToInt32(comboBoxServices.SelectedItem.ToString().Split('-')[1]);
            _order.date_end = DateTime.Now.AddDays(number);
            _order.id_laborant = FormAuthorization.user.id;
            _order.id_patient = idPacient;
            Program.labDB.order.Add(_order);
            Program.labDB.SaveChanges();
            MessageBox.Show("Успешное добавение", "Успешный поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            FormListOrder listOrder = new FormListOrder();
            listOrder.Show();
        }
    }
}
