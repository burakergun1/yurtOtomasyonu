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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace yurtVeriTabanı
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        private Baglanti bgl = new Baglanti();
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = bgl.Open();
            cmd.CommandText = "SELECT ay_adi,ay_id FROM Aylar";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmb_ay.Items.Add(dr[0].ToString());
            }
            cmd.Dispose();
            bgl.Close();

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT ay_adi,para FROM Aylar INNER JOIN Kasa ON Kasa.ay_id = Aylar.ay_id", bgl.Open());
            DataSet ds = new DataSet();
            da.Fill(ds, "Aylar,Kasa");
            dataGridView1.DataSource = ds.Tables["Aylar,Kasa"];

            bgl.Close();
        }

        private void btn_anamenu_Click(object sender, EventArgs e)
        {
           
        }

        private void cmb_ay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_ay.SelectedIndex != -1)
            {
                label_kasa.Text = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = bgl.Open();
                cmd.CommandText = "SELECT ay_id FROM Aylar WHERE ay_adi = '" + cmb_ay.SelectedItem.ToString() + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    txt_ay_id.Text = dr[0].ToString();
                cmd.Dispose();
                bgl.Close();
            }
        }

        private void btn_kasa_Click(object sender, EventArgs e)
        {
            label_kasa.Visible = true;

            if (cmb_ay.SelectedIndex == 0)
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = bgl.Open();
                cmd2.CommandText = "SELECT sum(para) FROM KASA ";
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                    label_kasa.Text = dr2[0].ToString() + " TL";
                cmd2.Dispose();
                bgl.Close();
                
            }

            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = bgl.Open();
                cmd.CommandText = "SELECT para FROM KASA WHERE ay_id = " + txt_ay_id.Text + "";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    label_kasa.Text = dr[0].ToString() + " TL";
                cmd.Dispose();
                bgl.Close();
            }
            
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmAnaMenu form = new FrmAnaMenu();
            form.Show();
            this.Hide();
        }
    } }
