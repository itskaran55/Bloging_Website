using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public List<RegModel> Users { get; set; } = new List<RegModel>();

        public IActionResult OnGet()
        {
            // Check if user is logged in
            if (HttpContext.Session.GetString("IsUserLoggedIn") != "true")
            {
                return RedirectToPage("/Users/Login");
            }
            string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM BlogsUserAd WHERE isActive = @isActive";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@isActive", true);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RegModel user = new RegModel();
                            user.userid = reader.GetInt32(0);
                            user.username = reader.GetString(1);
                            user.email = reader.GetString(2);
                            user.password = reader.GetString(3);
                            user.isActive = reader.GetBoolean(4);
                            user.Role = reader.GetString(5); // Fetch role
                            Users.Add(user);
                        }
                    }
                    conn.Close();
                }
            }
            return Page();
        }
    }
}
