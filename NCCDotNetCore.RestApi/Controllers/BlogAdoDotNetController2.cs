using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCCDotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;
using NCCDotNet.Shared;


namespace NCCDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController2 : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

            string query = "select * from tbl_blog";

            SqlCommand commond = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(commond);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if(dt.Rows.Count == 0)
            {
                return NotFound("No data found");
            }
            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor       
           ,@BlogContent)";

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,BlogModel blog) 
        {
            string selectQuery = "select * from tbl_blog";

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] =@BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(selectQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", blog.BlogId);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            var item = dt.Rows.Count;
            if (item == 0)
            {
                return NotFound("No data found");
            }

            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd2.Parameters.AddWithValue("@BlogId", blog.BlogId);
            cmd2.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd2.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd2.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd2.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            string selectQuery = "select * from tbl_blog";

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(selectQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", blog.BlogId);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            var item = dt.Rows.Count;
            if (item== 0)
            {
                return Ok("No data found to patch.");
            }

            string conditions = string.Empty;

            if (blog.BlogTitle != null)
            {
                conditions += "[BlogTitle] = @BlogTitle, ";
            }

            if (blog.BlogAuthor != null)
            {
                conditions += "[BlogAuthor] = @BlogAuthor, ";
            }

            if (blog.BlogContent != null)
            {
                conditions += "[BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                           SET {conditions}
                         WHERE BlogId=@BlogId";

            SqlCommand cmd2 = new SqlCommand(query, connection);

            cmd2.Parameters.AddWithValue("@BlogId", id);
            cmd2.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd2.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd2.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = cmd2.ExecuteNonQuery();
            
            connection.Close();

            string message = result > 0 ? "Patching successful." : "Patching failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE BlogId=@BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

           
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

    }
}

