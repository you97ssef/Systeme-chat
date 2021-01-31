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
using MySql.Data.MySqlClient;


namespace messagerie
{
    public partial class Connection : Form
    {
        MySqlConnection cn = new MySqlConnection(@"server =remotemysql.com; uid=v2kN8du8nI; pwd=nuHoAeCC0E;database=v2kN8du8nI;");
        //SqlConnection cn = new SqlConnection(@"Data Source=YOUSSEF-PC\SQLEXPRESS;Initial Catalog=messagerie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        MySqlCommand cmd = new MySqlCommand();
        string q = "";
        MySqlDataReader dr;

        public static string Id = "";

        public Connection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                q = "select count(*) from `users` where id ='" + textBox1.Text + "' and mdp ='" + textBox2.Text + "';";
                cmd = new MySqlCommand(q, cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                int i = Convert.ToInt32(dr[0].ToString());
                if (i == 1)
                {
                    Id = textBox1.Text;
                    Interface n = new Interface();
                    n.Show();
                    this.Hide();
                    // ouvrir conversation
                }
                else
                {
                    MessageBox.Show("mot de passe ou compte erroné !!");
                }
                cn.Close();
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreationCompte c = new CreationCompte();
            c.Show();
            this.Hide();
        }

        private void Connection_Load(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void Connection_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
