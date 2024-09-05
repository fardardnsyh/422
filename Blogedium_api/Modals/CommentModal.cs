using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogedium_api.Modals
{
    public class CommentModal
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your comment")]
        public string CommentContent { get; set; } = string.Empty;
        public int BlogId { get; set; }
        public virtual BlogModal? Blog { get; set; } 
    }
}
