using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace dominio
{
    public class Articulo
    {
        public Articulo() 
        {
            Marca = new Marca();
            Categoria = new Categoria();

        }
        public string Codigo { get; set; }
        public string Nombre { get; set; }  
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }

        public float Precio { get; set; }
        public string UrlImagen { get; set; }
    }
}
