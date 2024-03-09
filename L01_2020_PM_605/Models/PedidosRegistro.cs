using System.ComponentModel.DataAnnotations;
namespace L01_2020_PM_605.Models
{
    public class PedidosRegistro
    {
        [Key]
        public int motoristaId { get; set; }
        public int clienteId { get; set; }
        public int platoId { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }

    }
}
