using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlogApp.Models;
using Microsoft.Data.SqlClient;

namespace BlogApp.Pages.Users
{
    public class CreateModel : PageModel
    {
        public bModel BlogData { get; set; } = new bModel();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsUserLoggedIn") != "true")
            {
                return RedirectToPage("/Users/Login");
            }
            return Page();
        }
        public IActionResult OnPost()
        {

            string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";

            BlogData.title = Request.Form["Title"];
            BlogData.blogcontent = Request.Form["Content"];
            BlogData.author = HttpContext.Session.GetString("Username");
            BlogData.createdat = DateTime.Now;
            BlogData.isActive = true;
            BlogData.tags = Request.Form["tag"];

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "INSERT INTO UsersBlogs (title, blogcontent, author, createdat ,isActive, tags) VALUES (@title, @blogcontent, @author, @createdat, @isActive, @tags)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@title", BlogData.title);
                    cmd.Parameters.AddWithValue("@blogcontent", BlogData.blogcontent);
                    cmd.Parameters.AddWithValue("@author", BlogData.author);
                    cmd.Parameters.AddWithValue("@createdat", BlogData.createdat);
                    cmd.Parameters.AddWithValue("@isActive", BlogData.isActive);
                    cmd.Parameters.AddWithValue("@tags", BlogData.tags);

                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }

            return RedirectToPage("/Users/Blog");
        }

    }
}
