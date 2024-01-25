using Collection.DAL.Enums;
using Collection.Data;
using System.ComponentModel.DataAnnotations;

namespace Collection.ViewModels
{
    public class CollectionViewModel
    {
        public int? Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string ShortDescription { get; set; }
        public string Theme { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
        public string FirstFieldName { get; set; }
        public FieldType FirstFieldType { get; set; }
        public string SecondFieldName { get; set; }
        public FieldType SecondFieldType { get; set; }
        public string ThirdFieldName { get; set; }
        public FieldType ThirdFieldType { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<string> Themes { get; set; }
    }
}