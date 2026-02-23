using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoAPI.Shared.DTOs
{
    public class PedidoItemDto
    {
        /// <summary>
        /// Identificador único del producto
        /// </summary>
        [Required(ErrorMessage = "El ProductoId es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El ProductoId debe ser mayor a cero")]
        public int ProductoId { get; set; }

        /// <summary>
        /// Cantidad de unidades del producto
        /// Debe ser mayor a cero
        /// </summary>
        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, 10000, ErrorMessage = "La cantidad debe estar entre 1 y 10000")]
        public int Cantidad { get; set; }

        /// <summary>
        /// Precio unitario del producto
        /// No puede ser negativo
        /// </summary>
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        /// <summary>
        /// Nombre del producto (opcional, para referencia)
        /// </summary>
        [StringLength(200)]
        public string NombreProducto { get; set; }

        /// <summary>
        /// Calcula el subtotal de este item
        /// </summary>
        public decimal Subtotal => Cantidad * Precio;

        /// <summary>
        /// Valida que el item tenga datos consistentes
        /// </summary>
        public bool EsValido()
        {
            return ProductoId > 0 && Cantidad > 0 && Precio >= 0;
        }

        /// <summary>
        /// Override para debugging
        /// </summary>
        public override string ToString()
        {
            return $"ProductoId: {ProductoId}, Cantidad: {Cantidad}, Precio: {Precio:C2}, Subtotal: {Subtotal:C2}";
        }
    }
}
