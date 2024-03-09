using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using L01_2020_PM_605.Models;

namespace L01_2020_PM_605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : Controller
    {
        private readonly restauranteContext _restauranteContext;

        public pedidosController(restauranteContext restauranteContext)
        { 
            _restauranteContext = restauranteContext;
        }

        /// Metodo para retornar todos los pedidos
        
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<pedidos> listadoPedidos = _restauranteContext.pedidos.ToList();
            return Ok(listadoPedidos);
        }

        /// Metodo crear pedido

        [HttpPost]
        [Route("Create")]
        public IActionResult CreatePedido(pedidos pedido)
        {
            
            try 
            {
                _restauranteContext.pedidos.Add(pedido);
                _restauranteContext.SaveChanges();
                return Ok("El pedido ha sido creado");

            }catch (Exception ex) 
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    $"Error al crear el pedido: {ex.Message}");
            }

        }

        /// Metodo actualizar pedido

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult UpdatePedido(int id, pedidos pedidoActualizado)
        {
            

            var pedido = _restauranteContext.pedidos.Find(id);
            if (pedido == null) 
            {
                return NotFound("Pedido no encontrado");
            }

            pedido.motoristaId = pedidoActualizado.motoristaId;
            pedido.clienteId = pedidoActualizado.clienteId;
            pedido.platoId = pedidoActualizado.platoId;
            pedido.cantidad = pedidoActualizado.cantidad;
            pedido.precio = pedidoActualizado.precio;


            try
            {
                _restauranteContext.SaveChanges();
                return Ok("pedido actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el pedido: {ex.Message}");
            }

        }

        /// Metodo actualizar pedido

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeletePedido(int id)
        {
            var pedido = _restauranteContext.pedidos.Find(id);
            if (pedido == null)
            {
                return NotFound("Pedido no encontrado");
            }
            try
            {
                _restauranteContext.pedidos.Remove(pedido);
                _restauranteContext.SaveChanges();
                return Ok("El pedido se ha eliminado correctamnete");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al borra el pedido: {ex.Message}");
            }
        }

        /// Filtrar por clienteid

        [HttpGet]
        [Route("GetByClienteId/{idC}")]
        public IActionResult GetByCliente(int idC)
        {
            var pedido = _restauranteContext.pedidos
                .Where(e => e.clienteId == idC)
                .ToList();

            if (pedido == null) 
            { 
                return NotFound("Pedido por cliente no encontrado"); 
            }
            return Ok(pedido);
        }

        /// Filtrar por motoristaid

        [HttpGet]
        [Route("GetByMotoristaId/{idM}")]
        public IActionResult GetByMotoristaId(int idM)
        {
            var pedido = _restauranteContext.pedidos
                .Where(e => e.motoristaId == idM)
                .ToList();

            if (pedido == null) { return NotFound("Pedido por motorista no encontrado"); }
            return Ok(pedido);
        }

        

    }
}
