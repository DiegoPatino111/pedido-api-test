using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidoAPI.Shared.DTOs
{
    public class PedidoRequestDto
    {
        /// <summary>
        /// Identificador único del cliente
        /// Debe ser validado contra servicio externo
        /// </summary>
        [Required(ErrorMessage = "El ClienteId es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El ClienteId debe ser mayor a cero")]
        public int ClienteId { get; set; }

        /// <summary>
        /// Usuario que realiza el pedido
        /// Para auditoría y trazabilidad
        /// </summary>
        [Required(ErrorMessage = "El usuario es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 100 caracteres")]
        public string Usuario { get; set; }

        /// <summary>
        /// Lista de items/productos del pedido
        /// Debe contener al menos un item
        /// </summary>
        [Required(ErrorMessage = "La lista de items es requerida")]
        [MinLength(1, ErrorMessage = "El pedido debe contener al menos un item")]
        public List<PedidoItemDto> Items { get; set; }

        /// <summary>
        /// Notas adicionales del pedido (opcional)
        /// </summary>
        [StringLength(500, ErrorMessage = "Las notas no pueden exceder 500 caracteres")]
        public string Notas { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public PedidoRequestDto()
        {
            Items = new List<PedidoItemDto>();
        }

        /// <summary>
        /// Valida que todos los items tengan datos consistentes
        /// </summary>
        public bool TieneItemsValidos()
        {
            if (Items == null || Items.Count == 0)
                return false;

            foreach (var item in Items)
            {
                if (item == null || !item.EsValido())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Calcula el total del pedido
        /// </summary>
        public decimal CalcularTotal()
        {
            if (Items == null)
                return 0m;

            return Items.Sum(i => i.Cantidad * i.Precio);
        }
    }
}
