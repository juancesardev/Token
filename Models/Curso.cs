using System.ComponentModel.DataAnnotations;

namespace JwTokens.Models
{
    public enum enumNivel { Básico, Intermedio, Avanzado };
    public class Curso
    {
        [Key]
        public int Id { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Status { get; set; } = false;
        [Required, StringLength(100)]
        [Display(Name = "Curso")]
        public string CursoName { get; set; } = string.Empty;
        [Required, StringLength(280)]
        [Display(Name = "Descripción Corta")]
        public string ShortDescription { get; set; } = String.Empty;
        public enumNivel Nivel { get; set; }
    }
}
