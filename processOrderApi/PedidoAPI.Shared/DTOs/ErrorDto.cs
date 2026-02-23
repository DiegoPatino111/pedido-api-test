using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoAPI.Shared.DTOs
{
    public class ErrorDto
    {
        /// <summary>
        /// Código de error único para identificación técnica
        /// Ejemplo: VALIDATION_ERROR, CUSTOMER_NOT_FOUND
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Mensaje descriptivo del error (amigable para el usuario)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Detalles adicionales del error (solo en desarrollo/debug)
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// URL con documentación del error (opcional)
        /// </summary>
        public string Instance { get; set; }

        /// <summary>
        /// Timestamp del error en UTC
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ID de request para tracing y soporte
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Path del endpoint donde ocurrió el error
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ErrorDto()
        {
            Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Crea un error de validación
        /// </summary>
        public static ErrorDto Validacion(string mensaje, string errorCode = "VALIDATION_ERROR")
        {
            return new ErrorDto
            {
                ErrorCode = errorCode,
                Message = mensaje,
                Timestamp = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Crea un error de negocio
        /// </summary>
        public static ErrorDto Negocio(string mensaje, string errorCode = "BUSINESS_ERROR")
        {
            return new ErrorDto
            {
                ErrorCode = errorCode,
                Message = mensaje,
                Timestamp = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Crea un error interno (genérico para producción)
        /// </summary>
        public static ErrorDto Interno(string requestId = null)
        {
            return new ErrorDto
            {
                ErrorCode = "INTERNAL_ERROR",
                Message = "Ocurrió un error interno. Contacte al soporte técnico.",
                RequestId = requestId,
                Timestamp = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Override para logging
        /// </summary>
        public override string ToString()
        {
            return $"[{ErrorCode}] {Message} - {Timestamp:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
