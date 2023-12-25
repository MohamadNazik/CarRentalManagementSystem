using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalManagementSystem
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private ManageCars car = null;
        //private ManageCustomers customer = null;
        //private Rental rental = null;
        private Return rt = null;
        //private Dashboard dashboard= null;
        //private ManageUsers user = null;

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonCar_Click(object sender, EventArgs e)
        {
            if (car == null || car.IsDisposed)
            {
                car = new ManageCars();
            }

            car.Show();
            this.Hide();
        }

        private void buttonCustomer_Click(object sender, EventArgs e)
        {
            //if (customer == null || customer.IsDisposed)
            //{
            //    customer = new ManageCustomers();
            //}

            //customer.Show();
            //this.Hide();
        }

        private void buttonRental_Click(object sender, EventArgs e)
        {
            //if (rental == null || rental.IsDisposed)
            //{
            //    rental = new Rental();
            //}

            //rental.Show();
            //this.Hide();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            if (rt == null || rt.IsDisposed)
            {
                rt = new Return();
            }

            rt.Show();
            this.Hide();
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            //if (dashboard == null || dashboard.IsDisposed)
            //{
            //    dashboard = new Dashboard();
            //}

            //dashboard.Show();
            //this.Hide();
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            //if (user == null || user.IsDisposed)
            //{
            //    user = new ManageUsers();
            //}

            //user.Show();
            //this.Hide();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
