using NCCDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;


Console.WriteLine("Hello, World!");

//npm
//pub.dev
//nuget
//SqlClient



//Ctrl+.
//F10
//F11

//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
//stringBuilder.UserID = "sa";
//stringBuilder.Password = "sasa@123";

//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);


//string query = "select * from tbl_blog";
//SqlCommand commond= new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter(commond);
//DataTable dt = new DataTable();
//adapter.Fill(dt);
//connection.Close();

//dataset => datatable
//datatable => datarow
//datarow => datacolumn

//foreach(DataRow row in dt.Rows)
//{
 //   Console.WriteLine("Blog Id => "+ row["BlogId"]);
   // Console.WriteLine("Blog Title => " + row["BlogTitle"]);
   //Console.WriteLine("Blog Author => " + row["BlogAuthor"]);
   //Console.WriteLine("Blog Conect => " + row["BlogContent"]);
   //Console.WriteLine("--------------------------------");
//}
//ado.Net Read


AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.Read();
Console.ReadKey();
