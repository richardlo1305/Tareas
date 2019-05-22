using EntityFramework.ClasesEntidad;
using EntityFramework.OperacionesBD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Modelos;
using AutoMapper;

namespace Logica
{
    public class LogicaTareas : ILogicaTareas
    {
        private readonly IOperacionesTareas _operaciones;
        private readonly ILogicaUsuarios _logicaUsuarios;
        private readonly IMapper _mapper;

        public LogicaTareas(IOperacionesTareas operaciones,
            IMapper mapper, ILogicaUsuarios logicaUsuarios)
        {
            _operaciones = operaciones;
            _logicaUsuarios = logicaUsuarios;
            _mapper = mapper;
        }
        /// <summary>
        /// Función para obtener todas las tareas
        /// </summary>
        /// <returns>Lista de tareas</returns>
        public async Task<ListaTareas> ObtenerTodasTareas()
        {
            try
            {
                var listaTareas = new ListaTareas();

                var obtenerTareas = await _operaciones.ObtenerTodo();
                List<ObtenerTarea> listaObtenerTareas = new List<ObtenerTarea>();
                foreach(var tarea in obtenerTareas)
                {
                    listaObtenerTareas.Add(_mapper.Map<ObtenerTarea>(tarea));
                }
                listaTareas.Lista = listaObtenerTareas;
                return listaTareas;
            }
            catch(Exception)
            {
                throw;
            } 
        }

        /// <summary>
        /// Función para crear una tarea
        /// </summary>
        /// <param name="tarea">Tarea a crear</param>
        /// <returns>Retorna la tarea creada</returns>
        public async Task<ObtenerTarea> CrearTarea(CrearTarea crearTarea)
        {
            try
            {
                var tarea = _mapper.Map<Tarea>(crearTarea);
                var tareaCreada =  await _operaciones.CrearTarea(tarea);
                return _mapper.Map<ObtenerTarea>(tareaCreada);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Función para eliminar una tarea
        /// </summary>
        /// <param name="codigoTarea">Código de la tarea a eliminar</param>
        public async Task EliminarTarea(int codigoTarea)
        {
            try
            {
                var tareaBd = await _operaciones.ObtenerPorId(codigoTarea);
                if(tareaBd != null)
                {
                    await _operaciones.EliminarTarea(tareaBd);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Función para actualizar tarea
        /// </summary>
        /// <param name="actualizarTarea">Tarea a actualizar</param>
        /// <returns>Retorna la tarea actualizada</returns>
        public async Task<ObtenerTarea> ActualizarTarea(ActualizarTarea actualizarTarea)
        {
            try
            {
                var tareaBd = await _operaciones.ObtenerPorId(actualizarTarea.Codigo);
                if(tareaBd != null)
                {
                    var tarea = _mapper.Map<Tarea>(actualizarTarea);
                    var tareaActualizada = await _operaciones.ActualizarTarea(tarea);
                    return _mapper.Map<ObtenerTarea>(tareaActualizada);
                }
                return new ObtenerTarea();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Función para buscar tareas con filtro
        /// </summary>
        /// <param name="filtros">Clase que contiene filtros</param>
        /// <returns>Lista de tareas</returns>
        public async Task<ListaTareas> ObtenerPorFiltro(FiltrosTareas filtros)
        {
            try
            {
                ListaTareas listaTareas = new ListaTareas();
               
                if (filtros.FiltroTodasTareas)
                {
                    var obtenerTareas =  await _operaciones.ObtenerTodo();
                    
                    if(!filtros.FiltroTareasPendientes && !filtros.FiltroTareasFinalizadas)
                    {
                        List<ObtenerTarea> listaInicialTareas = new List<ObtenerTarea>();
                        foreach (var tarea in obtenerTareas)
                        {
                            listaInicialTareas.Add(_mapper.Map<ObtenerTarea>(tarea));
                        }
                        listaTareas.Lista = listaInicialTareas;
                    }
                    
                    if (filtros.FiltroTareasPendientes)
                    {
                        var listaPendientes = obtenerTareas.Where(t => !t.Finalizada).ToList();

                        List<ObtenerTarea> listaObtenerTareas = new List<ObtenerTarea>();
                        foreach (var tarea in listaPendientes)
                        {
                            listaObtenerTareas.Add(_mapper.Map<ObtenerTarea>(tarea));
                        }

                        listaTareas.Lista.AddRange(listaObtenerTareas);
                    }

                    if (filtros.FiltroTareasFinalizadas)
                    {
                        var listaFinalizadas = obtenerTareas.Where(t => t.Finalizada).ToList();

                        List<ObtenerTarea> listaObtenerTareas = new List<ObtenerTarea>();
                        foreach (var tarea in listaFinalizadas)
                        {
                            listaObtenerTareas.Add(_mapper.Map<ObtenerTarea>(tarea));
                        }

                        listaTareas.Lista.AddRange(listaObtenerTareas);
                    }

                    if (filtros.OrdenarPorFechaVencimiento)
                    {
                        listaTareas.Lista = listaTareas.Lista.OrderBy(t => t.FechaVencimiento).ToList();
                    }

                    return listaTareas;
                }

                if (filtros.FiltroMisTareas)
                {
                    var usuarioLogueado = await _logicaUsuarios.ObtenerUsuarioLogueado();
                    if(!filtros.FiltroTareasPendientes && !filtros.FiltroTareasFinalizadas)
                    {
                        var obtenerLista = (await _operaciones.EncontrarTareas(t => t.UsuarioId == usuarioLogueado.Codigo)).ToList();
                        List<ObtenerTarea> listaInicialTareas = new List<ObtenerTarea>();
                        foreach (var tarea in obtenerLista)
                        {
                            listaInicialTareas.Add(_mapper.Map<ObtenerTarea>(tarea));
                        }
                        listaTareas.Lista = listaInicialTareas;
                    }
                    
                    if (filtros.FiltroTareasPendientes)
                    {
                        var obtenerTareas = (await _operaciones.EncontrarTareas(t => !t.Finalizada && t.UsuarioId == usuarioLogueado.Codigo)).ToList();

                        List<ObtenerTarea> listaObtenerTareas = new List<ObtenerTarea>();
                        foreach (var tarea in obtenerTareas)
                        {
                            listaObtenerTareas.Add(_mapper.Map<ObtenerTarea>(tarea));
                        }

                        listaTareas.Lista.AddRange(listaObtenerTareas);
                    }

                    if (filtros.FiltroTareasFinalizadas)
                    {
                        var obtenerTareas = (await _operaciones.EncontrarTareas(t => t.Finalizada && t.UsuarioId == usuarioLogueado.Codigo)).ToList();

                        List<ObtenerTarea> listaObtenerTareas = new List<ObtenerTarea>();
                        foreach (var tarea in obtenerTareas)
                        {
                            listaObtenerTareas.Add(_mapper.Map<ObtenerTarea>(tarea));
                        }
                        listaTareas.Lista.AddRange(listaObtenerTareas);
                    }

                    if (filtros.OrdenarPorFechaVencimiento)
                    {
                        listaTareas.Lista = listaTareas.Lista.OrderBy(t => t.FechaVencimiento).ToList();
                    }

                    return listaTareas;
                }

                return listaTareas;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
