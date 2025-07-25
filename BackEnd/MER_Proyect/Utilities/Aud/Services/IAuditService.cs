using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Aud;

namespace Utilities.Aud.Services
{
    //Define un servicio que se encarga de guardar un registro de auditoría de forma asíncrona.
    public interface IAuditService
    {
        //Guarda un objeto AuditLog (registro de auditoría) en algún lugar, puede ser base de datos, archivo, etc.
        Task SaveAuditAsync(AuditLog entry);
    }
}
