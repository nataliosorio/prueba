using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Entity.Context.Aud;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entity.DataBaseFactory.Aud
{
    //fábrica que se encarga de crear instancias de AuditDbContext, que es el contexto de Entity Framework Core para la base de datos de auditoría.
    public class AuditDbContextFactory
    {
        //servicio que sabe cómo configurar el DbContextOptionsBuilder para distintos proveedores de base de datos.
        private readonly DatabaseFactoryProvider _provider;

        //Lee la configuración del archivo appsettings.json para saber qué proveedor y cadena de conexión usar.
        private readonly IConfiguration _configuration;

        public AuditDbContextFactory(DatabaseFactoryProvider provider, IConfiguration configuration)
        {
            _provider = provider;
            _configuration = configuration;
        }

        //Método que crea y devuelve una instancia lista para usar de AuditDbContext.
        public AuditDbContext Create()
        {
            //Lee la clave AuditDatabaseProvider o, si no está, usa DatabaseProvider para saber qué proveedor usar.
            var providerName = _configuration["AuditDatabaseProvider"] ?? _configuration["DatabaseProvider"];

            //Obtiene la cadena de conexión para ese proveedor desde la configuración.
            var connectionString = _configuration.GetConnectionString(providerName);

            //Construye las opciones necesarias para crear el contexto EF Core, como la cadena de conexión y el proveedor de base de datos.

            var optionsBuilder = new DbContextOptionsBuilder<AuditDbContext>();

            //Configura el optionsBuilder usando la fábrica específica del proveedor, que define detalles.
            var factory = _provider.GetFactory(providerName);
            factory.Configure(optionsBuilder, connectionString);

            //Finalmente devuelve una instancia de AuditDbContext con todas las opciones configuradas para conectarse a la base de datos de auditoría.

            return new AuditDbContext(optionsBuilder.Options);
        }
    }
}
