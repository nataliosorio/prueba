using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Entity.Context.Aud
{
    public class AuditDbContextFactory : IDesignTimeDbContextFactory<AuditDbContext>
    {
        public AuditDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuditDbContext>();

            // Asegúrate que la cadena coincida con tu appsettings.json (ajusta si usas otra BD)
            optionsBuilder.UseSqlServer("Server=LAPTOP-DDPC2U6E;Database=auditoria;Trusted_Connection=True;TrustServerCertificate=true");

            return new AuditDbContext(optionsBuilder.Options);
        }
    }
}
