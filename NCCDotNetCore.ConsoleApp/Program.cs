using NCCDotNetCore.ConsoleApp.EFCoreExamples;
using System.Data;
using System.Data.SqlClient;


//Console.WriteLine("Hello, World!");

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


//ado.Net
//CRUD
//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Edit(11);           
//adoDotNetExample.Update(2,"Test1Update", "Author1Update","ContentTestUpdate");
//adoDotNetExample.Delete(12);

//Dapper CRUDE
//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

//EFCore CRUDE
EFCoreExample efCore = new EFCoreExample();
efCore.Run();
Console.ReadKey();
