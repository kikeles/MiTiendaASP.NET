using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Ventas.Persona
{
    public class CrearViewModel
    {
        [Required]
        public string tipo_persona { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "El nombre de la persona debe tener 3 caracteres y menos de 100 caracteres.")]
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
