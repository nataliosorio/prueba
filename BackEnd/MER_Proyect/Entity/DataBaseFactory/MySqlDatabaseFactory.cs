using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Entity.DataBaseFactory
{
    public class MySqlDatabaseFactory : IDatabaseFactory
    {
        /// <summary>
        /// Nombre del proveedor MySQL.
        /// </summary>
        public string ProviderName => "MySql";

        /// <summary>
        /// Configura las opciones para usar MySQL como proveedor.
        /// </summary>
        /// <param name="options">Builder de opciones de contexto.</param>
        /// <param name="connectionString">String de conexión a MySQL.</param>
       
        //public void Configure(DbContextOptionsBuilder options, string connectionString)
        //{
        //    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        //}

        public void Configure(DbContextOptionsBuilder options, string connectionString)
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore));
        }

    }
}
