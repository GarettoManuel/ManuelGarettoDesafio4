using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManuelGarettoDesafio4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet("/producto/{descripciones}")]
        public Models.Producto ObtenerProductoPorIdUsuario(string descripciones)
        {
            Models.Producto producto = ManejadorProducto.obtenerProductoDescripcion(descripciones);
            return producto;
        }

        [HttpPut("/usuario/{id}")]
        public string ModificarUsuario(Models.Usuario usuario, long id)
        {
            return ManejadorUsuario.ModificarUsuario(usuario, id) == 1 ? "Modificado" : "No se pudo modificar";
        }


        [HttpPost]
        public Models.Producto CrearProducto(Models.Producto producto)
        {
            ManejadorProducto.CrearProducto(producto);
            return producto;
        }

        [HttpPut("{id}")]
        public string ModificarProducto(Models.Producto producto, long id)
        {
            return ManejadorProducto.ModificarProducto(producto, id) == 1 ? "Modificado" : "No se pudo modificar";
        }



        [HttpDelete("{id}")]
        public string BorrarProducto(long id)
        {
            return ManejadorProducto.BorrarProducto(id) == 1 ? "Eliminado" : "No se puede eliminar";
        }


    }
}
