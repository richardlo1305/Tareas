using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelos;
using PruebasUnitarias.FakeServices;
using System;
using System.IO;
using WebAPI.Controllers;

namespace PruebasUnitarias
{
    [TestClass]
    public class PruebasUnitariasUsuario
    {
        private ServiciosUsuariosFake serviciosUsuarios;
        private IConfigurationRoot configuration;

        public PruebasUnitariasUsuario()
        {
            serviciosUsuarios = new ServiciosUsuariosFake();
            configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebAPI"))
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [TestMethod]
        public void CrearUsuarioOk()
        {
            var controlador = new UsuarioController(serviciosUsuarios, configuration);
            var usuario = new CrearUsuario
            {
                 Correo = "rodolfo@unillanos.edu.co",
                 Identificacion = "100456785",
                 Nombre = "Rodolfo",
                 Password = "123456",
                 User = "rodolfo"
            };
            var response = controlador.Crear(usuario).Result;
            Assert.IsInstanceOfType(response, typeof(ObtenerUsuario));
        }

        [TestMethod]
        public void EliminarUsuarioOk()
        {
            var controlador = new UsuarioController(serviciosUsuarios, configuration);
            var response = controlador.Eliminar(1);
            Assert.IsNotInstanceOfType(response, typeof(Exception));
        }

        [TestMethod]
        public void ActualizarUsuarioOk()
        {
            var controlador = new UsuarioController(serviciosUsuarios, configuration);
            var usuarioActualizar = new ActualizarUsuario
            {
                Codigo = 1,
                Correo = "rodolfo@unillanos.edu.co",
                Identificacion = "100456785",
                Nombre = "Rodolfo",
                Password = "123456",
                User = "rodolfo"
            };

            var response = controlador.Actualizar(usuarioActualizar).Result;
            Assert.IsNotInstanceOfType(response, typeof(Exception));
        }

        [TestMethod]
        public void LoginUsuarioOk()
        {
            var controlador = new UsuarioController(serviciosUsuarios, configuration);
            var usuarioLogin = new LoginUsuario
            {
                User = "jorge",
                Password = "123456"
            };
            var response = controlador.Login(usuarioLogin);
            Assert.IsNotInstanceOfType(response, typeof(Exception));
        }
    }
}
