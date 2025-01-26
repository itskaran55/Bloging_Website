using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using BlogApp.Models;

namespace BlogApp.Pages.Users
{
    public class RegistrationModel : PageModel
    {
        public RegModel UserData { get; set; } = new RegModel();
        public string ErrorMessage { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";

            // Extract form data
            UserData.username = Request.Form["username"];
            UserData.email = Request.Form["email"];
            UserData.password = Request.Form["password"];
            string confirmPassword = Request.Form["cpassword"];
            UserData.isActive = true;

            // Password match check
            if (UserData.password != confirmPassword)
            {
                ErrorMessage = "Password and Confirm Password do not match.";
                return Page();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Extract form data
                UserData.username = Request.Form["username"];
                UserData.email = Request.Form["email"];
                UserData.password = Request.Form["password"];
                UserData.isActive = true;

                
                string query = $"INSERT INTO BlogsUserAd (username, email, password, isActive, Role) VALUES ('{UserData.username}', '{UserData.email}', '{UserData.password}', '{(UserData.isActive ? 1 : 0)}', @Role)";

                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Role", "User"); 
                    cmd.ExecuteNonQuery();
                }

                
                HttpContext.Session.SetString("IsUserLoggedIn", "true");
                HttpContext.Session.SetString("Username", UserData.username);
                HttpContext.Session.SetString("UserRole", "User"); 

                conn.Close();
            }

            return RedirectToPage("/Users/Index");
        }
    }
}


