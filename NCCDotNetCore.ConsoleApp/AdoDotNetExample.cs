using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",//server name
            InitialCatalog = "DotNetTrainingBatch4",//database name
            UserID = "sa",
            Password = "sasa@123"
        };

        public void Read()
        {
            SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);


            string query = "select * from tbl_blog";
            SqlCommand commond = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(commond);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine("Blog Id => " + row["BlogId"]);
                Console.WriteLine("Blog Title => " + row["BlogTitle"]);
                Console.WriteLine("Blog Author => " + row["BlogAuthor"]);
                Console.WriteLine("Blog Conect => " + row["BlogContent"]);
                Console.WriteLine("--------------------------------");
            }
        }
        

    }
}
