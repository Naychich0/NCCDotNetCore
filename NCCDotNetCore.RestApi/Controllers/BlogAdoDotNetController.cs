using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCCDotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;


namespace NCCDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

            string query = "select * from tbl_blog";

            SqlCommand commond = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(commond);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            //List<BlogModel> lst = new List<BlogModel>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    BlogModel blog = new BlogModel();
            //    blog.BlogId = Convert.ToInt32(dr["BlogId"]);
            //    blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            //    blog.BlogAuthor= Convert.ToString(dr["BlogAuthor"]);
            //    blog.BlogContent = Convert.ToString(dr["BlogContent"]);
            //}

            //BlogModel blog = new BlogModel
            //{
            //   BlogId = Convert.ToInt32(dr["BlogId"]),
            //    BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //    BlogContent = Convert.ToString(dr["BlogContent"])
            //};
            //    lst.Add(blog);

            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();

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
        }
}

