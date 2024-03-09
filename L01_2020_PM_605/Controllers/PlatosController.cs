using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using L01_2020_PM_605.Models;

namespace L01_2020_PM_605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : Controller
    {
        private readonly restauranteContext _restauranteContext;

        public PlatosController(restauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }
        ///////////////////////CRUD////////////////////////////////

        /// Metodo para retornar todos los pedidos

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<platos> listadoPlatos = _restauranteContext.platos.ToList();
            return Ok(listadoPlatos);
        }

        /// Metodo crear plato

        [HttpPost]
        [Route("Create")]
        public IActionResult CreatePlato(platos plato)
        {

            try
            {
                _restauranteContext.platos.Add(plato);
                _restauranteContext.SaveChanges();
                return Ok("El plato ha sido creado");

            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Error al crear el plato: {ex.Message}");
            }

        }

        /// Metodo actualizar plato

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult UpdatePlatos(int id, platos platoActualizado)
        {


            var plato = _restauranteContext.platos.Find(id);
            if (plato == null)
            {
                return NotFound("Plato no encontrado");
            }

            plato.nombrePlato = platoActualizado.nombrePlato;
            plato.precio = platoActualizado.precio;

            try
            {
                _restauranteContext.SaveChanges();
                return Ok("plato actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el plato: {ex.Message}");
            }

        }

        /// Metodo borrar plato

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeletePlato(int id)
        {
            var plato = _restauranteContext.platos.Find(id);
            if (plato == null)
            {
                return NotFound("Plato no encontrado");
            }
            try
            {
                _restauranteContext.platos.Remove(plato);
                _restauranteContext.SaveChanges();
                return Ok("El plato se ha eliminado correctamnete");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al borra el plato: {ex.Message}");
            }
        }

        /// Metodo filtrar plato

        [HttpGet]
        [Route("BuscarPorNombre/{nombre}")]
        public IActionResult BuscarPorNombre(string nombre)
        {
            // Filtrar platos por el nombre proporcionado
            List<platos> platosFiltrados = _restauranteContext.platos
                                                .Where(p => p.nombrePlato.ToLower().Contains(nombre.ToLower()))
                                                .ToList();

            if (platosFiltrados.Count == 0)
            {
                return NotFound("No se encontraron platos con el nombre especificado.");
            }

            return Ok(platosFiltrados);
        }
    }
}
