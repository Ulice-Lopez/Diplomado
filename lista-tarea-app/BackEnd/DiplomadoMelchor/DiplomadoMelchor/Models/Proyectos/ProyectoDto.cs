using System.ComponentModel.DataAnnotations;
using Tareas.Entidades;

namespace Tareas.Dtos
{
    public class ProyectoSetDto
    {

        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;
        public bool EstaActivo { get; set; } = true;
    }

    public class ProyectoGetDto
    {


        [Required]
        public int IdProyecto { get; set; }

        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        public bool EstaActivo { get; set; }
    }

}