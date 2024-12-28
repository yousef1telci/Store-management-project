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
    public partial class frmAdmin : Form
    {
        // connection with server
        static String strConn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        SqlConnection sqlConn = new SqlConnection(strConn);

        public frmAdmin()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Form newForm = new frmMain();
            newForm.ShowDialog();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("sp_selectAll", sqlConn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvAdmin.DataSource = dt;

                //dgvAdmin.Columns[5].Visible = false;
                dgvAdmin.Columns[0].Width = (dgvAdmin.Width * 5) / 100;
                dgvAdmin.Columns[1].Width = (dgvAdmin.Width * 15)/100 ;
                dgvAdmin.Columns[2].Width = (dgvAdmin.Width * 20)/100 ;
                dgvAdmin.Columns[3].Width = (dgvAdmin.Width *15 )/100 ;
                dgvAdmin.Columns[4].Width = (dgvAdmin.Width * 20)/100 ;
                dgvAdmin.Columns[5].Width = (dgvAdmin.Width * 20)/100;
                dgvAdmin.Columns[6].Width = (dgvAdmin.Width * 20) /100;
            }
            catch (Exception ex)
            {
                Form error = new frmError("ERROR: myChatGpt_Load ");
                error.ShowDialog();
            }
        }

        private void txtSearch_MouseDown(object sender, MouseEventArgs e)
        {
            txtSearch.SelectAll();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            String id = dgvAdmin.CurrentRow.Cells[0].Value.ToString();
            Form edit = new frmMain(id);
            edit.ShowDialog();
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
                    dgvAdmin.DataSource = dt;
                    dgvAdmin.Columns[0].Width = (dgvAdmin.Width * 5) / 100;
                    dgvAdmin.Columns[1].Width = (dgvAdmin.Width * 15) / 100;
                    dgvAdmin.Columns[2].Width = (dgvAdmin.Width * 20) / 100;
                    dgvAdmin.Columns[3].Width = (dgvAdmin.Width * 15) / 100;
                    dgvAdmin.Columns[4].Width = (dgvAdmin.Width * 20) / 100;
                    dgvAdmin.Columns[5].Width = (dgvAdmin.Width * 20) / 100;
                    dgvAdmin.Columns[6].Width = (dgvAdmin.Width * 20) / 100;

                }
                else
                {
                    Application.Restart();
                    //dgvAdmin.Refresh();
                    this.Refresh();

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = dgvAdmin.CurrentRow.Cells[0].Value.ToString();
            Form frmDel = new frmMessage(id,this.dgvAdmin);
            frmDel.ShowDialog();    
        }
    }
}
