using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BlogApp.Pages.Admin
{
    public class bDataDeleteModel : PageModel
    {
        private string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";
        public IActionResult OnGet(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE UsersBlogs SET isActive = @isActive WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@isActive", false);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return RedirectToPage("/Admin/blogsData");
        }
    }
}
