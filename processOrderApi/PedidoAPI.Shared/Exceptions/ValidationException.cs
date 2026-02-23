using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PedidoAPI.Shared.Exceptions
{
    [Serializable]
    public class ValidationException : BusinessException
    {
        /// <summary>
        /// Lista de errores de validación específicos por campo
        /// </summary>
        public Dictionary<string, string[]> CamposInvalidos { get; private set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ValidationException() : base("Error de validación de datos", "VALIDATION_ERROR")
        {
            CamposInvalidos = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Constructor con mensaje personalizado
        /// </summary>
        public ValidationException(string message) : base(message, "VALIDATION_ERROR")
        {
            CamposInvalidos = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Constructor con mensaje y campos inválidos
        /// </summary>
        public ValidationException(string message, Dictionary<string, string[]> camposInvalidos)
            : base(message, "VALIDATION_ERROR")
        {
            CamposInvalidos = camposInvalidos ?? new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Constructor con mensaje y excepción interna
        /// </summary>
        public ValidationException(string message, Exception innerException)
            : base(message, "VALIDATION_ERROR", innerException)
        {
            CamposInvalidos = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Constructor para deserialización
        /// </summary>
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Restaurar CamposInvalidos si es necesario
        }

        /// <summary>
        /// Agrega un error de validación para un campo específico
        /// </summary>
        public void AgregarError(string campo, string mensaje)
        {
            if (string.IsNullOrWhiteSpace(campo))
                throw new ArgumentException("El nombre del campo es requerido", nameof(campo));

            if (CamposInvalidos.ContainsKey(campo))
            {
                var errores = CamposInvalidos[campo];
                var nuevosErrores = new string[errores.Length + 1];
                errores.CopyTo(nuevosErrores, 0);
                nuevosErrores[errores.Length] = mensaje;
                CamposInvalidos[campo] = nuevosErrores;
            }
            else
            {
                CamposInvalidos[campo] = new[] { mensaje };
            }
        }

        /// <summary>
        /// Verifica si hay errores de validación
        /// </summary>
        public bool TieneErrores()
        {
            return CamposInvalidos.Count > 0 || !string.IsNullOrWhiteSpace(Message);
        }

        /// <summary>
        /// Override para incluir campos inválidos en el string
        /// </summary>
        public override string ToString()
        {
            var baseStr = base.ToString();

            if (CamposInvalidos.Count == 0)
                return baseStr;

            var camposStr = string.Join("; ",
                CamposInvalidos.Select(kvp => $"{kvp.Key}: [{string.Join(", ", kvp.Value)}]"));

            return $"{baseStr} - Campos: {camposStr}";
        }
    }
}
