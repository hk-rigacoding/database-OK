using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace database
{
    public partial class Form1 : Form
    {
        //mūsu mysql servera pieslēguma dati
        //string conn_str = "server=localhost;user=root;database=test;port=3306;password=jaguar";
        //string conn_str = "server=localhost;user=pulkstenis;database=test;port=3306;password=parole";
        string conn_str = "server=localhost;user=pulkstenis;port=3306;password=parole";

        //mysql klienta instance
        MySqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ja nav instancēts, instancējam
            if (conn == null)
                conn = new MySqlConnection(conn_str);


            //ja query ir tukšs, tad atveram konekciju
            if (query.Text == "")
            {
                try
                {
                    log.Text = "Opening Connection to DB Server ... " + Environment.NewLine + log.Text;
                    conn.Open();
                    log.Text = "SUCCESS !" + Environment.NewLine + log.Text;
                    query.ReadOnly = false;

                }
                catch (Exception exc)
                {
                    log.Text = exc.Message + Environment.NewLine + log.Text;
                }
            } else // aizpildīts query
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query.Text, conn);


                    //if (query.Text.ToLower().Contains("select"))
                    if (true)
                    {
                        //gadījums, kad izgūstam datus no datubāzes
                        //cmd.ExecuteReader

                        //veidu, kā izgūt datus
                        MySqlDataReader reader = cmd.ExecuteReader();//sūtam query.text saturu komandu

                        while (reader.Read())
                        {
                            string temp2 = "";

                            for (int i=0; i< reader.FieldCount; i++)
                                temp2 += reader[i].ToString();

                            log.Text = temp2 + Environment.NewLine + log.Text;
                        }

                        reader.Close();

                    } else
                    {
                        //gadījums, kad mums nav atbildes datu
                        cmd.ExecuteNonQuery();


                    }



                }
                catch (Exception exc)
                {
                    log.Text = exc.Message + Environment.NewLine + log.Text;
                }

            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
        }
    }
}
