﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bOOKBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bOOKBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.testDatabaseDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'testDatabaseDataSet.BOOK' table. You can move, or remove it, as needed.
            this.bOOKTableAdapter.Fill(this.testDatabaseDataSet.BOOK);
            DataTable table = testDatabaseDataSet.Tables[0];
            Console.WriteLine("columns count: " + table.Columns.Count);
            Console.WriteLine("rows count: " + table.Rows.Count);
            //MessageBox.Show(testDatabaseDataSet.Tables[0].Columns.Count);
            //MessageBox.Show((testDatabaseDataSet.Tables[0].Rows[0][0].ToString()));
        }
    }
}
