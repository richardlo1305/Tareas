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
            var response = controlador.Crear(usuario);
            Assert.IsInstanceOfType(response, typeof(ObtenerUsuario));
        }

        [TestMethod]
        public void EliminarUsuarioOk()
        {
            var controlador = new UsuarioController(serviciosUsuarios, configuration);
            var response = controlador.Eliminar(1);
            Assert.IsInstanceOfType(response, typeof(Exception));
        }
    }
}
