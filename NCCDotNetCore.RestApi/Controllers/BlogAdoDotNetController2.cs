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

            string query = "select * from tbl_blog";

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@BlogId", id);
            //var lst = _adoDotNetService.Query<BlogModel>(query, parameters);

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));


            //SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            //SqlCommand commond = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(commond);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            //connection.Close();

            if (item is null)
            {
                return NotFound("No data found");
            }
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

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );
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
            int result = _adoDotNetService.Execute(query,
            new AdoDotNetParameter("@BlogId", id),
            new AdoDotNetParameter("@BlogTitle", blog.BlogTitle!),
            new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor!),
            new AdoDotNetParameter("@BlogContent", blog.BlogContent!)
        );

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

            int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

    }
}

