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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }

        private Home home = null;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\CarRentalManagementSystem\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
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

        private void populateRet()
        {
            con.Open();
            string query = "select * from ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void Deleteonreturn()
        {
            int rentId;
            rentId = Convert.ToInt32(RentalDGV.SelectedRows[0].Cells[0].Value.ToString());
            con.Open();
            string query = "delete from RentalTbl where RentID=" + rentId + ";";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            populate();
        }

        private void Return_Load(object sender, EventArgs e)
        {
            populate();
            populateRet();
        }

        private void RentalDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxCarReg.Text = RentalDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxCustName.Text = RentalDGV.SelectedRows[0].Cells[2].Value.ToString();
            dateTimePickerReturnDate.Text = RentalDGV.SelectedRows[0].Cells[4].Value.ToString();
            DateTime d1 = dateTimePickerReturnDate.Value.Date;
            DateTime d2 = DateTime.Now;
            TimeSpan t = d2 - d1;
            int NrOfDays = Convert.ToInt32(t.TotalDays);
            if (NrOfDays <= 0)
            {
                textBoxDelay.Text = "No Fine";
                textBoxFine.Text = "0";
            }
            else
            {
                textBoxDelay.Text = "" + NrOfDays;
                textBoxFine.Text = "" + (NrOfDays * 250);
            }
        }


        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == "" || textBoxCustName.Text == "" || textBoxFine.Text == "" || textBoxDelay.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into ReturnTbl values(" + textBoxID.Text + ",'" + textBoxCarReg.Text + "','" + textBoxCustName.Text + "','" + dateTimePickerReturnDate.Text + "','" + textBoxDelay.Text + "','" + textBoxFine.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Dully Returned!");
                    con.Close();
                    populateRet();
                    Deleteonreturn();
                    updateonRent();
                    clearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void clearFields()
        {
            textBoxID.Clear();
            textBoxCustName.Clear();
            textBoxCarReg.Clear();
            textBoxDelay.Clear();
            textBoxFine.Clear();
        }

        public void updateonRent()
        {
            con.Open();

            string query = "update CarTbl set Available='" + "Yes" + "' where RegNo='" + textBoxCarReg.Text + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Car successfully edited!");
            clearFields();

            con.Close();

            populate();
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

        private void ReturnDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = ReturnDGV.SelectedRows[0].Cells[0].Value.ToString();
            textBoxCarReg.Text = ReturnDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxCustName.Text = ReturnDGV.SelectedRows[0].Cells[2].Value.ToString();
            dateTimePickerReturnDate.Text = ReturnDGV.SelectedRows[0].Cells[3].Value.ToString();
            textBoxDelay.Text = ReturnDGV.SelectedRows[0].Cells[4].Value.ToString();
            textBoxFine.Text = ReturnDGV.SelectedRows[0].Cells[5].Value.ToString();
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

                    string query = "delete from ReturnTbl where ReturnID = '" + textBoxID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Return detail successfully deleted!");
                    clearFields();

                    con.Close();
                    populateRet();

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
