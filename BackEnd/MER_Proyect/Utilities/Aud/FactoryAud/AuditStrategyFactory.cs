using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Aud.Strategy;
using Entity.DataBaseFactory.Aud;


namespace Utilities.Aud.FactoryAud
{
    //Es responsable de crear y devolver las diferentes estrategias de auditoría que se usarán en la aplicación.
    public class AuditStrategyFactory : IAuditStrategyFactory
    {
        private readonly AuditDbContextFactory _auditDbFactory;

        public AuditStrategyFactory(AuditDbContextFactory auditDbFactory)
        {
            _auditDbFactory = auditDbFactory;
        }

        //Crea las instancias de estrategias y las devuelve como un arreglo para ser usadas juntas
        public IAuditStrategy[] GetStrategies()
        {
            var dbStrategy = new DatabaseAuditStrategy(_auditDbFactory.Create());
            var fileStrategy = new FileAuditStrategy("Logs/audit.txt");

            return new IAuditStrategy[] { dbStrategy, fileStrategy };
        }
    }
}
