using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using L01_2020_PM_605.Models;

namespace L01_2020_PM_605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly restauranteContext _restauranteContext;
        public ClientesController(restauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

        /// Metodo para retornar todos los clientes

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List < clientes> listadoClientes = _restauranteContext.clientes.ToList();
            return Ok(listadoClientes);
        }

        /// Metodo crear cliente

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCliente(clientes cliente)
        {

            try
            {
                _restauranteContext.clientes.Add(cliente);
                _restauranteContext.SaveChanges();
                return Ok("El cliente ha sido creado");

            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Error al crear el cliente: {ex.Message}");
            }

        }

        /// Metodo actualizar cliente

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult UpdateCliente(int id, clientes clienteActualizado)
        {


            var cliente = _restauranteContext.clientes.Find(id);
            if (cliente == null)
            {
                return NotFound("cliente no encontrado");
            }

            cliente.nombreCliente = clienteActualizado.nombreCliente;
            cliente.direccion = clienteActualizado.direccion;


            try
            {
                _restauranteContext.SaveChanges();
                return Ok("cliente actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el cliente: {ex.Message}");
            }

        }
        
        /// Metodo actualizar cliente

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteCliente(int id)
        {
            var cliente = _restauranteContext.clientes.Find(id);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado");
            }
            try
            {
                _restauranteContext.clientes.Remove(cliente);
                _restauranteContext.SaveChanges();
                return Ok("El cliente se ha eliminado correctamnete");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al borra el cliente: {ex.Message}");
            }
        }

        // Método para filtrar clientes por palabra en la dirección
        [HttpGet]
        [Route("BuscarPorDireccion/{palabra}")]
        public IActionResult BuscarPorDireccion(string palabra)
        {
            // Filtrar clientes por la palabra en la dirección
            List<clientes> clientesFiltrados = _restauranteContext.clientes
                                                .Where(c => c.direccion.ToLower().Contains(palabra.ToLower()))
                                                .ToList();

            if (clientesFiltrados.Count == 0)
            {
                return NotFound("No se encontraron clientes con la dirección especificada.");
            }

            return Ok(clientesFiltrados);
        }
    }
}
