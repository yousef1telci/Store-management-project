using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace Stores_Management
{

    public partial class frmMessage : Form
    {

        static string strConn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        SqlConnection sqlConn = new SqlConnection(strConn);

        string id;
        DataGridView dgvAdmin;

        public frmMessage(string id ,DataGridView dataGridView)
        {
            InitializeComponent();
            this.id = id;
            this.dgvAdmin = dataGridView;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sqlCommaned = new SqlCommand("sp_deleteEntry",sqlConn);

                sqlCommaned.CommandType = CommandType.StoredProcedure;

                sqlCommaned.Parameters.AddWithValue("@id", id);
                sqlConn.Open();
                sqlCommaned.ExecuteNonQuery();

                dgvAdmin.Refresh();
            }
            catch (Exception ex)
            {

                Form frmError = new frmError("Button yes Error!");
            }
            finally
            {
                sqlConn.Close();
               dgvAdmin.Refresh();
                this.Close();
            }
        }
    }
}
