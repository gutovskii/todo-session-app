using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TodoSession.DataAccess.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsChecked { get; set; } = false;
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
