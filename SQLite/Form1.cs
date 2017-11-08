using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //create table and insert
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            //string sql = "create table highscores (name varchar(20), score int)";
            string sql = @"CREATE TABLE IF NOT EXISTS artist (  
                                artistid INTEGER PRIMARY KEY,
                                artistname  TEXT
                            )";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            //sql = "insert into highscores (name, score) values ('Me', 9001)";
            sql = "insert into artist (artistname) values ('Phil Collins')";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //read first row in table
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
            using (m_dbConnection)
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter("select * from artist", m_dbConnection);
                SQLiteDataReader dr = sda.SelectCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    label1.Text = dr["artistname"].ToString();
                }
            }
        }

    }
}
