using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PedidoAPI.Shared.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        /// <summary>
        /// Código de error único para identificación y manejo
        /// </summary>
        public string ErrorCode { get; private set; }

        /// <summary>
        /// Parámetros adicionales para contexto del error
        /// </summary>
        public object[] Parameters { get; private set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public BusinessException() : base("Ocurrió un error de negocio")
        {
            ErrorCode = "BUSINESS_ERROR";
        }

        /// <summary>
        /// Constructor con mensaje personalizado
        /// </summary>
        public BusinessException(string message) : base(message)
        {
            ErrorCode = "BUSINESS_ERROR";
        }

        /// <summary>
        /// Constructor con mensaje y código de error
        /// </summary>
        public BusinessException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode ?? "BUSINESS_ERROR";
        }

        /// <summary>
        /// Constructor con mensaje, código de error y excepción interna
        /// </summary>
        public BusinessException(string message, string errorCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode ?? "BUSINESS_ERROR";
        }

        /// <summary>
        /// Constructor con mensaje, código de error y parámetros
        /// </summary>
        public BusinessException(string message, string errorCode, params object[] parameters)
            : base(message)
        {
            ErrorCode = errorCode ?? "BUSINESS_ERROR";
            Parameters = parameters;
        }

        /// <summary>
        /// Constructor para deserialización (requerido para Serializable)
        /// </summary>
        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = info.GetString(nameof(ErrorCode)) ?? "BUSINESS_ERROR";
        }

        /// <summary>
        /// Override para incluir ErrorCode en la serialización
        /// </summary>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(ErrorCode), ErrorCode);
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Override para incluir detalles del error de negocio
        /// </summary>
        public override string ToString()
        {
            return $"[{ErrorCode}] BusinessException: {Message}";
        }
    }
}
