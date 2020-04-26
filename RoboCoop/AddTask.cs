using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace RoboCoop
{
    public partial class AddTask : Form
    {
        public AddTask()
        {
            InitializeComponent();
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            //step 1 : create a connection
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = @"Data Source=DESKTOP-7J9ODH9;Initial Catalog=RobotDB6;Integrated Security=True;Pooling=False";
                //step 2 :Open the connection
                con.Open();
                //step 3 : Create Command object and intialize its properties
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into Task(name,duration,status) values(@name,@duration,@satus)";
                cmd.CommandType = CommandType.Text;//defaut value for this property is TEXT
                cmd.Parameters.AddWithValue("@name", txt_name.Text);
                cmd.Parameters.AddWithValue("@duration", float.Parse(txt_duration.Text));
                cmd.Parameters.AddWithValue("@satus", cmb_status.Text);
                //step 4 :Execute the command
                cmd.ExecuteNonQuery();
                MessageBox.Show("Tacha ajoutée avec succés...");


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
