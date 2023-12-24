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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //private Home home = null;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\CarRentalManagementSystem\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");


        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxUserName.Clear();
            textBoxPassword.Clear();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string query = "select count(*) from UserTbl where Uname='" + textBoxUserName.Text + "' and Upassword='" + textBoxPassword.Text + "';";

            con.Open();
            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            if(dt.Rows[0][0].ToString() == "1")
            {
                //home = new Home();
                //home.Show();
                //this.Hide();
            }
            else
            {
                MessageBox.Show("Wromg UserName or Password!!");
            }
            con.Close();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
