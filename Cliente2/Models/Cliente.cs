using System.ComponentModel.DataAnnotations;

namespace Cliente2.Models
{
    public class ModelCliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
