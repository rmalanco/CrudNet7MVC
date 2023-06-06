using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudNet7MVC.Models
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El celular es requerido")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}

// datos para la tabla Contacto es:
// Id 
// Nombre
// Telefono
// Celular
// Email
// FechaCreacion
//