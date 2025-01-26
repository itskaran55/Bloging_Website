using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BlogApp.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";

        public IActionResult OnGet(int userid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE BlogsUserAd SET isActive = @isActive WHERE userid = @userid";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@isActive", false);
                    command.Parameters.AddWithValue("@userid", userid);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToPage("/Admin/index");
        }
    }
}
