using System.ComponentModel.DataAnnotations;

namespace SCEE.Shared.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}