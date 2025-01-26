using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BlogApp.Pages.Users
{
    public class MyBlogsModel : PageModel
    {
        public List<bModel> Blogs { get; set; } = new List<bModel>();
        public void OnGet()
        {
            if (HttpContext.Session.GetString("IsUserLoggedIn") != "true")
            {
                Response.Redirect("/Users/Login");
                return;
            }            

            string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";
            string username = HttpContext.Session.GetString("Username");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT * FROM UsersBlogs WHERE author = @username and isActive = @isActive";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@isActive", true);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Blogs.Add(new bModel
                            {
                                blogid = reader.GetInt32(0),
                                title = reader.GetString(1),
                                blogcontent = reader.GetString(2),
                                author = reader.GetString(3),
                                createdat = reader.GetDateTime(4),
                                isActive = reader.GetBoolean(5),
                                tags = reader.GetString(6)
                            });
                        }
                    }
                }
                con.Close();
            }
        }
    }
}
