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
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }

        private Home home = null;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\CarRentalManagementSystem\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

        public void fillCombo()
        {
            con.Open();

            string query = "select RegNo from CarTbl where Available='"+"Yes"+"';";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNo", typeof(string));
            dt.Load(rdr);
            comboBoxCarReg.ValueMember = "RegNo";
            comboBoxCarReg.DataSource = dt;

            con.Close();
        }

        public void fillCustomer()
        {
            con.Open();

            string query = "select CustID from CustomerTbl";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustID", typeof(string));
            dt.Load(rdr);
            comboBoxCustID.ValueMember = "CustID";
            comboBoxCustID.DataSource = dt;

            con.Close();
        }

        public void fetchCustName()
        {
            con.Open();

            string query = "select * from CustomerTbl where CustID=" + comboBoxCustID.SelectedValue.ToString() + ";";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                textBoxCustName.Text = dr["CustName"].ToString();
            }

            con.Close();
        }

        private void Rental_Load(object sender, EventArgs e)
        {
            fillCombo();
            fillCustomer();
            populate();
        }

        private void comboBoxCarReg_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void comboBoxCustID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustName();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        public void clearFields()
        {
            textBoxID.Clear();
            textBoxCustName.Clear();
            textBoxFee.Clear();
        }

        public void populate()
        {
            con.Open();

            string query = "select * from RentalTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentalDGV.DataSource = ds.Tables[0];

            con.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == "" || comboBoxCarReg.SelectedValue.ToString() == "" || comboBoxCustID.SelectedValue.ToString() == "" || textBoxCustName.Text == "" || dateTimePickerRentalDate.Text == "" || dateTimePickerReturnDate.Text == "" || textBoxFee.Text == "")        
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "insert into RentalTbl values('" + textBoxID.Text + "','" + comboBoxCarReg.SelectedValue.ToString() + "','" + textBoxCustName.Text + "','" + dateTimePickerRentalDate.Text + "','" + dateTimePickerReturnDate.Text + "'," + textBoxFee.Text + ");";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car successfully rented!");
                    clearFields();

                    con.Close();

                    updateonRent();

                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
        }

        
        private void RentalDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = RentalDGV.SelectedRows[0].Cells[0].Value.ToString();
            comboBoxCarReg.Text = RentalDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxCustName.Text = RentalDGV.SelectedRows[0].Cells[2].Value.ToString();
            dateTimePickerRentalDate.Text = RentalDGV.SelectedRows[0].Cells[3].Value.ToString();
            dateTimePickerReturnDate.Text = RentalDGV.SelectedRows[0].Cells[4].Value.ToString();
            textBoxFee.Text = RentalDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        public void updateonRent()
        {
            con.Open();

            string query = "update CarTbl set Available='" + "No" + "' where RegNo='" + comboBoxCarReg.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Car successfully edited!");
            clearFields();

            con.Close();

            populate();
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

                    string query = "delete from RentalTbl where RentID = '" + textBoxID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental detail successfully deleted!");
                    clearFields();

                    con.Close();

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
            if (textBoxID.Text == "" || comboBoxCarReg.SelectedValue.ToString() == "" || comboBoxCustID.SelectedValue.ToString() == "" || textBoxCustName.Text == "" || dateTimePickerRentalDate.Text == "" || dateTimePickerReturnDate.Text == "" || textBoxFee.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "update RentalTbl set CarReg='" + comboBoxCarReg.SelectedValue.ToString() + "',CustName='" + textBoxCustName.Text + "',RentalDate='" + dateTimePickerRentalDate.Text + "',ReturnDate='" + dateTimePickerReturnDate.Text + "',RentFee=" + textBoxFee.Text + " where RentID='" + textBoxID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental details successfully updated!");
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
    }
}
