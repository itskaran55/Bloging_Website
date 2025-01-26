
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Pages.Users
{
    public class LoginModel : PageModel
    {
        public RegModel UserData { get; set; } = new RegModel();
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                UserData.username = Request.Form["username"];
                UserData.password = Request.Form["password"];

                string query = "SELECT * FROM BlogsUserAd WHERE username = @username AND password = @password AND isActive = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", UserData.username);
                    cmd.Parameters.AddWithValue("@password", UserData.password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            UserData.Role = reader.GetString(reader.GetOrdinal("Role"));

                         
                            HttpContext.Session.SetString("IsUserLoggedIn", "true");
                            HttpContext.Session.SetString("Username", UserData.username);
                            HttpContext.Session.SetString("UserRole", UserData.Role);

                         
                            if (UserData.Role == "Admin")
                            {
                                return RedirectToPage("/Admin/Index");
                            }
                            else
                            {
                                return RedirectToPage("/Users/Index");
                            }
                        }
                        else
                        {
                            ErrorMessage = "Invalid Username or Password";
                        }
                    }
                }
                conn.Close();
            }
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            return RedirectToPage("/Users/Login");
        }
    }
}
