using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Exceptions
{
    /// <summary>
    /// Excepción base para todos los errores relacionados con la capa de presentación (Controller).
    /// </summary>
    public class ControllerException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ControllerException"/> con un mensaje de error.
        /// </summary>
        /// <param name="message">El mensaje que describe el error.</param>
        public ControllerException(string message) : base(message)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ControllerException"/> con un mensaje de error y una excepción interna.
        /// </summary>
        /// <param name="message">El mensaje que describe el error.</param>
        /// <param name="innerException">La excepción que es la causa del error actual.</param>
        public ControllerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Excepción lanzada cuando se recibe una solicitud inválida desde el cliente.
    /// </summary>
    public class BadRequestControllerException : ControllerException
    {
        public BadRequestControllerException(string message) : base(message)
        {
        }

        public BadRequestControllerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Excepción lanzada cuando se produce un error interno inesperado en el controlador.
    /// </summary>
    public class InternalServerErrorControllerException : ControllerException
    {
        public InternalServerErrorControllerException(string message) : base(message)
        {
        }

        public InternalServerErrorControllerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Excepción lanzada cuando no se encuentra un recurso solicitado desde el controlador.
    /// </summary>
    public class NotFoundControllerException : ControllerException
    {
        public NotFoundControllerException(string message) : base(message)
        {
        }

        public NotFoundControllerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Excepción lanzada cuando el usuario no tiene permisos para acceder a un recurso desde el controlador.
    /// </summary>
    public class UnauthorizedControllerException : ControllerException
    {
        public UnauthorizedControllerException(string message) : base(message)
        {
        }

        public UnauthorizedControllerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
