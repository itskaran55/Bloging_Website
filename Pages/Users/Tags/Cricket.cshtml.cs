using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BlogApp.Pages.Users.Tags
{
    public class CricketModel : PageModel
    {
        public List<bModel> Blogs { get; set; } = new List<bModel>();

        public void OnGet()
        {
            string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM UsersBlogs WHERE isActive = @isActive and tags = @tags";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@isActive", true);
                    cmd.Parameters.AddWithValue("@tags", "Cricket");

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

                conn.Close();
            }
        }
    }
}
