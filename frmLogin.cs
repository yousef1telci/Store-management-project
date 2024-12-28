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
using System.Configuration;

namespace Stores_Management
{
    public partial class frmLogin : Form
    {
        // txt in config file
        static string strConn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        SqlConnection sqlConn = new SqlConnection(strConn);

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //check Admin and Password
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try 
            {

                string Admin = txtAdmin.Text;
                string Password = txtPassword.Text;
                bool Login = false;
                sqlConn.Open();

                SqlCommand sqlComm = new SqlCommand("sp_selectAdmin", sqlConn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataReader dataReader = sqlComm.ExecuteReader();

                while (dataReader.Read())
                {
                    if (Admin == dataReader["admin"].ToString() && Password == dataReader["password"].ToString())
                    {
                        Login = true;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("yanlis Admin veya password !!");
                    }
                }

                if (Login)
                {
                    MessageBox.Show("Giris basarili");
                    this.Close();
                    Form frm = new frmAdmin();
                    frm.ShowDialog();
                }

            }
            catch (Exception ex) {
                Form frmError = new frmError("ERROR In Login Page ");
                frmError.ShowDialog();
            }
            finally
            {
                sqlConn.Close();
            }


        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char) Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
