using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

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
            string connstring = "Data Source = BENSQLTRAININGM;Initial Catalog=BENASXDATABASE;Integrated Security=true";
            SqlConnection con = new SqlConnection(connstring);
            con.Open();
            string query = "SELECT TOP 5 * FROM ShareTransactions";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read()) 
            {
                string output = "Ouput = " + reader.GetValue(0) + "+" + reader.GetValue(1) + "-" + reader.GetValue(2) + "-" + reader.GetValue(3) + "-" + reader.GetValue(4) + "-" + reader.GetValue(5);
                MessageBox.Show(output);
            }

        }
    }
}