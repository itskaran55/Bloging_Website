//using BlogApp.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.Data.SqlClient;

//namespace BlogApp.Pages.Users
//{
//    public class bEditsModel : PageModel
//    {
//        [BindProperty]
//        public bModel bData { get; set; }

//        private string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";
//        public void OnGet(int id)
//        {
//            using (SqlConnection con = new SqlConnection(connectionString))
//            {
//                string query = "SELECT * FROM BlogsData13 WHERE id = @id";

//                using(SqlCommand cmd = new SqlCommand(query, con))
//                {
//                    cmd.Parameters.AddWithValue("@id", id);
//                    con.Open();
//                    using(SqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            bData = new bModel
//                            {
//                                blogid = reader.GetInt32(0),
//                                title = reader.GetString(1),
//                                blogcontent = reader.GetString(2)
//                            };
//                        }
//                    }
//                }
//            }
//        }
//        public IActionResult OnPost()
//        {
//            using (SqlConnection con = new SqlConnection(connectionString))
//            {
//                string query1 = "UPDATE BlogsData13 SET title = @title, blogcontent = @blogcontent WHERE id = @id";

//                using (SqlCommand cmd= new SqlCommand(query1, con))
//                {
//                    cmd.Parameters.AddWithValue("@id", bData.blogid);
//                    cmd.Parameters.AddWithValue("@title", bData.title);
//                    cmd.Parameters.AddWithValue("@blogcontent", bData.blogcontent);

//                    con.Open();
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            return RedirectToPage("/Users/Blog");
//        }
//    }
//}

using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace BlogApp.Pages.Users
{
    public class bEditsModel : PageModel
    {
        [BindProperty]
        public bModel bData { get; set; }

        private string connectionString = "Server=Karannn-115\\SQLEXPRESS01;Database=BUsersTable;Trusted_Connection=True;Encrypt=False";
        public void OnGet(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM UsersBlogs WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bData = new bModel
                            {
                                blogid = reader.GetInt32(0),
                                title = reader.GetString(1),
                                blogcontent = reader.GetString(2),
                                tags = reader.GetString(6)
                                //isActive = reader.GetBoolean(5)
                            };
                        }
                    }
                }
            }
        }
        public IActionResult OnPost()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE UsersBlogs SET title = @title, blogcontent = @blogcontent, tags = @tags WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", bData.blogid);
                    cmd.Parameters.AddWithValue("@title", bData.title);
                    cmd.Parameters.AddWithValue("@blogcontent", bData.blogcontent);
                    cmd.Parameters.AddWithValue("@tags", bData.tags);
                    //if (Request.Form["isActive"].ToString() == "Select" || Request.Form["isActive"].ToString() == "1")
                    //{
                    //    cmd.Parameters.AddWithValue("@isActive", true);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@isActive", false);
                    //}

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return RedirectToPage("/Users/MyBlogs");
                    }
                    //cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Page();
        }
    }
}
