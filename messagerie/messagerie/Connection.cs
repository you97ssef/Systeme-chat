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

namespace messagerie
{
    public partial class Connection : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=YOUSSEF-PC\SQLEXPRESS;Initial Catalog=messagerie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd = new SqlCommand();
        string q = "";

        public static string Id = "";

        public Connection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            q = "select count(*) from users where id ='" + textBox1.Text + "' and mdp ='" + textBox2.Text + "'";
            cmd = new SqlCommand(q, cn);
            cn.Open();
            int i = (int)cmd.ExecuteScalar();
            if(i==1)
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
    }
}
