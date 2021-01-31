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
    public partial class Interface : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=YOUSSEF-PC\SQLEXPRESS;Initial Catalog=messagerie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        string q = "";
        public Interface()
        {
            InitializeComponent();
        }

        private void Interface_Load(object sender, EventArgs e)
        {
            label3.Text += Connection.Id;
            q = "select * from conversation where uide ='" + Connection.Id + "'";
            cmd = new SqlCommand(q, cn);
            cn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[1].ToString());
            }
            cn.Close();
            q = "select * from conversation where uidr ='" + Connection.Id + "'";
            cmd = new SqlCommand(q, cn);
            cn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[0].ToString());
            }
            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(Connection.Id != textBox2.Text)
                {
                    q = "insert into conversation(uide, uidr) values('" + Connection.Id + "','" + textBox2.Text + "')";
                    cmd = new SqlCommand(q, cn);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cn.Close();
                    //refresh:
                    listBox1.Items.Clear();
                    q = "select * from conversation where uide ='" + Connection.Id + "'";
                    cmd = new SqlCommand(q, cn);
                    cn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        listBox1.Items.Add(dr[1].ToString());
                    }
                    cn.Close();
                    textBox2.Clear();
                }
                else
                {
                    MessageBox.Show("entrer un utilisateur different!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }
        int idc;
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                q = "select idc from conversation where uide ='" + Connection.Id + "' and uidr ='" + listBox1.SelectedItem.ToString() + "'";
                cmd = new SqlCommand(q, cn);
                cn.Open();
                int i = (int)cmd.ExecuteScalar();
                idc = i;
                cn.Close();
                //
                q = "select * from message where idc = " + i.ToString();
                cmd = new SqlCommand(q, cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listBox2.Items.Add("(" + dr[2].ToString() + ") " + dr[3].ToString() + ": " + dr[0].ToString());
                }
                cn.Close();
            }
            catch
            {
                cn.Close();
                q = "select idc from conversation where uidr ='" + Connection.Id + "' and uide ='" + listBox1.SelectedItem.ToString() + "'";
                cmd = new SqlCommand(q, cn);
                cn.Open();
                int i = (int)cmd.ExecuteScalar();
                idc = i;
                cn.Close();
                //
                q = "select * from message where idc = " + i.ToString();
                cmd = new SqlCommand(q, cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listBox2.Items.Add("(" + dr[2].ToString() + ") " + dr[3].ToString() + ": " + dr[0].ToString());
                }
                cn.Close();
            }
            groupBox2.Text = "Id:" + listBox1.SelectedItem.ToString();
            textBox1.ReadOnly = false;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    q = "insert into message values('" + textBox1.Text + "'," + idc.ToString() + ", getdate(),'" + Connection.Id + "')";
                    cmd = new SqlCommand(q, cn);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    listBox2.Items.Clear();
                    //
                    q = "select * from message where idc = " + idc.ToString();
                    cmd = new SqlCommand(q, cn);
                    cn.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        listBox2.Items.Add("(" + dr[2].ToString() + ") " + dr[3].ToString() + ": " + dr[0].ToString());
                    }
                    cn.Close();
                    textBox1.Clear();
                }
                else
                {
                    MessageBox.Show("entrer un message pour envoyé");
                }
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
