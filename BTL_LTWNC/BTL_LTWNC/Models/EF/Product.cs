using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTWNC.Models.EF
{
    [Table("tblProduct")]
    public class Product
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int iProductID { get; set; }

        public string? sProductName { get; set; }

        public int iProductCategoryID { get; set; }

        public double fPrice { get; set; }

        public string? sImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImagePath { get; set; }

    }
}
