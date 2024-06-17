using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCDotNetCore.ConsoleApp.AdoDotNetExamples
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
        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);

            string query = $@"SELECT [BlogId]
                            ,[BlogTitle]
                            ,[BlogAuthor]
                            ,[BlogContent]
                            FROM [dbo].[Tbl_Blog] Where BlogId = {id}";

            SqlCommand commond = new SqlCommand(query, connection);
            commond.Parameters.AddWithValue(parameterName: "@BlogId", id);//for sql injection
            SqlDataAdapter adapter = new SqlDataAdapter(commond);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found!");
                return;
            }
            DataRow row = dt.Rows[0];

            Console.WriteLine("Blog Id => " + row["BlogId"]);
            Console.WriteLine("Blog Title => " + row["BlogTitle"]);
            Console.WriteLine("Blog Author => " + row["BlogAuthor"]);
            Console.WriteLine("Blog Conect => " + row["BlogContent"]);
            Console.WriteLine("--------------------------------");
        }
        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor       
           ,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }
        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] =@BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }
        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }

    }
}
