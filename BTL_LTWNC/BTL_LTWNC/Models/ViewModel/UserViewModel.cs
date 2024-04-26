using System.ComponentModel.DataAnnotations;

namespace BTL_LTWNC.Models.ViewModel
{
    public class UserViewModel
    {
        [Key]
        public int iUserID { get; set; }

        public string? sUserName { get; set; }

        public string? sUserPassword { get; set; }

        public string? sRoleName { get; set; }

    }
}
