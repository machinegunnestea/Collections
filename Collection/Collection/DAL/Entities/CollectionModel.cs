using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collection.Data;

namespace Collection.DAL.Entities
{
    public class CollectionModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Theme { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [StringLength(50)]
        public string FirstFieldName { get; set; }

        [StringLength(50)]
        public string SecondFieldName { get; set; }

        [StringLength(50)]
        public string ThirdFieldName { get; set; }

        public FieldType FirstFieldType { get; set; }
        public FieldType SecondFieldType { get; set; }
        public FieldType ThirdFieldType { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}