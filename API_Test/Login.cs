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
using RestSharp;
using Newtonsoft.Json;

namespace API_Test
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public static string companyName = "";
        public static string UserName = "";
        public static string Password = "";
        public static string DomainLoged = "";
        public static string companyNumber = "";

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (txtDomain.Text != "" && txtCompanyName.Text != "" && txtUserName.Text != "" && txtPassword.Text != "")
            {
                var client = new RestClient("https://" + txtDomain.Text + "/modules/api3/auth.php");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Cookie", "FAb3708d6ca6c9f028f053d60aefbdffaf=guo4pvkvq6p4amhg7rv78cpc37");
                request.AddParameter("X-COMPANY", "" + txtCompanyName.Text + "");
                request.AddParameter("X-USER", "" + txtUserName.Text + "");
                request.AddParameter("X-PASSWORD", "" + txtPassword.Text + "");
                IRestResponse response = client.Execute(request);

                LoginInfo log = JsonConvert.DeserializeObject<LoginInfo>(response.Content);

                if (log.state == "success")
                {
                    Form1 f = new Form1();
                    f.Show();
                    companyName = txtCompanyName.Text;
                    UserName = txtUserName.Text;
                    Password = txtPassword.Text;
                    DomainLoged = txtDomain.Text;
                    companyNumber = log.companyNumber;
                }
                else
                {
                    MessageBox.Show("Invalid Login , Please Recheck Your Info !!");
                }

                using (SqlConnection connection = new SqlConnection(ConnectionString.Connection()))
                using (SqlCommand command = new SqlCommand("InsertCompanyInfo", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@domain", SqlDbType.NVarChar);
                    command.Parameters.Add("@companyName", SqlDbType.NVarChar);
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar);
                    command.Parameters.Add("@password", SqlDbType.NVarChar);
                    command.Parameters.Add("@date", SqlDbType.DateTime);
                    command.Parameters.Add("@state", SqlDbType.NVarChar);
                    command.Parameters.Add("@companyNumber", SqlDbType.NVarChar);
                    command.Parameters.Add("@user", SqlDbType.NVarChar);

                    command.Parameters["@domain"].Value = txtDomain.Text;
                    command.Parameters["@companyName"].Value = txtCompanyName.Text;
                    command.Parameters["@UserName"].Value = txtUserName.Text;
                    command.Parameters["@password"].Value = txtPassword.Text;
                    command.Parameters["@date"].Value = DateTime.Now;
                    command.Parameters["@state"].Value = log.state;
                    command.Parameters["@companyNumber"].Value = log.companyNumber;
                    command.Parameters["@user"].Value = log.user;

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please Insert All Required Fields !!");
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol =
                                      System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

        }
        
        private void Login_Load_1(object sender, EventArgs e)
        {
           
        }
    }
}
