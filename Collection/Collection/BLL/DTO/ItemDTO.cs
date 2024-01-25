using Collection.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Collection.BLL.DTO
{
    public class ItemDTO
    {
        public int? Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public int likes { get; set; } = 0;
        public int? FirstInteger { get; set; }
        public int? SecondInteger { get; set; }
        public int? ThirdInteger { get; set; }
        public string FirstString { get; set; }
        public string SecondString { get; set; }
        public string ThirdString { get; set; }
        public string FirstText { get; set; }
        public string SecondText { get; set; }
        public string ThirdText { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FirstDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SecondDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ThirdDate { get; set; }

        public bool? FirstBoolean { get; set; }
        public bool? SecondBoolean { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public bool? ThirdBoolean { get; set; }
        public string TagsJson { get; set; }
        public CollectionModel Collection { get; set; }
        public int? CollectionId { get; set; }
        public bool Liked { get; set; } = false;
    }
}