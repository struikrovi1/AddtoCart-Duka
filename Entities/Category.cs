using System.ComponentModel;

namespace Entities
{
    public class Category: BaseEntity
    {

        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }

        public bool IsFeatured { get; set; }

        public string SanitizedName { get; set; }

        public int DisplaySeqNo { get; set; }

        public int? PictureID { get; set; } 

        public virtual Picture Picture { get; set; }

        public virtual List<Product> Products { get; set; }

        public virtual List<CategoryRecord> CategoryRecords { get; set; }
    }

    public class CategoryRecord : BaseEntity
    {

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int LanguageID { get; set; }

        [DisplayName("Category name")]
        public string Name { get; set; }
        public string? Summary { get; set; }

        public string? Description { get; set; }

    }
}


