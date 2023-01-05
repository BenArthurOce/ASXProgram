using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Globalization;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;


namespace ASXProgram
{
    public partial class MyInputForm : Form
    {
        public MyInputForm()
        {
            InitializeComponent();
        }

        private void MyInputForm_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";
            string sqlQuery = "SELECT * FROM ShareTransactions WHERE ASXCode = @Value";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Value", "CBA");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }
        //
        //  Tab 1
        //
        private void btnInsert_Click(object sender, EventArgs e)
        {
        }

        private void btnNewButton_Click(object sender, EventArgs e)
        {

            string queryString = "SELECT TOP 5 * FROM ShareTransactions";
            string connectionString = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT TOP 5 * FROM ShareTransactions", connection))
                {
                    // command.Parameters.AddWithValue("@Country", "USA");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine("{0}, {1}", reader["Name"], reader["Email"]);
                            Console.WriteLine("{0}, {1}", reader["ContractNote"], reader["Date"]);

                            System.Diagnostics.Debug.WriteLine("{0}, {1}", reader["ASXCode"], reader["Date"]);

                        }
                    }
                }
            }
        }
        //
        //  Tab 2
        //
        private void btn_tab2_Submit_Click(object sender, EventArgs e)
        {
            SubmitData();
        }

        private void SubmitData()
        {

            string selectDateAsString = dtPick_TransDate.Value.ToString("yyyyMMdd");


            string TypeId;
            int IsIncrease;
            int IsDecrease;

            switch (tBox_tab2_Type.Text)
            {
                case "Buy":
                    TypeId = "FE3657F9-B05F-4EE6-AB9B-1D4AE345A39C";
                    IsIncrease = 1;
                    IsDecrease = 0;
                    break;

                case "Sell":
                    TypeId = "F2B0E405-E3B5-45A4-AFCE-8CCFC50BBE8B";
                    IsIncrease = 0;
                    IsDecrease = 1;
                    break;

                case "DRP":
                    TypeId = "539CFBED-8D06-4C05-AC9A-CD711BB263F9";
                    IsIncrease = 1;
                    IsDecrease = 0;
                    break;

                case "Other":
                    TypeId = "638F2970-0F25-4D99-BA11-E367FE649CA3";
                    IsIncrease = 1;
                    IsDecrease = 0;
                    break;

                case "Dummy":
                    TypeId = "B87B2537-B15A-4C46-86C5-5172BF438763";
                    IsIncrease = 1;
                    IsDecrease = 0;
                    break;

                default:
                    TypeId = "FE3657F9-B05F-4EE6-AB9B-1D4AE345A39C";
                    IsIncrease = 1;
                    IsDecrease = 0;
                    break;
            }

            string connectionString = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";
            string insertSql = "INSERT INTO ShareTransactions (ContractNote, ASXCode, Date, TypeId, Quantity, UnitPrice, TradeValue, Brokerage, TotalValue, IsIncrease, IsDecrease) " +
                "VALUES (@ContractNote, @ASXCode, @Date, @TypeId, @Quantity, @UnitPrice, @TradeValue, @Brokerage, @TotalValue, @IsIncrease, @IsDecrease)";

            SqlQuery query = new SqlQuery(connectionString, insertSql);
            query.AddParameter("@ContractNote", int.Parse(tBox_tab2_ContractNote.Text));
            query.AddParameter("@ASXCode", tBox_tab2_ASXCode.Text);
            query.AddParameter("@Date", int.Parse(selectDateAsString));
            query.AddParameter("@TypeId", TypeId);
            query.AddParameter("@Quantity", int.Parse(tBox_tab2_Quantity.Text));
            query.AddParameter("@UnitPrice", float.Parse(tBox_tab2_UnitPrice.Text));
            query.AddParameter("@TradeValue", float.Parse(tBox_tab2_TradeValue.Text));
            query.AddParameter("@Brokerage", float.Parse(tBox_tab2_Brokerage.Text));
            query.AddParameter("@TotalValue", float.Parse(tBox_tab2_TotalValue.Text));
            query.AddParameter("@IsIncrease", IsIncrease);
            query.AddParameter("@IsDecrease", IsDecrease);

            using (SqlDataReader reader = query.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Read columns from the current row of the result set
                }
            }
        }


        private void btn_tab2_Clear_Click(object sender, EventArgs e)
        {
            //ClearTextBoxes();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_tab2_DeleteRecord_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        private void DeleteRecord()
        {
            string connectionString = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";
            string sqlQuery = "DELETE FROM ShareTransactions WHERE Id = @RecordId";

            SqlQuery query = new SqlQuery(connectionString, sqlQuery);
            query.AddParameter("@RecordId", tBox_tab2_RecordId.Text);

            using (SqlDataReader reader = query.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Read columns from the current row of the result set
                }
            }
        }


        private void tBox_tab2_Quantity_TextChanged(object sender, EventArgs e)
        {
            if (tBox_tab2_Quantity.Text.EndsWith(".")) { return; } else { CheckFormBlanksAndApplyMath(); }
        }

        private void tBox_tab2_UnitPrice_TextChanged(object sender, EventArgs e)
        {
            if (tBox_tab2_UnitPrice.Text.EndsWith(".")) { return; } else { CheckFormBlanksAndApplyMath(); }
        }

        private void tBox_tab2_TradeValue_TextChanged(object sender, EventArgs e)
        {
            if (tBox_tab2_TradeValue.Text.EndsWith(".")) { return; } else { CheckFormBlanksAndApplyMath(); }
        }

        private void tBox_tab2_Brokerage_TextChanged(object sender, EventArgs e)
        {
            if (tBox_tab2_Brokerage.Text.EndsWith(".")) { return; } else { CheckFormBlanksAndApplyMath(); }
        }

        private void CheckFormBlanksAndApplyMath()
        {
            float TradeValue = 0;
            float TotalValue = 0;

            // If "UnitPrice or Quantity are blank or null, this code is to be skipped
            if (string.IsNullOrWhiteSpace(tBox_tab2_Quantity.Text) == true || string.IsNullOrWhiteSpace(tBox_tab2_UnitPrice.Text) == true)
            {
                tBox_tab2_TradeValue.Text = null;
                tBox_tab2_TotalValue.Text = null;
                return;     // Leave the method
            }
            else
            {   // Calculate the Trade value and apply it to textbox
                TradeValue = float.Parse(tBox_tab2_Quantity.Text) * float.Parse(tBox_tab2_UnitPrice.Text);
                tBox_tab2_TradeValue.Text = TradeValue.ToString();
            }

            // If Brokerage is empty
            if (string.IsNullOrWhiteSpace(tBox_tab2_Brokerage.Text) == true)
            {
                // Then calculate Total Value without brokerage
                TotalValue = float.Parse(tBox_tab2_TradeValue.Text);
                tBox_tab2_TotalValue.Text = TotalValue.ToString();
            }

            // If Brokerage is not empty
            else
            {
                // Then calculate Total Value with brokerage
                TotalValue = float.Parse(tBox_tab2_TradeValue.Text) + float.Parse(tBox_tab2_Brokerage.Text);
                tBox_tab2_TotalValue.Text = TotalValue.ToString();
            }
        }


        private void tBox_tab2_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tBox_tab2_UnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isNumber(e.KeyChar, tBox_tab2_UnitPrice.Text))
                e.Handled = true;
        }

        private void tBox_tab2_TradeValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isNumber(e.KeyChar, tBox_tab2_TradeValue.Text))
                e.Handled = true;
        }

        private void tBox_tab2_Brokerage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isNumber(e.KeyChar, tBox_tab2_Brokerage.Text))
                e.Handled = true;
        }


        public bool isNumber(char ch, string text)
        {
            bool res = true;
            char decimalChar = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            //check if it´s a decimal separator and if doesn´t already have one in the text string
            if (ch == decimalChar && text.IndexOf(decimalChar) != -1)
            {
                res = false;
                return res;
            }

            //check if it´s a digit, decimal separator and backspace
            if (!Char.IsDigit(ch) && ch != decimalChar && ch != (char)Keys.Back)
                res = false;

            return res;
        }
        //
        //  Tab 3
        //
        private void btn_tab3_generate_Click(object sender, EventArgs e)
        {

            string connectionString = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";
            string sqlQuery =
            "SELECT ShareTransactions.Id, ContractNote, TransactionTypes.Type, ASXCode, Date, Quantity, TotalValue FROM ShareTransactions INNER JOIN TransactionTypes ON TransactionTypes.Id = ShareTransactions.TypeId ORDER BY Date";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // command.Parameters.AddWithValue("@Value", "CBA");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView_tab3.DataSource = dataTable;
                    }
                }
            }
        }



        private void dataGridView_tab3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = e.RowIndex;
            textBox1.Text = Convert.ToString(dataGridView_tab3[0, row].Value);
            //textBox1.Text = Convert.ToString(dataGridView_tab3[1, row].Value);
        }

        private void btn_tab3_Delete_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            // Set the column and row styles to have equal width and height
            for (int i = 0; i < dataGridView_tab3.ColumnCount; i++)
            {
                dataGridView_tab3.Columns[i].Width = dataGridView_tab3.Width / dataGridView_tab3.ColumnCount;
            }
            for (int i = 0; i < dataGridView_tab3.RowCount; i++)
            {
                dataGridView_tab3.Rows[i].Height = dataGridView_tab3.Height / dataGridView_tab3.RowCount;
            }
            */
            // Set the value of each cell to the corresponding day of the year
            for (int i = 0; i < dataGridView_tab3.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView_tab3.RowCount; j++)
                {
                    dataGridView_tab3[i, j].Value = (i * 31 + j + 1).ToString();
                }
            }



            string connectionString = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";
            string sqlQuery = "SELECT [Prices].PriceClose FROM [ASXSharePrices] [Prices] WHERE ([Prices].ASXDate BETWEEN @StartDate AND @EndDate) AND [Prices].ASXCode = @ASXCode ORDER BY [Prices].ASXDate";

            SqlQuery query = new SqlQuery(connectionString, sqlQuery);
            query.AddParameter("@StartDate", 20210101);
            query.AddParameter("@EndDate", 20211231);
            query.AddParameter("@ASXCode", "CBA");


            using (SqlDataReader reader = query.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Read columns from the current row of the result set

                }
            }
        }


        //
        //  Tab 4
        //
        private void btn_tab4_Generate_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";
            string filePath = tBox_tab4_FileLocation.Text;

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\vboxuser\Desktop\ASXHistoricalPrices",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tBox_tab4_FileLocation.Text = openFileDialog1.FileName;
            }

            string fileName = tBox_tab4_FileLocation.Text.ToString();
            string fullPath = tBox_tab4_FileLocation.Text.ToString();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {   new DataColumn("ASXCode", typeof(string)),
                new DataColumn("ASXDate",typeof(int)),
                new DataColumn("PriceOpen",typeof(decimal)),
                new DataColumn("PriceHigh",typeof(decimal)),
                new DataColumn("PriceLow",typeof(decimal)),
                new DataColumn("PriceClose",typeof(decimal)),
                new DataColumn("VolumeTraded",typeof(int)),
            });

            foreach (string row in File.ReadAllLines(fullPath))
            {
                //if ((!string.IsNullOrEmpty(row)) || (!row.Contains("Date")))
                //MessageBox.Show(row.ToString());

                if (string.IsNullOrEmpty(row) || row.Contains("Date")) { continue; }

                else
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }

            dataGridView_tab4.DataSource = dt;


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "[dbo].[ASXSharePricesTemp2]";
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
        }


        private void btn_tab4_FindV2_Click(object sender, EventArgs e)
        {
            AddTextFilesToDataTable();
        }


        private void btn_tab4_Import_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView_tab4_Notepads.Rows.Count; i++)
            {
                // do something with each row
                DataGridViewRow row = dataGridView_tab4_Notepads.Rows[i];

                // access cells in the row using the Cells property
                string cellValue = row.Cells[0].Value.ToString();

                //MessageBox.Show(cellValue);
                ImportASingleTextFile(cellValue);

                
            }

            AddEntryToDocumentTable();
        }






        private void AddTextFilesToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5]
            {   new DataColumn("FilePath", typeof(string)),
                new DataColumn("FileName", typeof(string)),
                new DataColumn("DateUploaded",typeof(DateTime)),
                new DataColumn("FileSize",typeof(int)),
                new DataColumn("RowCount",typeof(int)),
            });

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\vboxuser\Desktop\ASXHistoricalPrices",
                Title = "Browse Text Files",
                Multiselect = true,

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };


            int rowCount = 0;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                // get the list of selected file names
                string[] fileNames = openFileDialog1.FileNames;

                // iterate over the file names
                foreach (string fileName in fileNames)
                {
                    rowCount += 1;
                    dt.Rows.Add();
                    int i = 0;

                    FileInfo fileInfo = new FileInfo(fileName);
                    string fileInfo_Path = fileInfo.FullName;
                    string fileInfo_Name = fileInfo.Name;
                    DateTime currentDateTime = DateTime.Now;
                    string fileInfo_Size = fileInfo.Length.ToString();


                    // Number of Rows in File
                    string[] lines = File.ReadAllLines(fileName);
                    int fileInfo_RowCount = lines.Length;

                    // Add All File properties to a list for a row
                    List<string> ListOfFileInformation = new List<string>();
                    ListOfFileInformation.Add(fileInfo_Path);
                    ListOfFileInformation.Add(fileInfo_Name);
                    ListOfFileInformation.Add(currentDateTime.ToString());
                    ListOfFileInformation.Add(fileInfo_Size);
                    ListOfFileInformation.Add(fileInfo_RowCount.ToString());

                    foreach (string cell in ListOfFileInformation)
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }

                // Put all the file data into the datatable
                dataGridView_tab4_Notepads.DataSource = dt;
            }
        }

        private void ImportASingleTextFile(string filePath)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {   new DataColumn("ASXCode", typeof(string)),
                new DataColumn("ASXDate",typeof(int)),
                new DataColumn("PriceOpen",typeof(decimal)),
                new DataColumn("PriceHigh",typeof(decimal)),
                new DataColumn("PriceLow",typeof(decimal)),
                new DataColumn("PriceClose",typeof(decimal)),
                new DataColumn("VolumeTraded",typeof(int)),
            });

            foreach (string row in File.ReadAllLines(filePath))
            {
                //if ((!string.IsNullOrEmpty(row)) || (!row.Contains("Date")))
                //MessageBox.Show(row.ToString());

                if (string.IsNullOrEmpty(row) || row.Contains("Date")) { continue; }

                else
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }


            string connectionString = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "[dbo].[ASXSharePricesTemp2]";
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
        }


        private void AddEntryToDocumentTable()
        {

            DataTable dt = new DataTable();
            dt = (DataTable)dataGridView_tab4_Notepads.DataSource;

            string connectionString = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "[dbo].[DocumentUploadHistory]";
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
        }





    }
}