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
using System.IO;
using System.Drawing.Imaging;

namespace Stores_Management
{
    public partial class frmMain : Form
    {
        // baglanama ifadesi 
        static string strConn = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        SqlConnection sqlConn = new SqlConnection(strConn);

        string id;
        string CategotyTxt;

        public frmMain()
        {
            InitializeComponent();
            txtId.Enabled = false;
        }

        //this for edit button
        public frmMain(string id)
        {
            InitializeComponent();
            this.id = id;

            try
            {
                SqlCommand sqlComm = new SqlCommand("sp_selectid", sqlConn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@id", id);
                sqlConn.Open();
                SqlDataReader dr = sqlComm.ExecuteReader();
                while (dr.Read())
                {
                    txtId.Text = dr[0].ToString();
                    CategotyTxt = dr[1].ToString();
                    txtName.Text = dr[2].ToString();  //سيتم عرض لغات البرمجة وقت اللود تبع الفورم
                    txtCity.Text = dr[3].ToString();
                    txtStreet.Text = dr[4].ToString();
                    txtEmail.Text = dr[5].ToString();
                    txtPhone.Text = dr[6].ToString();

                }
            }
            catch (Exception)
            {
                Form error = new frmError("Error: frmMain(String id) error");
                error.ShowDialog();
            }
            finally
            {
                sqlConn.Close();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                //display the languages of the comboBox
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("sp_selectCategory", sqlConn);
                da.Fill(dt);

                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "categorys";
            }
            catch (Exception ex)
            {
                Form error = new frmError("frmMain Error");
                error.ShowDialog();
            }

            if (!String.IsNullOrEmpty(CategotyTxt))
            {
                cmbCategory.Text = CategotyTxt;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // تحقق مما إذا كانت قيمة id فارغة  //(New)
            if (String.IsNullOrEmpty(id))
            {
                try
                {

                    SqlCommand sqlComm = new SqlCommand("sp_addEntry", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@category", cmbCategory.Text);
                    sqlComm.Parameters.AddWithValue("@store_name", txtName.Text);
                    sqlComm.Parameters.AddWithValue("@city", txtCity.Text);
                    sqlComm.Parameters.AddWithValue("@street", txtStreet.Text);
                    sqlComm.Parameters.AddWithValue("@email", txtEmail.Text); 
                    sqlComm.Parameters.AddWithValue("@phone", txtEmail.Text);

                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("error:" + ex.ToString());
                    Form error = new frmError("ERROR: bnt save ");
                    error.ShowDialog();
                }
                finally
                {
                    sqlConn.Close();
                    Application.Restart();
                   

                }
            }
            else  //(Edit)
            {
                try
                {
                    
                    // Update Info
                    SqlCommand sqlComm = new SqlCommand("sp_editEntry", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@id", id);
                    sqlComm.Parameters.AddWithValue("@category", cmbCategory.Text);
                    sqlComm.Parameters.AddWithValue("@store_name", txtName.Text);
                    sqlComm.Parameters.AddWithValue("@city", txtCity.Text);
                    sqlComm.Parameters.AddWithValue("@street", txtStreet.Text);
                    sqlComm.Parameters.AddWithValue("@email", txtEmail.Text);
                    sqlComm.Parameters.AddWithValue("@phone", txtEmail.Text);


                    sqlConn.Open();
                    sqlComm.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Form error = new frmError("bnt save (edit) Error!");
                }
                finally
                {
                    sqlConn.Close();
                    Application.Restart();
                }
            }
        }
    }
}

