using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarRentalManagementSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private Home home = null;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\CarRentalManagementSystem\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");


        private void buttonBack_Click(object sender, EventArgs e)
        {
            if(home==null || home.IsDisposed)
            {
                home = new Home();
            }

            home.Show();
            this.Hide();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string queryCar = "select count(*) from CarTbl;";
            SqlDataAdapter carda = new SqlDataAdapter(queryCar, con);
            DataTable cardt = new DataTable();
            carda.Fill(cardt);
            labelCars.Text = cardt.Rows[0][0].ToString();

            string queryCustomer = "select count(*) from CustomerTbl;";
            SqlDataAdapter cusda = new SqlDataAdapter(queryCustomer, con);
            DataTable cusdt = new DataTable();
            cusda.Fill(cusdt);
            labelCustomers.Text = cusdt.Rows[0][0].ToString();

            string queryUsers = "select count(*) from UserTbl;";
            SqlDataAdapter userda = new SqlDataAdapter(queryUsers, con);
            DataTable userdt = new DataTable();
            userda.Fill(userdt);
            labelUsers.Text = userdt.Rows[0][0].ToString();
        }
    }
}
