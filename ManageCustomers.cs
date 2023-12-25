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
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
        }

        private Home home = null;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\CarRentalManagementSystem\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");


        private void ManageCustomers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void clearFields()
        {
            textBoxID.Clear();
            textBoxName.Clear();
            textBoxPhoneNo.Clear();
            textBoxAddress.Clear();
        }

        public void populate()
        {
            con.Open();

            string query = "select * from CustomerTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CustomersDGV.DataSource = ds.Tables[0];

            con.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == "" || textBoxName.Text == "" || textBoxAddress.Text == "" || textBoxPhoneNo.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "insert into CustomerTbl values(" + textBoxID.Text + ",'" + textBoxName.Text + "','" + textBoxAddress.Text + "','" + textBoxPhoneNo.Text + "');";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer successfully added!");
                    clearFields();

                    con.Close();

                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == "" || textBoxName.Text == "" || textBoxAddress.Text == "" || textBoxPhoneNo.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "update CustomerTbl set CustName='" + textBoxName.Text + "',CustAdd='" + textBoxAddress.Text + "',Phone='" + textBoxPhoneNo.Text + "' where CustID='" + textBoxID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer successfully edited!");
                    clearFields();

                    con.Close();

                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        private void CustomersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = CustomersDGV.SelectedRows[0].Cells[0].Value.ToString();
            textBoxName.Text = CustomersDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxAddress.Text = CustomersDGV.SelectedRows[0].Cells[2].Value.ToString();
            textBoxPhoneNo.Text = CustomersDGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "delete from CustomerTbl where CustID = '" + textBoxID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer successfully deleted!");
                    clearFields();

                    con.Close();

                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            if (home == null || home.IsDisposed)
            {
                home = new Home();
            }

            home.Show();
            this.Hide();
        }
    }
}
