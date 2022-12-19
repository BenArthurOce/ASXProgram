using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;

namespace ASXProgram
{
    public partial class MyInputForm : Form
    {
        public MyInputForm()
        {
            InitializeComponent();
        }

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

        private void btn_tab2_Submit_Click(object sender, EventArgs e)
        {
            SubmitData();
        }

        private void SubmitData()
        {

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
            query.AddParameter("@Date", int.Parse(tBox_tab2_Date.Text));
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
    }
}