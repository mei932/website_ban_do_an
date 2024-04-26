using System.ComponentModel.DataAnnotations;

namespace BTL_LTWNC.Models.ViewModel
{
    public class ProductViewModel
    {
        [Key]
        public int iProductID { get; set; }

        public string? sProductName { get; set; }

        public string? sCategoryName { get; set; }

        public double fPrice { get; set; }

        public string sImageUrl { get; set; }
    }
}
