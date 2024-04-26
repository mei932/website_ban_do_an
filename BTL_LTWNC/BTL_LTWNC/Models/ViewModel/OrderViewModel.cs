using System.ComponentModel.DataAnnotations;

namespace BTL_LTWNC.Models.ViewModel
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string? sCustomerName { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string? sCustomerPhone { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string? sCustomerAddress { get; set; }
    }
}
