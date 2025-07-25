using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Aud;
using Microsoft.EntityFrameworkCore;

namespace Entity.Context.Aud
{
    public class AuditDbContext : DbContext
    {

        public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options)
        {

        }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
