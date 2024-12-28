using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stores_Management
{
    public partial class frmUsers : Form
    {
        // connection with server
        static String strConn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        SqlConnection sqlConn = new SqlConnection(strConn);
        public frmUsers()
        {
            InitializeComponent();
        }

        private void bntAdmin_Click(object sender, EventArgs e)
        {
            Form login = new frmLogin();
            login.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("sp_selectAll", sqlConn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsers.DataSource = dt;

                dgvUsers.Columns[0].Width = (dgvUsers.Width * 5) / 100;
                dgvUsers.Columns[1].Width = (dgvUsers.Width * 15) / 100;
                dgvUsers.Columns[2].Width = (dgvUsers.Width * 20) / 100;
                dgvUsers.Columns[3].Width = (dgvUsers.Width * 15) / 100;
                dgvUsers.Columns[4].Width = (dgvUsers.Width * 20) / 100;
                dgvUsers.Columns[5].Width = (dgvUsers.Width * 20) / 100;
                //dgvUsers.Columns[6].Width = (dgvUsers.Width * 20) / 100;
                dgvUsers.Columns[6].Visible = false;

            }
            catch (Exception ex)
            {
                Form error = new frmError("ERROR: FRM USERS ERROR ");
                error.ShowDialog();
            }
        }

        private void txtSearch_MouseDown(object sender, MouseEventArgs e)
        {
            txtSearch.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtSearch.Text))
                {
                    SqlDataAdapter sqlDa = new SqlDataAdapter("sp_selectSearch", sqlConn);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("@param", txtSearch.Text);
                    ///;
                    DataTable dt = new DataTable();
                    sqlDa.Fill(dt);
                    ///
                    dgvUsers.DataSource = dt;
                    dgvUsers.Columns[0].Width = (dgvUsers.Width * 5) / 100;
                    dgvUsers.Columns[1].Width = (dgvUsers.Width * 15) / 100;
                    dgvUsers.Columns[2].Width = (dgvUsers.Width * 20) / 100;
                    dgvUsers.Columns[3].Width = (dgvUsers.Width * 15) / 100;
                    dgvUsers.Columns[4].Width = (dgvUsers.Width * 20) / 100;
                    dgvUsers.Columns[5].Width = (dgvUsers.Width * 20) / 100;
                    //dgvUsers.Columns[6].Width = (dgvUsers.Width * 20) / 100;

                }
                else
                {
                    //Application.Restart();
                    dgvUsers.Refresh();
                }
            }
            catch
            {
                Form error = new frmError("txtSearch_TextChanged Error!");
                error.ShowDialog();
            }
            finally
            {
                sqlConn.Close();
            }
        }
    }
}
