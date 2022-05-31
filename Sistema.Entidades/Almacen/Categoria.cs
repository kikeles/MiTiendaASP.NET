using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Sistema.Entidades.Almacen
{
    public class Categoria
    {
        public int idcategoria { get; set; }
        [Required]//el campo nombre es requerido
        [StringLength(50,MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [StringLength(256)]//el tamaño maximo de la descripcion de de 256
        public string descripcion { get; set; }
        public bool condicion { get; set; }

        //propiedad que obtiene todos los articulos a los que pertenecen 
        //a dicha categoria
        public ICollection<Articulo> articulos { get; set; }
    }
}
