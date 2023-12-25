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
    public partial class ManageCars : Form
    {
        public ManageCars()
        {
            InitializeComponent();
        }

        private Home home = null;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\CarRentalManagementSystem\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");


        private void ManageCars_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void clearFields()
        {
            textBoxRegNo.Clear();
            textBoxBrand.Clear();
            textBoxModel.Clear();
            textBoxPrice.Clear();
        }

        public void populate()
        {
            con.Open();

            string query = "select * from CarTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarsDGV.DataSource = ds.Tables[0];

            con.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxRegNo.Text == "" || textBoxBrand.Text == "" || textBoxModel.Text == "" || textBoxPrice.Text == "" || comboBoxAvailable.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "insert into CarTbl values('" + textBoxRegNo.Text + "','" + textBoxBrand.Text + "','" + textBoxModel.Text + "','" + comboBoxAvailable.Text + "','" + textBoxPrice.Text + "');";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car successfully added!");
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
            if (textBoxRegNo.Text == "" || textBoxBrand.Text == "" || textBoxModel.Text == "" || textBoxPrice.Text == "" || comboBoxAvailable.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "update CarTbl set Brand='" + textBoxBrand.Text + "',Model='" + textBoxModel.Text + "',Price='" + textBoxPrice.Text + "',Available='" + comboBoxAvailable.Text + "' where RegNo='" + textBoxRegNo.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car successfully edited!");
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

        private void CarsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxRegNo.Text = CarsDGV.SelectedRows[0].Cells[0].Value.ToString();
            textBoxBrand.Text = CarsDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxModel.Text = CarsDGV.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxAvailable.Text = CarsDGV.SelectedRows[0].Cells[3].Value.ToString();
            textBoxPrice.Text = CarsDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBoxRegNo.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "delete from CarTbl where RegNo = '" + textBoxRegNo.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car successfully deleted!");
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (home == null || home.IsDisposed)
            {
                home = new Home();
            }

            home.Show();
            this.Hide();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void comboBoxSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = "";

            if(comboBoxSearch.SelectedItem.ToString() == "Available")
            {
                flag = "Yes";
            }
            else
            {
                flag = "No";
            }

            con.Open();

            string query = "select * from CarTbl where Available = '" + flag + "';";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarsDGV.DataSource = ds.Tables[0];

            con.Close();
        }
    }
}
