using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Blogedium_api.Modals;

namespace Blogedium_api.Modals
{
    public class UserModal
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password should atleast contain 8 characters")]
        public string Password { get; set; }

        [EnumDataType(typeof(UserRole))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; } = UserRole.User; // Default role set to User
        // Empty constructor required by EF Core for migrations and queries
        public UserModal() {}
    }
}
