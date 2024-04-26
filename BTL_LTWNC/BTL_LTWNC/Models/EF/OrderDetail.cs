using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTWNC.Models.EF
{
    [Table("tblOrderDetail")]
    public class OrderDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int iDetailID { get; set; }

        public int iDetailOrderID { get; set; }

        public int iDetailProductID { get; set; }

        public double fDetailPrice { get; set; }

        public int iDetailQuantity { get; set; }

    }
}
