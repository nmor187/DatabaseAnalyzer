using System;
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

            //Store the first table in the database into a table variable
            DataTable table = testDatabaseDataSet.Tables[0];

            //Call function to find candidate keys
            findCandidateKey(table);

            //FIND FUNCTIONAL DEPENDENCIES
            //correlations between columns
            //begin in first column and compare with second, if two items in the first are the same then the two items in the compared column must also be the same
            //if not no functional dependency exists between the two columns
            //NEED: to figure out a way to iterate through all the possible comparisons between data



        }
        private void findCandidateKey(DataTable t)
        {

            //Number of columns and rows in table
            int numCol = t.Columns.Count;
            int numRow = t.Rows.Count;

            //FIND CANDIDATE KEYS
            //go through each column and see if the value in every row of that column is different
            //potentially: store elements of a column in an array, then if element already exists in array, exit, its not a candidate key

            //store values of each column an array
            for (int i = 0; i < numCol; i++)
            {
                string[] rowItems = new string[numRow];
                bool isCandidateKey = true;
                for (int j = 0; j < numRow; j++)
                {
                    //Check if the item in this row is already in the array
                    string currStr = t.Rows[j][i].ToString();
                    if (rowItems.Contains(currStr) == true)
                    {
                        isCandidateKey = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(t.Rows[j][i].ToString());
                        rowItems[j] = currStr;
                    }
                }
                if (isCandidateKey == true)
                {
                    MessageBox.Show("Column #" + i + " is a candidate key");
                }
            }
        }
    }
}
