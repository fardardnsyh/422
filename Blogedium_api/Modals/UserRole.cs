using System.ComponentModel.DataAnnotations;

namespace Blogedium_api.Modals
{
    public enum UserRole
    {
        [Display(Name = "User")]
        User,
        [Display(Name = "Admin")]
        Admin
    }
}