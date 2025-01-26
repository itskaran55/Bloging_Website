using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegModel
    {
        [Key]
        public int userid { get; set; }
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public bool isActive { get; set; }

        public string Role { get; set; }
       
    }
}
