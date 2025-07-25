using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Aud;

namespace Utilities.Aud.Strategy
{
    //Su función es guardar un registro de auditoría en un archivo de texto.
    public class FileAuditStrategy : IAuditStrategy
    {
        //	Ruta del archivo donde se guardarán los registros de auditoría
        private readonly string _filePath;

        //Recibe la ruta del archivo para usarla en la operación de guardado
        public FileAuditStrategy(string filePath)
        {
            _filePath = filePath;
        }

        //Método asíncrono que escribe una línea con el registro de auditoría en el archivo de texto
        public async Task AuditAsync(AuditLog entry)
        {
            //Se construye una cadena de texto (logLine) con los detalles importantes del registro (fecha, usuario, acción, entidad, id y cambios).
            var logLine = $"{entry.Timestamp:u} | {entry.UserName} | {entry.Action} | {entry.EntityName} | ID: {entry.EntityId} | {entry.Changes}";
            //Se añade esta línea al final del archivo indicado por _filePath usando File.AppendAllTextAsync.
            await File.AppendAllTextAsync(_filePath, logLine + "\n");
        }
    }
}
