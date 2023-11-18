﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace anbar
{
    public partial class Form_country : Form
    {
        static OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb;Persist Security Info=True");
        OleDbDataAdapter da = new OleDbDataAdapter("", con);
        DataSet ds = new DataSet();

        public Form_country()
        {
            InitializeComponent();
        }
        string nam;

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text == "")
            {
                MessageBox.Show("پرش کن");
            }
            else
            {
                ds.Clear();
                da.SelectCommand.CommandText = "select name from country where name='" + textBoxX1.Text + "'";
                da.Fill(ds, "d1");
                if (ds.Tables["d1"].Rows.Count > 0)
                {
                    DialogResult d = MessageBox.Show("این رکورد قبلا ثبت شده است ", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxX1.Clear();

                }
                else
                {
                    DataBase db = new DataBase();
                    db.DoCommand("insert into country(name) values('" + textBoxX1.Text + "')");
                    Form_country_Load(sender, e);
                    dataGridViewX1.CurrentCell = dataGridViewX1.Rows[dataGridViewX1.RowCount - 2].Cells[0];
                    MessageBox.Show("Inserted");
                }
            }
        }

        private void Form_country_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataBase db = new DataBase();
            dt = db.MySelect("select * from country");
            dataGridViewX1.DataSource = dt;
            dataGridViewX1.Columns[0].HeaderText = "نام ";
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            db.DoCommand("update country set name='" + textBoxX1.Text+ "' where name='" + nam +"'");
            //'UPDATE DATAGRID
            Form_country_Load(sender, e);
            MessageBox.Show("Updated"); 
        }

        private void dataGridViewX1_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxX1.Text = dataGridViewX1[0, dataGridViewX1.CurrentRow.Index].Value.ToString();
            nam = textBoxX1.Text;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                DataBase db = new DataBase();
                db.DoCommand("delete from country where name='" + textBoxX1.Text + "'");
                //'update datagrid
                Form_country_Load(sender, e);
                MessageBox.Show("Deleted");
            }
            else { }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            string str = "select * from country where ";

            //'inja deghat konid ke mitavan faghat ghesmati az name ra nevesht va jostejoo kard 
            if (textBoxX1.Text != "") str += "name like '%" + textBoxX1.Text + "' and ";
            if (str == "select * from country where ")
                str = "select * from country";
            else
                str = str.Remove(str.Length - 4, 4);
            DataTable dt = new DataTable();
            DataBase db = new DataBase();
            dt = db.MySelect(str);
            MessageBox.Show(dt.Rows.Count.ToString() + " Rows founded.");
            dataGridViewX1.DataSource = dt;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dataGridViewX1);
        }
        }
    }
