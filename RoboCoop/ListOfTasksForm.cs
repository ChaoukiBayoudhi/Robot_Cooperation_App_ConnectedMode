using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboCoop
{
    public partial class ListOfTasksForm : Form
    {
        public ListOfTasksForm()
        {
            InitializeComponent();
        }
        
        private void ListOfTasksForm_Load(object sender, EventArgs e)
        {
            //step 1 : create a connection
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = @"Data Source=DESKTOP-7J9ODH9;Initial Catalog=RobotDB6;Integrated Security=True;Pooling=False";
                //step 2 :Open the connection
                con.Open();
                //step 3 : Create Command object
                SqlCommand cmd = new SqlCommand("select * from task",con);
                
                //step 4 :Execute the command
                SqlDataReader Dr=cmd.ExecuteReader();
                if (Dr == null)
                    throw new Exception("Verifier Votre requete");
                List<Task> lst1 = new List<Task>();
                while(Dr.Read())
                {
                    Task t1 = new Task();
                    t1.Code = Dr.GetInt32(0);
                    t1.Name = Dr.GetString(1);
                    t1.duration = SafeAccess.SafeGetDouble(Dr,2);
                    t1.status = SafeAccess.SafeGetString(Dr,3);
                    lst1.Add(t1);
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lst1;                


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
