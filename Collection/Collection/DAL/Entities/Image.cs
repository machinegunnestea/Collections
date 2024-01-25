using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection.DAL.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public string PublicId { get; set; }
        public int CollectionId { get; set; }
        public CollectionModel Collection { get; set; }
    }
}
