using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PedidoAPI.Shared.Exceptions
{
    [Serializable]
    public class ExternalServiceException : Exception
    {
        /// <summary>
        /// Nombre del servicio externo que falló
        /// </summary>
        public string ServicioNombre { get; private set; }

        /// <summary>
        /// URL del endpoint que falló
        /// </summary>
        public string EndpointUrl { get; private set; }

        /// <summary>
        /// Código de estado HTTP (si aplica)
        /// </summary>
        public HttpStatusCode? HttpStatusCode { get; private set; }

        /// <summary>
        /// Intentos de retry realizados antes de fallar
        /// </summary>
        public int IntentosRealizados { get; private set; }

        /// <summary>
        /// Indica si el error es recuperable (retry posible)
        /// </summary>
        public bool EsRecuperable { get; private set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ExternalServiceException() : base("Error al consumir servicio externo")
        {
            ServicioNombre = "Desconocido";
            EsRecuperable = true;
        }

        /// <summary>
        /// Constructor con mensaje personalizado
        /// </summary>
        public ExternalServiceException(string message) : base(message)
        {
            ServicioNombre = "Desconocido";
            EsRecuperable = true;
        }

        /// <summary>
        /// Constructor con mensaje y excepción interna
        /// </summary>
        public ExternalServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
            ServicioNombre = "Desconocido";
            EsRecuperable = true;
        }

        /// <summary>
        /// Constructor completo con todos los detalles
        /// </summary>
        public ExternalServiceException(
            string message,
            string servicioNombre,
            HttpStatusCode? httpStatusCode = null,
            int intentosRealizados = 1,
            bool esRecuperable = true)
            : base(message)
        {
            ServicioNombre = servicioNombre ?? "Desconocido";
            HttpStatusCode = httpStatusCode;
            IntentosRealizados = intentosRealizados;
            EsRecuperable = esRecuperable;
        }

        /// <summary>
        /// Constructor con mensaje, código HTTP y excepción interna
        /// </summary>
        public ExternalServiceException(
            string message,
            HttpStatusCode httpStatusCode,
            Exception innerException)
            : base(message, innerException)
        {
            ServicioNombre = "Desconocido";
            HttpStatusCode = httpStatusCode;
            EsRecuperable = httpStatusCode >= HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Constructor para deserialización
        /// </summary>
        protected ExternalServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ServicioNombre = info.GetString(nameof(ServicioNombre)) ?? "Desconocido";
            EndpointUrl = info.GetString(nameof(EndpointUrl));
            IntentosRealizados = info.GetInt32(nameof(IntentosRealizados));
            EsRecuperable = info.GetBoolean(nameof(EsRecuperable));
        }

        /// <summary>
        /// Override para incluir detalles del servicio externo
        /// </summary>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(ServicioNombre), ServicioNombre);
            info.AddValue(nameof(EndpointUrl), EndpointUrl);
            info.AddValue(nameof(IntentosRealizados), IntentosRealizados);
            info.AddValue(nameof(EsRecuperable), EsRecuperable);

            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Override para incluir detalles completos del error
        /// </summary>
        public override string ToString()
        {
            var statusStr = HttpStatusCode.HasValue
                ? $" HTTP {(int)HttpStatusCode.Value} {HttpStatusCode.Value}"
                : string.Empty;

            return $"[{ServicioNombre}] ExternalServiceException:{statusStr} - {Message} (Intentos: {IntentosRealizados})";
        }
    }
}
