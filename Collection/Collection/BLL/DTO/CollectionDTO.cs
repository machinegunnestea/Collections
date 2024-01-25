using Collection.DAL.Enums;
using Collection.Data;

namespace Collection.BLL.DTO
{
    public class CollectionDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Topic { get; set; }
        public List<IFormFile> Files { get; set; }
        public string FirstFieldName { get; set; }
        public FieldType FirstFieldType { get; set; }
        public string SecondFieldName { get; set; }
        public FieldType SecondFieldType { get; set; }
        public string ThirdFieldName { get; set; }
        public FieldType ThirdFieldType { get; set; }
        public ApplicationUser User { get; set; }

        public IEnumerable<string> Topics { get; set; }
    }
}