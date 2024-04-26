using System.ComponentModel.DataAnnotations;

namespace BTL_LTWNC.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Không được để trống")]
        public string? username { get; set; }


        [Required(ErrorMessage = "Không được để trống")]
        public string? password { get; set; }
    }
}
