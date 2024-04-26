using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTWNC.Models.EF
{
    [Table("tblOrder")]
    public class Order
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int iOrderID { get; set; }

        public DateTime dOrderDate { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string? sCustomerName { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string? sCustomerPhone { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string? sCustomerAddress { get; set; }

        public double fTotal { get; set; }

    }
}
