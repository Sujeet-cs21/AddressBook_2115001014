using System.ComponentModel.DataAnnotations;

namespace RepositoryLayer.Entity
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
