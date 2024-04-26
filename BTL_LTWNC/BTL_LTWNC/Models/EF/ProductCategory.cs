using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTWNC.Models.EF
{
    [Table("tblProductCategory")]
    public class ProductCategory
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int iCategoryID { get; set; }

        public string? sCategoryName { get; set; }

    }
}
