using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BlogApp.Pages.Admin
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public RegModel User { get; set; }

        private string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";
        public void OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM BlogsUserAd WHERE userid = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User = new RegModel
                            {
                                userid = reader.GetInt32(0),
                                username = reader.GetString(1),
                                email = reader.GetString(2),
                                password = reader.GetString(3),
                                isActive = reader.GetBoolean(4),
                                Role = reader.GetString(5)
                            };
                        }
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE BlogsUserAd SET username = @username, email = @email, password = @password, isActive = @isActive, Role = @Role WHERE userid = @userid";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", User.username);
                    cmd.Parameters.AddWithValue("@userid", User.userid);
                    cmd.Parameters.AddWithValue("@password", User.password);
                    //cmd.Parameters.AddWithValue("@isActive", User.isActive);
                    if (Request.Form["isActive"].ToString() == "Select" || Request.Form["isActive"].ToString() == "1")
                    {
                        cmd.Parameters.AddWithValue("@isActive",true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isActive", false);
                    }
                    cmd.Parameters.AddWithValue("@email", User.email);
                    cmd.Parameters.AddWithValue("@Role", User.Role);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToPage("/Admin/index");
        }
      
    }
}

