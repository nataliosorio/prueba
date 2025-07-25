using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context.Aud;
using Entity.Model.Aud;

namespace Utilities.Aud.Strategy
{
    //implementación concreta de la interfaz IAuditStrategy. guarda la información de auditoría en la base de datos 
    public class DatabaseAuditStrategy : IAuditStrategy
    {
        private readonly AuditDbContext _context;

        public DatabaseAuditStrategy(AuditDbContext context)
        {
            _context = context;
        }

        //Método asíncrono que agrega el registro de auditoría a la tabla y guarda los cambios en la base
        public async Task AuditAsync(AuditLog entry)
        {
            _context.AuditLogs.Add(entry);
            await _context.SaveChangesAsync();
        }
    }
}
