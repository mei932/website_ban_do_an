using System.ComponentModel.DataAnnotations;

namespace BTL_LTWNC.Models.ViewModel
{
    public class OrderDetailViewModel
    {
        [Key]
        public int iDetailID { get; set; }

        public int iDetailOrderID { get; set; }

        public int iDetailProductID { get; set; }

        public string? sProductName { get; set; }

        public double? fDetailPrice { get; set; }

        public int iDetailQuantity { get; set; }
    }
}
