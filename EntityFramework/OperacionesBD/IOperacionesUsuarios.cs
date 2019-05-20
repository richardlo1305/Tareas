﻿using EntityFramework.ClasesEntidad;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFramework.OperacionesBD
{
    public interface IOperacionesUsuarios
    {
        Task CrearUsuario(Usuario crearUsuario);
        Task EliminarUsuario(Usuario eliminarUsuario);
        Task ActualizarUsuario(Usuario actualizarUsuario);
        Task<IQueryable<Usuario>> ObtenerTodo();
        Task<IQueryable<Usuario>> EncontrarUsuario(Expression<Func<Usuario, bool>> expresion);
        Task<Usuario> ObtenerPorId(int codigoTarea);
    }
}