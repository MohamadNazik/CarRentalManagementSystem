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
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        private Home home = null;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\CarRentalManagementSystem\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");


        private void ManageUsers_Load(object sender, EventArgs e)
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
            textBoxPassword.Clear();
        }

        public void populate()
        {
            con.Open();

            string query = "select * from UserTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];

            con.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if(textBoxID.Text == "" || textBoxName.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "insert into UserTbl values(" + textBoxID.Text + ",'" + textBoxName.Text + "','" + textBoxPassword.Text + "');";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User successfully added!");
                    clearFields();

                    con.Close();

                    populate();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
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

                    string query = "delete from UserTbl where id = " + textBoxID.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User successfully deleted!");
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

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = UserDGV.SelectedRows[0].Cells[0].Value.ToString();
            textBoxName.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxPassword.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == "" || textBoxName.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();

                    string query = "update UserTbl set Uname='"+textBoxName.Text+"',Upassword='"+textBoxPassword.Text+"' where id=" + textBoxID.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User successfully edited!");
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
            if(home == null || home.IsDisposed)
            {
                home = new Home();
            }

            home.Show();
            this.Hide();
        }
    }
}
