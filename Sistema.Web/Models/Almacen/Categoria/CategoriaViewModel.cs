using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Almacen.Categoria
{
    public class CategoriaViewModel
    {
        //esta entidad sirve para restringir la entrada a los servicios web
        //estas se validan en el servidor web y no se espera llegar hasta la base de datos
        //para validarlo
        public int idcategoria { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
    }
}
