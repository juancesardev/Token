using System.ComponentModel.DataAnnotations;

namespace JwTokens.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // de int a guid
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool Enabled { get; set; } = false;
    }
}
