using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection.DAL.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
