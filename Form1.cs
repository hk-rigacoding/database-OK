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
                    log.AppendText("Opening Connection to DB Server ... ");
                    conn.Open();
                    log.AppendText ("SUCCESS !" + Environment.NewLine);
                    query.ReadOnly = false;

                }
                catch (Exception exc)
                {
                    log.AppendText(exc.Message + Environment.NewLine);
                }
            } else // aizpildīts query
            {
                try
                {
                    using (var cmd = new MySqlCommand(query.Text, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())//sūtam query.text saturu komandu
                        {
                            while (reader.Read())
                            {

                                for (int i = 0; i < reader.FieldCount; i++)
                                    log.AppendText(reader[i].ToString() + " ");

                                log.AppendText(Environment.NewLine);

                            }

                            reader.Close();
                        }


                    }

                }
                catch (Exception exc)
                {
                    log.AppendText(exc.Message + Environment.NewLine);
                }

                

            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
        }
    }
}
