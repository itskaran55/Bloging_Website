using System.ComponentModel.DataAnnotations;
using System;
namespace BlogApp.Models
{
    public class bModel
    {
        [Key]
        public int blogid { get; set; }
        public string title { get; set; } = string.Empty;
        public string blogcontent { get; set; } =  string.Empty;
        public string author { get; set; }
        public DateTime createdat { get; set; }
        public bool isActive { get; set; }

        public string tags { get; set; } = string.Empty;

    }
}
