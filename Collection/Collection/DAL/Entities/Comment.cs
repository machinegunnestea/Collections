using Collection.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection.DAL.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}