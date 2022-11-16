using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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

        //init the dataTable variable to be used throughout the program
        DataTable dataTable = new DataTable();
        string[] functionalDependencies;
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'testDatabaseDataSet.BOOK' table. You can move, or remove it, as needed.

            //FIND FUNCTIONAL DEPENDENCIES
            //correlations between columns
            //begin in first column and compare with second, if two items in the first are the same then the two items in the compared column must also be the same
            //if not no functional dependency exists between the two columns
            //NEED: to figure out a way to iterate through all the possible comparisons between data



        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //TODO: Add file type error checking to the file selection processs by only allowing users to select .mdb files

            //When button is clicked open window that allows user to select a file
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //From the file dialog, grab the file name and path
                txtPath.Text = fileDialog.FileName;
                string nameFile = fileDialog.SafeFileName;
                string[] names = nameFile.Split('.');
                string newName = names[0];

                //Show a popup with the name of the file
                MessageBox.Show(nameFile);

                //If a file is selected (text is showing), read the database into the program
                if (txtPath.Text != "")
                {
                    readDatabase(newName, txtPath.Text);
                }
            }
        }
        private void readDatabase (string name, string path)
        {
            //Connect to the database and read the database
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path;
            string query = "select * from " + name;
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand(query, connection);
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();

            //Store the read database table in a datatable variable and assign it to the grid view
            dataTable.Load(reader);
            dataGridView.DataSource = dataTable.DefaultView;
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
                        //Console.WriteLine(t.Rows[j][i].ToString());
                        rowItems[j] = currStr;
                    }
                }
                if (isCandidateKey == true)
                {
                    MessageBox.Show("" + t.Columns[i].ColumnName + " is a candidate key");
                    functionalDependencies.Append(t.Columns[i].ColumnName);
                }
            }
        }

        private void btnTemp_Click(object sender, EventArgs e)
        {
            //Call the find candidate key function from the find candidate key button
            //by passing the wide scope dataTable variable in
            findCandidateKey(dataTable);
        }

        private void temp2_Click(object sender, EventArgs e)
        {
            int numCol = dataTable.Columns.Count;
            int numRow = dataTable.Rows.Count;
            DataTable t = dataTable;
            //for each column, go through and compare it to all the other columns
            //if items in the selected column are the same, then the compared columns must also be the same for those items
            //NEED TO ADAPT THIS LOGIC TO WORK FOR FUNCTIONAL DEPENDENCY
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
                        //Console.WriteLine(t.Rows[j][i].ToString());
                        rowItems[j] = currStr;
                    }
                }
                if (isCandidateKey == true)
                {
                    MessageBox.Show("" + t.Columns[i].ColumnName + " is a candidate key");
                    functionalDependencies.Append(t.Columns[i].ColumnName);
                }
            }
        }
    }
}
