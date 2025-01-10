using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    
    public class ArticulosNegocio
    {
        public AccesoDatos conexion = new AccesoDatos();
        
        public List<Articulo> listar()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            try
            {
                conexion.setearConsulta("SELECT A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.ImagenUrl, A.Precio FROM ARTICULOS A, MARCAS M , CATEGORIAS C WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id;");

                SqlDataReader lector = conexion.lectura();
                
                while (lector.Read()) 
                {
                    Articulo articuloAux = new Articulo();
                    articuloAux.Codigo = (string)lector["Codigo"];
                    articuloAux.Nombre = (string)lector["Nombre"];
                    articuloAux.Descripcion = (string)lector["Descripcion"];
                    articuloAux.Marca.Descripcion = (string)lector["Marca"];
                    articuloAux.Categoria.Descripcion = (string)lector["Categoria"];
                    articuloAux.UrlImagen = (string)lector["ImagenUrl"];
                    articuloAux.Precio = (decimal)lector["Precio"];

                    listaArticulos.Add(articuloAux);
                    
                }

                return listaArticulos;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void agregar(Articulo articulo) 
        {
            try
            {
                conexion.setearConsulta("INSERT INTO ARTICULOS VALUES(@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Url, @Precio)");
                conexion.setearParametro("@Codigo", articulo.Codigo);
                conexion.setearParametro("@Nombre", articulo.Nombre);
                conexion.setearParametro("@Descripcion", articulo.Descripcion);
                conexion.setearParametro("@IdMarca", articulo.Marca.Id);
                conexion.setearParametro("@IdCategoria", articulo.Categoria.Id);
                conexion.setearParametro("@Url", articulo.UrlImagen);
                conexion.setearParametro("@Precio", articulo.Precio);
                conexion.ejecutarLectura();

            }
            catch(Exception ex) 
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();

            }

        }
    }
}
