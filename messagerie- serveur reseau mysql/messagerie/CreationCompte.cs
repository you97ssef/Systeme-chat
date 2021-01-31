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
    public partial class CreationCompte : Form
    {
        MySqlConnection cn = new MySqlConnection(@"server =remotemysql.com; uid=v2kN8du8nI; pwd=nuHoAeCC0E;database=v2kN8du8nI;");
        //SqlConnection cn = new SqlConnection(@"Data Source=YOUSSEF-PC\SQLEXPRESS;Initial Catalog=messagerie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        MySqlCommand cmd = new MySqlCommand();
        string q = "";
        public CreationCompte()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {
                q = "insert into users values('" + textBox1.Text + "','" + textBox2.Text + "')";
                cmd = new MySqlCommand(q, cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (textBox2.Text == textBox3.Text && i == 1)
                {
                    MessageBox.Show("Compte crée successivement !!");
                    Connection f = new Connection();
                    f.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mot de passe ou compte erroné !!");
                }
                cn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void CreationCompte_Load(object sender, EventArgs e)
        {

        }

        private void CreationCompte_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
