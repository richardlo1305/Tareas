using Modelos;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/tareas")]
    [ApiController]
    [Authorize]
    public class TareaController: ControllerBase
    {
        private readonly ILogicaTareas _logicaTareas;

        public TareaController(ILogicaTareas logicaTareas)
        {
            _logicaTareas = logicaTareas;
        }

        [HttpGet]
        [Route("consultar")]
        public async Task<ListaTareas> Consultar([FromQuery] FiltrosTareas filtros)
        {
            return await _logicaTareas.ObtenerPorFiltro(filtros);
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ObtenerTarea> Crear([FromBody] CrearTarea crearTarea)
        {
            return await _logicaTareas.CrearTarea(crearTarea);
        }

        [HttpPost]
        [Route("actualizar")]
        public async Task<ObtenerTarea> Actualizar([FromBody] ActualizarTarea ActualzarTarea)
        {
            return await _logicaTareas.ActualizarTarea(ActualzarTarea);
        }

        [HttpPost]
        [Route("borrar")]
        public async Task Borrar([FromQuery] int codigoTarea)
        {
            await _logicaTareas.EliminarTarea(codigoTarea);
        }

        [HttpGet]
        [Route("obtenertodastareas")]
        public async Task<ListaTareas> ObtenerTodasTareas()
        {
            return await _logicaTareas.ObtenerTodasTareas();
        }
    }
}
